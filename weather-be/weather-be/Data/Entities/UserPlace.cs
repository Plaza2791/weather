using System.ComponentModel.DataAnnotations;

namespace weather_be.Data.Entities
{
    public class UserPlace
    {
        [Required]
        [Key]
        public string code { get; set; }
        [Required]
        public string name { get; set; }
        [StringLength(255, ErrorMessage = "Aprašymas negali būti ilgesnis nei 255 simbolių")]
        public string? description { get; set; }
        public UserPlace(string code, string name, string? description)
        {
            this.code = code;
            this.name = name;
            this.description = description;
        }
    }
}
