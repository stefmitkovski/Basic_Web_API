using AutoMapper;
using Basic_Web_API.Dto.Contact;
using Basic_Web_API.Interfaces;
using Basic_Web_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public ContactController(IContactRepository contactRepository, ICompanyRepository companyRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]
        // GET: api/Contact
        public IActionResult Get()
        {
            var contacts = _mapper.Map<List<UpdateContactDto>>(_contactRepository.Get());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(contacts);
        }

        [HttpGet("Filter/{companyId?}/{countryId?}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]
        // GET: api/Contact/Filter/{companyId?}/{countryId?}
        public IActionResult FilterContacts(int? companyId = 0, int? countryId = 0)
        {
            // if both companyId and countryId are zero
            if (companyId <= 0 && countryId <= 0)
            {
                return Ok(new List<Contact>());
            }

            return Ok(_mapper.Map<List<UpdateContactDto>>(_contactRepository.FilterContacts(companyId, countryId)));
        }

        [HttpGet("Specific/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]
        // GET: api/Contact/Specific/{id}
        public IActionResult GetContactsWithCompanyAndCountry(int id)
        {
            var contact = _contactRepository.GetContactsWithCompanyAndCountry(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UpdateContactDto>(contact));
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        // POST: /api/Contact
        public IActionResult Create([FromBody] CreateContactDto contactDto)
        {
            if (contactDto == null)
                return BadRequest(ModelState);

            var contactExist = _contactRepository.Get()
                .Where(c => c.ContactName.Trim().ToUpper() == contactDto.ContactName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (contactExist != null)
            {
                ModelState.AddModelError("", "Contact already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doesCompanyExist = _companyRepository.Get()
                .Where(c => c.CompanyId == contactDto.CompanyId)
                .FirstOrDefault();

            if (doesCompanyExist == null)
            {
                ModelState.AddModelError("", "The company doesn't exists");
                return StatusCode(422, ModelState);
            }

            var doesCountryExist = _countryRepository.Get()
                .Where(c => c.CountryId == contactDto.CountryId)
                .FirstOrDefault();

            if (doesCountryExist == null)
            {
                ModelState.AddModelError("", "The country doesn't exists");
                return StatusCode(422, ModelState);
            }

            var contactMaped = _mapper.Map<Contact>(contactDto);
            if (_contactRepository.Create(contactMaped) < 0)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        // PUT: /api/Contact
        public IActionResult Update([FromBody] UpdateContactDto contactDto)
        {
            if (contactDto == null)
                return BadRequest(ModelState);

            var existingContact = _contactRepository.Get()
                .Where(c => c.ContactId == contactDto.ContactId)
                .FirstOrDefault();

            if (existingContact == null)
            {
                return NotFound();
            }

            var nameExists = _contactRepository.Get()
                .Where(c => c.ContactName.Trim().ToUpper() == contactDto.ContactName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (nameExists != null)
            {
                ModelState.AddModelError("", "Contact already exists");
                return StatusCode(422, ModelState);
            }

            _mapper.Map(contactDto, existingContact);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _contactRepository.Update(existingContact);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        // DELETE: /api/Contact/{id}
        public IActionResult Delete(int id)
        {
            var existingContact = _contactRepository.Get()
                .Where(c => c.ContactId == id)
                .FirstOrDefault();

            if (existingContact == null)
            {
                return NotFound();
            }

            _contactRepository.Delete(existingContact.ContactId);

            return NoContent();
        }
    }
}
