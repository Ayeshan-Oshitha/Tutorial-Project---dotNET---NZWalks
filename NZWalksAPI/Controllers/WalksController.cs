using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Net;

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
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            
                //Map DTO to Domain Model
                var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);


                await _walkRepository.CreateAsync(walkDomainModel);


                //Map Domain model to DTO

                var walkdto = _mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkdto);
            

            

            
            
        }





        //GET Walks
        // GET: //api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {

            try
            {

                throw new Exception("This was the error");

                var walksDomainModel = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

                //Map Domain Model to DTO
                var walksDto = _mapper.Map<List<WalkDto>>(walksDomainModel);

                return Ok(walksDto);
            }

            catch (Exception ex)
            {
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }
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
        [ValidateModel]

        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
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
