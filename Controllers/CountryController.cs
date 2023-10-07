using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using World.Api.Data;
using World.Api.DTO.Country;
using World.Api.Models;
using World.Api.Repository.IRepository;

namespace World.Api.Controllers
{
    /// <summary>
    /// [Route("api/[controller]")] - This one shown url in swagger eg:
    /// /api/country - For Get
    /// /api/country - For Post
    /// Above the two URL will be same It will differentiated with GET and POST Attributes.
    /// /api/country/{id} - For Get
    /// /api/country/{id} - For Put    
    /// /api/country/{id} - For Delete
    /// Above the three URL will be same It will differentiated with Get and post attrubutes or verbs.
    /// </summary>
    [Route("api/[controller]")] 
    [ApiController]    
    public class CountryController : ControllerBase
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository, IMapper mapper, ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]// This is for document purpose.
        [ProducesResponseType(StatusCodes.Status200OK)]    // This is for document purpose.    
        //public ActionResult<IEnumerable<Country>> GetAll()
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetAll()
        {
            var countries = await _countryRepository.GetAll();
            var countriesDto = _mapper.Map<List<CountryDTO>>(countries);
            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countriesDto);
        }

        //[HttpGet] //If I use this attribute or verb means it's shown error because in single controller does not allowed
                    //two same attribute or verbs that why we mentioned {id:int}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Country>> GetById(int id)
        {
            var country = await _countryRepository.Get(id);
            if (country == null)
            {
                _logger.LogError($"Error while try to get record id:{id}");
                return NoContent();
            }
            var countryDto = _mapper.Map<CountryDTO>(country);
            //return _dbContext.Countries.Find(id);
            return Ok(countryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        //public ActionResult<Country> Create([FromBody]Country country)
        public async Task<ActionResult<CreateCountryDTO>> Create([FromBody] CreateCountryDTO countryDTO)
        {
            var result = _countryRepository.IsRecordExsits(x => x.Name == countryDTO.Name);
            if (result == true)
            {
                return Conflict("Country already exists in the database");
            }

            //Without Automapper
            //Country country = new Country();
            //country.Name = countryDTO.Name;
            //country.ShortName = countryDTO.ShortName;
            //country.CountryCode = countryDTO.CountryCode;

            //Use Automapper
            var country = _mapper.Map<Country>(countryDTO);

            //_dbContext.Countries.Add(country);
            //_dbContext.SaveChanges();
            await _countryRepository.Create(country);
            return CreatedAtAction("GetById", new { id = country.Id }, country);
            //return Ok();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> Update(int id, [FromBody] UpdateCountryDto countryDto)
        {
            if (countryDto == null || id != countryDto.Id)
            {
                return BadRequest();
            }

            //Without Automapper.
            //var countryFromDb = _dbContext.Countries.Find(id);
            //if(countryFromDb == null)
            //{
            //    return NotFound();
            //}
            //countryFromDb.Name = country.Name;
            //countryFromDb.ShortName = country.ShortName;
            //countryFromDb.CountryCode = country.CountryCode;

            //With Automapper
            var country = _mapper.Map<Country>(countryDto);

            //_dbContext.Countries.Update(country);
            //_dbContext.SaveChanges();
            await _countryRepository.Update(country);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var country = await _countryRepository.Get(id);

            if (country == null)
            {
                return NotFound();
            }

            //_dbContext.Countries.Remove(country);
            //_dbContext.SaveChanges();
            await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
