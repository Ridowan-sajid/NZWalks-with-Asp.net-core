using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code has to be minimum of 3 characters")]
        [MaxLength(6, ErrorMessage = "Code has to be maximum of 6 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage ="Name has to be a maximum of 30 characters")]
        public string Name { get; set; }
        public string RegionImgUrl { get; set; }
    }
}
