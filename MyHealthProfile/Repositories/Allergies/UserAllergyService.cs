using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyHealthProfile.Models;
using MyHealthProfile.Models.Dtos;
using MyHealthProfile.Persistence;
using MyHealthProfile.Validators;
using System.Security.Claims;

namespace MyHealthProfile.Repositories.Allergies
{
    public class UserAllergyService : IUserAllergyService
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAllergyService(ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddAllergyAsync(UserAllergyDto request)
        {
            try
            {
                var validator = new UserAllergyValidator().Validate(request);
                if (!validator.IsValid) throw new ValidationException(validator.Errors);

                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                UserAllergy userAllergy = new()
                {
                    AllergyId = request.AllergyId,
                    Remarks = request.Remarks,
                    UserId = request.UserId
                };

                await _appDbContext.AddAsync<UserAllergy>(userAllergy);

                await _appDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<UserAllergy>> AllergiesListAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            // Fetch allergies for the logged-in user
            var userAllergies = await _appDbContext.UserAllergies
                .Where(ua => ua.UserId == userId)
                .Include(a => a.Allergy)
                .ToListAsync();

            return userAllergies;
        }

        public async Task<bool> DeleteUserAllergyAsync(int id)
        {
            var allergy = await _appDbContext.UserAllergies
                .FirstOrDefaultAsync(ua => ua.Id == id);

            if (allergy == null)
            {
                throw new KeyNotFoundException("Allergy not found or does not belong to the current user.");
            }

            _appDbContext.UserAllergies.Remove(allergy);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserAllergy> GetAllergyAsync(int id)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            var allergy = await _appDbContext.UserAllergies
                .FirstOrDefaultAsync(ua => ua.Id == id && ua.UserId == userId);

            if (allergy == null)
            {
                throw new KeyNotFoundException("Allergy not found or does not belong to the current user.");
            }

            return allergy;
        }

        // Edit Allergy by ID
        public async Task<bool> UpdateUserAllergyAsync(UserAllergyDto updatedAllergy)
        {

            var existingAllergy = await _appDbContext.UserAllergies
                .FirstOrDefaultAsync(ua => ua.Id == updatedAllergy.Id);

            if (existingAllergy == null)
            {
                throw new KeyNotFoundException("Allergy not found or does not belong to the current user.");
            }

            // Update fields
            existingAllergy.Remarks = updatedAllergy.Remarks;

            // Save changes
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
