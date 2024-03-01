using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    // https:// localhost:1234 /api/regions
    [Route("api/[controller]")] //api/[controller] can be replaced by api/regions

    [ApiController]  // Tells taht, This Controller is for API use

    

    //action methods - to create , read, update, delete
    public class RegionsController : ControllerBase
        
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this._nZWalksDbContext = dbContext;
            this._regionRepository = regionRepository;   //this keyword is not essential
            this._mapper = mapper;
            _logger = logger;
        }






        //GET ALL REGIONS
        // GET: https//localhost:portnumber/api/regions
        [HttpGet]
       //[Authorize(Roles = "Reader,Writer")]  
        public async Task<IActionResult> GetAll()
        {


            try
            {

                throw new Exception("This is a custom exception");


                // --- Get Data From Database - Domain models ---
                var regionsDomain = await _regionRepository.GetAllAsync();

                // --- Map Domain Model to DTOs --- USING Mappers

                _logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");  //$ symbol used to convert this to  JSON object
                var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);


                // --- Return DTOs ---
                return Ok(regionsDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }






        // GET REGION BY ID
        // GET: https//localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]   // ":Guid" is not necessary - used for type safe
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)    //"[FromRout]e" is not necessary
        { 
            
            
            // --- Get Region Domain ---
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();  //NotFound means 404
            }

            // --- Map Domain Model to DTOs --- USING Mappers
            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            //Return DTO back to client
            return Ok(regionDto);

        }






        //POST To Create a New Region
        //POST: https//localhost:portnumber/api/regions

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)       //In post method, we receive the body from the client
        {

           
                // Map/Convert DTO to Domain Model - Using Mappers
                var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

                // Use Domain Model to create Region
                regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

                // Map Domain model back to DTO - Using Mappers
                var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);  // Success code is 201
            

            

        }







        // Update Region
        // PUT: https//localhost:portnumber/api/regions/{id}


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            
                //Map DTO to Domain Model - Using Mappers
                var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

                // Check if region exists
                regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //Convert Domain Model to DTO - Using Mappers
                var regionDto = _mapper.Map<RegionDto>(regionDomainModel);


                return Ok(regionDto);
            

            
        }







        //Delete Region
        //DELETE: https//localhost:portnumber/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
           var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);


            return Ok(regionDto);
        }

    }
}
