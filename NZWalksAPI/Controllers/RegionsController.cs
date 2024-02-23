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
    }
}
