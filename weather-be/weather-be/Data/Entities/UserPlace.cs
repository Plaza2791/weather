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
        //[StringLength(255, ErrorMessage = "Description can't be longer than 255 symbols")]
        [MaxLength(255)]
        public string? description { get; set; }
    }
}
