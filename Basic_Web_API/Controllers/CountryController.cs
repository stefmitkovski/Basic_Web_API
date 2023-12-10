using AutoMapper;
using Basic_Web_API.Dto;
using Basic_Web_API.Dto.Country;
using Basic_Web_API.Interfaces;
using Basic_Web_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basic_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        // GET: api/Country
        public IActionResult Get()
        {
            var countries = _mapper.Map<List<UpdateCountryDto>>(_countryRepository.Get());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(countries);
        }

        // POST: /api/Country/
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] CreateCountryDto countryDto)
        {
            if (countryDto == null)
                return BadRequest(ModelState);

            var countryExist = _countryRepository.Get()
                .Where(c => c.CountryName.Trim().ToUpper() == countryDto.CountryName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (countryExist != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMaped = _mapper.Map<Country>(countryDto);
            if (_countryRepository.Create(countryMaped) < 0)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        // PUT: /api/Country/
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update([FromBody] UpdateCountryDto countryDto)
        {
            if (countryDto == null)
                return BadRequest(ModelState);

            var existingCountry = _countryRepository.Get()
                .Where(c => c.CountryId == countryDto.CountryId)
                .FirstOrDefault();

            if (existingCountry == null)
            {
                return NotFound();
            }

            var nameExists = _countryRepository.Get()
                .Where(c => c.CountryName.Trim().ToUpper() == countryDto.CountryName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (nameExists != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            _mapper.Map(countryDto, existingCountry);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _countryRepository.Update(existingCountry);

            return NoContent();
        }

        // DELETE: /api/Country/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id)
        {
            var existingCountry = _countryRepository.Get()
                .Where(c => c.CountryId == id)
                .FirstOrDefault();

            if (existingCountry == null)
            {
                return NotFound();
            }

            _countryRepository.Delete(existingCountry.CountryId);

            return NoContent();
        }
    }
}
