using Microsoft.EntityFrameworkCore;
using MyHealthProfile.Models;
using MyHealthProfile.Models.Dtos;
using MyHealthProfile.Persistence;

namespace MyHealthProfile.Repositories.Allergies
{
    public class AllergyService : IAllergyService
    {
        public readonly ApplicationDbContext _appDbContext;
        public AllergyService(ApplicationDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<LocalizedAllergyDto>> AllergiesList(string lang)
        {
            return await _appDbContext.Allergies
        .Select(a => new LocalizedAllergyDto
        {
            Id = a.Id,
            Name = lang == "ar" ? a.NameAr : a.NameEn
        })
        .ToListAsync();
        }
    }
}
