using MyHealthProfile.Models;
using MyHealthProfile.Models.Dtos;

namespace MyHealthProfile.Repositories.Allergies
{
    public interface IAllergyService
    {
        public Task<List<LocalizedAllergyDto>> AllergiesList(string lang);
    }
}
