using System.ComponentModel.DataAnnotations;

namespace Basic_Web_API.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set;}
        public ICollection<Contact> Contacts { get; set; }
    }
}
