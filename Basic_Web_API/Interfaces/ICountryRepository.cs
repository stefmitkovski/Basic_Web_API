using Basic_Web_API.Models;

namespace Basic_Web_API.Interfaces
{
    public interface ICountryRepository
    {
        int Create(Country country);
        Country Update(Country country);
        void Delete(int id);
        List<Country> Get();
    }
}
