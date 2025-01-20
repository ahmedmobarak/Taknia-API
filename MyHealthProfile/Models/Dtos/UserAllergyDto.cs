namespace MyHealthProfile.Models.Dtos
{
    public class UserAllergyDto
    {
        public int? Id { get; set; }
        public int AllergyId { get; set; }
        public string? Remarks { get; set; }
        public string? UserId { get; set; }
    }
}
