using System.ComponentModel.DataAnnotations;

namespace Basic_Web_API.Dto.Contact
{
    public class UpdateContactDto
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public int CompanyId { get; set; } 
        public int CountryId { get; set; }
    }
}
