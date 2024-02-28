using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateRegionRequestDto
    {

        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 characters")]
        public string Code { get; set; }


        [Required]
        [MaxLength(45, ErrorMessage = "Code has to be maximum of 45 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
