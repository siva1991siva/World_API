using System.ComponentModel.DataAnnotations;

namespace World.Api.Models
{
    public class Country
    {
        [Key]//This Annotation will be set as primary key in database.
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }
        [Required]
        [MaxLength(10)]
        public string CountryCode { get; set; }
    }
}
