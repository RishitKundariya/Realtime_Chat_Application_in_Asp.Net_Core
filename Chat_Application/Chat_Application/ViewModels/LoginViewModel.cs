using System.ComponentModel.DataAnnotations;

namespace Chat_Application.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
