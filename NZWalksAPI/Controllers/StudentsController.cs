using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{

    //https://localhost:portnumber/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        //GET: https://localhost:portnumber/api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {

            string[] StudentNames = new string[] { "John", "Jane", "Mark", "Emily", "David" };

            //OK means return successful response in ASP.NET Core
            return Ok(StudentNames);

        }
    }
}
