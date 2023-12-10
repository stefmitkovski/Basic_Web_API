using AutoMapper;
using Basic_Web_API.Dto;
using Basic_Web_API.Dto.Company;
using Basic_Web_API.Dto.Contact;
using Basic_Web_API.Dto.Country;
using Basic_Web_API.Models;

namespace Basic_Web_API.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() 
        {
            // Company Mapping
            CreateMap<Company, CreateCompanyDto>();
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<Company, UpdateCompanyDto>();
            CreateMap<UpdateCompanyDto, Company>();
            
            // Country Mapping
            CreateMap<Country, CreateCountryDto>();
            CreateMap<CreateCountryDto, Country>();
            CreateMap<Country, UpdateCountryDto>();
            CreateMap<UpdateCountryDto, Country>();

            // Contact Mapping
            CreateMap<Contact, CreateContactDto>();
            CreateMap<CreateContactDto, Contact>();
            CreateMap<Contact, UpdateContactDto>();
            CreateMap<UpdateContactDto, Contact>();
        }
    }
}
