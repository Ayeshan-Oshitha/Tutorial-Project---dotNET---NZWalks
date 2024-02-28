﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{

    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository) 
        { 
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }


        //CREATE Walk
        //POST: /api/walks

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            if(ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);


                await _walkRepository.CreateAsync(walkDomainModel);


                //Map Domain model to DTO

                var walkdto = _mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkdto);
            }

            else 
            {
                return BadRequest(ModelState); 
            }

            
            
        }





        //GET Walks
        // POST: //api/walks
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            var walksDomainModel = await _walkRepository.GetAllAsync();

            //Map Domain Model to DTO
            var walksDto = _mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walksDto);
        }




        // GET Walk By Id
        // GET: /api/Walks/{id}
        [HttpGet("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);


            if( walkDomainModel == null)
            {
                return NotFound();
            }

            //Map DomainModel to DTO
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }




        // UPDATE Walk by Id
        // PUT: /api/Walks/{id}
        [HttpPut("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

            if(ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

                await _walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                //Map Domain Model to DTO
                var WalkDto = _mapper.Map<WalkDto>(walkDomainModel);

                return Ok(WalkDto);
            }
            else
            {
                return BadRequest(ModelState);
            }

            
        }




        // DELETE a walk By Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var deletedWalkDomainModel =  await _walkRepository.DeleteAsync(id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO

            var deleteWalkDto = _mapper.Map<WalkDto>(deletedWalkDomainModel);

            return Ok(deleteWalkDto);
        }
    }
}
