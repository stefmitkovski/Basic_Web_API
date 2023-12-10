using System.ComponentModel.DataAnnotations;

namespace Basic_Web_API.Dto
{
    public class UpdateCountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
