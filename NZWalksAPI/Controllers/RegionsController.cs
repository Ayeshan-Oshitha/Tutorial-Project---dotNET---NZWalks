using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    // https:// localhost:1234 /api/regions
    [Route("api/[controller]")] //api/[controller] can be replaced by api/regions

    [ApiController]  // Tells taht, This Controller is for API use



    //action methods - to create , read, update, delete
    public class RegionsController : ControllerBase
        
    {

        //GET ALL REGIONS
        // GET: https//localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {

            var regions = new List<Region>
            {
                new Region
                {
                    Id=Guid.NewGuid(),
                    Name="Auckland Region",
                    Code="AKL",
                    RegionImageUrl="https://images.pexels.com/photos/5342978/pexels-photo-5342978.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },

                new Region
                {
                    Id = Guid.NewGuid(),
                    Name="Wellington Region",
                    Code="WLG",
                    RegionImageUrl="https://images.pexels.com/photos/710263/pexels-photo-710263.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }
            };

            return Ok(regions);

        }
    }
}
