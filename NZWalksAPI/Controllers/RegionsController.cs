﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

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

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository, IMapper mapper
            )
        {
            this._nZWalksDbContext = dbContext;
            this._regionRepository = regionRepository;   //this keyword is not essential
            this._mapper = mapper;
        }






        //GET ALL REGIONS
        // GET: https//localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // --- Get Data From Database - Domain models ---
            var regionsDomain = await _regionRepository.GetAllAsync();

            // --- Map Domain Model to DTOs --- USING Mappers
            var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);


            // --- Return DTOs ---
            return Ok(regionsDto);

        }






        // GET REGION BY ID
        // GET: https//localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]   // ":Guid" is not necessary - used for type safe

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

        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)       //In post method, we receive the body from the client
        {

           if (ModelState.IsValid)
            {
                // Map/Convert DTO to Domain Model - Using Mappers
                var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

                // Use Domain Model to create Region
                regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

                // Map Domain model back to DTO - Using Mappers
                var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);  // Success code is 201
            }

            else
            {
                return BadRequest(ModelState);     // Error code is 400
            }

            

        }







        // Update Region
        // PUT: https//localhost:portnumber/api/regions/{id}


        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            if (!ModelState.IsValid)
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

            else
            {
                return BadRequest(ModelState);
            }

            
        }







        //Delete Region
        //DELETE: https//localhost:portnumber/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
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
