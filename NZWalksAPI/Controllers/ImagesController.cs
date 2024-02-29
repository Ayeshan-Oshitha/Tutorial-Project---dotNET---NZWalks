using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if(ModelState.IsValid)
            {
                // Convert DTO to Domain model

                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    FileName = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription,
                };
                
                // User repository to upload image
                await _imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
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
