using Chat_Application.Models;
using Chat_Application.ViewModels;
using Chat_Application.Views.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chat_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Chat_ApplicationContext chat_ApplicationContext;
        public HomeController(ILogger<HomeController> logger, Chat_ApplicationContext _chat_ApplicationContext)
        {
            chat_ApplicationContext= _chat_ApplicationContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = registerViewModel.Name;
                user.MobileNumber = registerViewModel.PhoneNumber;
                user.Password = registerViewModel.PassWord;
                return RedirectToAction("Login");
            }
            return View();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}