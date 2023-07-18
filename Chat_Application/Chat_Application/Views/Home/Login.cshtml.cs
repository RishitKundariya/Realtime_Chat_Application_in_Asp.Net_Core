using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chat_Application.Views.Home
{
    public class LoginModel : PageModel
    {
        public string phonenumber { get; set; }
        public string password { get; set; }
    }
}
