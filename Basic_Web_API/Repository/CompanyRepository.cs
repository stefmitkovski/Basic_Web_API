using Basic_Web_API.Data;
using Basic_Web_API.Interfaces;
using Basic_Web_API.Models;

namespace Basic_Web_API.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public int Create(Company company)
        {
            _context.Add(company);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var company = _context.Companies.Where(c => c.CompanyId == id).FirstOrDefault();
            if (company != null)
            {
                _context.Remove(company);
                _context.SaveChanges();
            }
        }

        public List<Company> Get()
        {
            return _context.Companies.ToList();
        }

        public Company Update(Company company)
        {
            _context.Update(company);
            _context.SaveChanges();
            return company;
        }
    }
}
