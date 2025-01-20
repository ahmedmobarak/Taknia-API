using System.Text.Json.Serialization;

namespace MyHealthProfile.Models
{
    public class Allergy
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        [JsonIgnore]
        public ICollection<UserAllergy> UserAllergies { get; set; }
    }
}
