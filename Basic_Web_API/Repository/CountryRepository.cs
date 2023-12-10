using Basic_Web_API.Data;
using Basic_Web_API.Interfaces;
using Basic_Web_API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Basic_Web_API.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        public int Create(Country country)
        {
            _context.Add(country);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var country = _context.Countries.Where(c => c.CountryId == id).FirstOrDefault();
            if (country != null)
            {
                _context.Remove(country);
                _context.SaveChanges();
            }
        }

        public List<Country> Get()
        {
            return _context.Countries.ToList();
        }

        public Country Update(Country country)
        {
            _context.Update(country);
            _context.SaveChanges();
            return country;
        }
    }
}
