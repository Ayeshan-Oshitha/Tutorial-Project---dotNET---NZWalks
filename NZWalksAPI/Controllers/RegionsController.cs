using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    // https:// localhost:1234 /api/regions
    [Route("api/[controller]")] //api/[controller] can be replaced by api/regions

    [ApiController]  // Tells taht, This Controller is for API use



    //action methods - to create , read, update, delete
    public class RegionsController : ControllerBase
        
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this._nZWalksDbContext = dbContext;
        }

        //GET ALL REGIONS
        // GET: https//localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // --- Get Data From Database - Domain models ---
            var regionsDomain = _nZWalksDbContext.Regions.ToList();

            // --- Map Domain Model to DTOs ---

            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            // --- Return DTOs ---
            return Ok(regionsDto);

        }


        // GET REGION BY ID
        // GET: https//localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]   // ":Guid" is not necessary - used for type safe

        public IActionResult GetById([FromRoute]Guid id)    //"[FromRout]e" is not necessary
        {

            // var region = _nZWalksDbContext.Regions.Find(id);
            //"Find" method only takes the primary key( So we can't use it for other properties such as code, name etc..)

        
            
            
            // --- Get Region Domain ---
            var regionDomain = _nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);
            // "FirstorDefault" mathod work with any parameter - (x => x.Name == id) - Here, We should pass the "Name" in the route

            if (regionDomain == null)
            {
                return NotFound();  //NotFound means 404
            }


            // --- Map Region Domain Model to Region Dto ---
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };



            //Return DTO back to client
            return Ok(regionDto);

        }


        //POST To Create a New Region
        //POST: https//localhost:portnumber/api/regions

        [HttpPost]

        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)       //In post method, we receive the body from the client
        {

            // Map/Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };


            // Use Domain Model to create Region
            _nZWalksDbContext.Regions.Add(regionDomainModel);
            _nZWalksDbContext.SaveChanges();


            // Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);  // Success code is 201

        }





        // Update Region
        // PUT: https//localhost:portnumber/api/regions/{id}


        [HttpPut]
        [Route("{id:Guid}")]

        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {


            // Check if region exists
            var regionDomainModel =  _nZWalksDbContext.Regions.FirstOrDefault( x => x.Id == id );

            if (regionDomainModel == null)
            {
                return NotFound();
            }


            //Map DTO to Domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _nZWalksDbContext.SaveChanges();


            //Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };


            return Ok(regionDto);
        }

    }
}
