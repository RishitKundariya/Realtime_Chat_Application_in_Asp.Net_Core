using System.ComponentModel.DataAnnotations;

namespace Chat_Application.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
