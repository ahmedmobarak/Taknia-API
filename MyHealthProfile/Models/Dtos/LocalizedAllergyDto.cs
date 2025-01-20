using System.ComponentModel.DataAnnotations;

namespace MyHealthProfile.Models.Dtos
{
    public class LocalizedAllergyDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
