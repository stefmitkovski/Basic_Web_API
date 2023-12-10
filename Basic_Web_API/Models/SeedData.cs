using Basic_Web_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Basic_Web_API.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // Look for any existing data
                if (context.Companies.Any() || context.Contacts.Any() || context.Countries.Any())
                {
                    return;   // Database has been seeded
                }

                var countries = new List<Country>
            {
                new Country { CountryName = "United States" },
                new Country { CountryName = "United Kingdom" },
                new Country { CountryName = "Germany" },
                new Country { CountryName = "Japan" },
                new Country { CountryName = "Australia" },
            };

                context.Countries.AddRange(countries);
                context.SaveChanges();

                var companies = new List<Company>
            {
                new Company { CompanyName = "Tech Solutions Inc." },
                new Company { CompanyName = "Global Finance Group" },
                new Company { CompanyName = "Software Innovations Ltd." },
                new Company { CompanyName = "International Logistics Corp." },
            };

                context.Companies.AddRange(companies);
                context.SaveChanges();

                var contacts = new List<Contact>
            {
                new Contact { ContactName = "John Doe", CompanyId = 1, CountryId = 1 },
                new Contact { ContactName = "Alice Smith", CompanyId = 2, CountryId = 2 },
                new Contact { ContactName = "Robert Johnson", CompanyId = 3, CountryId = 3 },
                new Contact { ContactName = "Yuki Tanaka", CompanyId = 4, CountryId = 4 },
                new Contact { ContactName = "Emma Brown", CompanyId = 1, CountryId = 5 },
                new Contact { ContactName = "Michael Wang", CompanyId = 2, CountryId = 1 },
                // Add more contacts as needed
            };

                context.Contacts.AddRange(contacts);
                context.SaveChanges();
            }
        }
    }
}
