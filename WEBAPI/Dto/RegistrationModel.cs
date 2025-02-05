using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Dto
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public required string UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}