using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {


        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if(ModelState.IsValid)
            {
                // User repository to upload image
            }

            return BadRequest(ModelState);
        }


        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))   //Here File measn IFORMFile in the imageUpdateRequestDto , Filename is the checking property, not a property of a imageUpdateRequestDto class
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }


            if(imageUploadRequestDto.File.Length > 10485760)    //Here File measn IFORMFile in the imageUpdateRequestDto , Length is the checking property, not a property of a imageUpdateRequestDto class
            {
                ModelState.AddModelError("file", "File size is more than 10MB, Please upload a smaller file");
            }
        }
    }
}
