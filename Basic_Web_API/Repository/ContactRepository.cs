using Basic_Web_API.Data;
using Basic_Web_API.Interfaces;
using Basic_Web_API.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Basic_Web_API.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;
        public ContactRepository(DataContext context)
        {
            _context = context;
        }

        public int Create(Contact contact)
        {
            _context.Add(contact);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var contact = _context.Contacts.Where(c => c.ContactId == id).FirstOrDefault();
            if (contact != null)
            {
                _context.Remove(contact);
                _context.SaveChanges();
            }
        }

        public List<Contact> FilterContacts(int? companyId, int? countryId)
        {

            var filterContacts = _context.Contacts.AsQueryable();

            if (countryId > 0)
            {
                filterContacts = filterContacts.Where(c => c.CountryId == countryId);
            }

            if (companyId > 0)
            {
                filterContacts = filterContacts.Where(c => c.CompanyId == companyId);
            }

            return filterContacts.ToList();

        }

        public List<Contact> Get()
        {
            return _context.Contacts.ToList();
        }

        public Contact GetContactsWithCompanyAndCountry(int id)
        {
            var contact = _context.Contacts
                .Where(c => c.ContactId == id).FirstOrDefault();

            return contact;
        }

        public Contact Update(Contact contact)
        {
            _context.Update(contact);
            _context.SaveChanges();
            return contact;
        }
    }
}
