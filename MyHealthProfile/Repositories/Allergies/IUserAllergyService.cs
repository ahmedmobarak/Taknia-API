using MyHealthProfile.Models;
using MyHealthProfile.Models.Dtos;

namespace MyHealthProfile.Repositories.Allergies
{
    public interface IUserAllergyService
    {
        public Task<UserAllergy> GetAllergyAsync(int id);
        public Task<bool> UpdateUserAllergyAsync(UserAllergyDto userAllergy);
        public Task<bool> DeleteUserAllergyAsync(int id);
        public Task<List<UserAllergy>> AllergiesListAsync(string UserId);
        public Task<bool> AddAllergyAsync(UserAllergyDto userAllergy);
    }
}
