namespace Basic_Web_API.Dto.Contact
{
    public class CreateContactDto
    {
        public string ContactName { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
    }
}
