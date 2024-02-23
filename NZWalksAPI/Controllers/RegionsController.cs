using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

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

           var regions = _nZWalksDbContext.Regions.ToList();

            return Ok(regions);

        }


        // GET REGION BY ID
        // GET: https//localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]   // ":Guid" is not necessary - used for type safe

        public IActionResult GetById([FromRoute]Guid id)    //"[FromRout]e" is not necessary
        {

            var region = _nZWalksDbContext.Regions.Find(id);
            //"Find" method only takes the primary key( So we can't use it for other properties such as code, name etc..)

            // Another Method
            // var region1 = _nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);
            // "FirstorDefault" mathod work with any parameter - (x => x.Name == id) - Here, We should pass the "Name" in the route

            if (region == null)
            {
                return NotFound();  //NotFound means 404
            }

            return Ok(region);




        }
    }
}
