using System.ComponentModel.DataAnnotations;

namespace Basic_Web_API.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
