using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {   [Required]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(8,MinimumLength = 4, ErrorMessage ="You must specify Password between 4 and 8 charaters")]
        public string Password { get; set; }
    }
}