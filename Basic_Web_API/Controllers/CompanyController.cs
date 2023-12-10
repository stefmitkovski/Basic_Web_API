using AutoMapper;
using Basic_Web_API.Dto.Company;
using Basic_Web_API.Interfaces;
using Basic_Web_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        // GET: api/Company
        public IActionResult Get()
        {
            var companies = _mapper.Map<List<UpdateCompanyDto>>(_companyRepository.Get());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(companies);
        }

        // POST: /api/Company/
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] CreateCompanyDto companyDto)
        {
            if (companyDto == null)
                return BadRequest(ModelState);

            var companyExist = _companyRepository.Get()
                .Where(c => c.CompanyName.Trim().ToUpper() == companyDto.CompanyName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (companyExist != null)
            {
                ModelState.AddModelError("", "Company already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyMaped = _mapper.Map<Company>(companyDto);
            if (_companyRepository.Create(companyMaped) < 0)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        // PUT: /api/Company/
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update([FromBody] UpdateCompanyDto companyDto)
        {
            if (companyDto == null)
                return BadRequest(ModelState);

            var existingCompany = _companyRepository.Get()
                .Where(c => c.CompanyId == companyDto.CompanyId)
                .FirstOrDefault();

            if (existingCompany == null)
            {
                return NotFound();
            }

            var nameExists = _companyRepository.Get()
                .Where(c => c.CompanyName.Trim().ToUpper() == companyDto.CompanyName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (nameExists != null)
            {
                ModelState.AddModelError("", "Company already exists");
                return StatusCode(422, ModelState);
            }

            _mapper.Map(companyDto, existingCompany);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _companyRepository.Update(existingCompany);

            return NoContent();
        }

        // DELETE: /api/Company/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id)
        {
            var existingCompany = _companyRepository.Get()
                .Where(c => c.CompanyId == id)
                .FirstOrDefault();

            if (existingCompany == null)
            {
                return NotFound();
            }

            _companyRepository.Delete(existingCompany.CompanyId);

            return NoContent();
        }


    }
}
