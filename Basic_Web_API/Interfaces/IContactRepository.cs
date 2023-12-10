using Basic_Web_API.Models;

namespace Basic_Web_API.Interfaces
{
    public interface IContactRepository
    {
        int Create(Contact contact);
        Contact Update(Contact contact);
        void Delete(int id);
        List<Contact> Get();
        Contact GetContactsWithCompanyAndCountry();
        List<Contact> FilterContacts(int? countryId, int? companyId);
    }
}
