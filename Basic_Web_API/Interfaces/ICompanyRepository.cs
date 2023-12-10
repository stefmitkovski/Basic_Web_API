using Basic_Web_API.Models;

namespace Basic_Web_API.Interfaces
{
    public interface ICompanyRepository
    {
        int Create(Company company);
        Company Update(Company company);
        void Delete(int id);
        List<Company> Get();
    }
}
