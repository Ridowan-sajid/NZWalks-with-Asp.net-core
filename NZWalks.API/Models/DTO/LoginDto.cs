using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
