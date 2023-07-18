using Chat_Application.Models;
using Chat_Application.SignalRChat;
using Chat_Application.ViewModels;
using Chat_Application.Views.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Chat_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Chat_ApplicationContext chat_ApplicationContext;
        private readonly IHubContext<ChatHub> hubContext;
        public HomeController(ILogger<HomeController> logger, Chat_ApplicationContext _chat_ApplicationContext, IHubContext<ChatHub> notificationHub)
        {
            chat_ApplicationContext= _chat_ApplicationContext;
            _logger = logger;
            hubContext = notificationHub;
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
        public IActionResult Login(LoginViewModel loginModel)
        {

            if (ModelState.IsValid)
            {
                User user =chat_ApplicationContext.Users.Where(x=>x.Password == loginModel.Password && x.MobileNumber == loginModel.PhoneNumber).FirstOrDefault();
                if(user != null)
                {
                    HttpContext.Session.SetString("userName", user.UserName);
                    HttpContext.Session.SetInt32("userID", user.Id);

                    return RedirectToAction("Chat", "Home");
                }
                else
                {
                    return View();
                }
            }
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
                chat_ApplicationContext.Users.Add(user);
                chat_ApplicationContext.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat() 
        {
            ViewBag.Username = HttpContext.Session.GetString("userName");             
            return View();
        }
        [HttpGet]
        public IActionResult GetAllUserListByUserId()
        {
            int userID = Convert.ToInt32(HttpContext != null ? HttpContext.Session != null ? HttpContext.Session.GetInt32("userID") ?? default(int) : 2 : 2);
            return PartialView("_chatUserList", chat_ApplicationContext.UserListModels.FromSqlInterpolated($"EXEC  [dbo].[getAllUserList]  @UserId={userID}").ToList());
        }
        [HttpGet]
        public IActionResult GetChat(int ReceivedUserId)
        {
            try
            {
                User userReceive = chat_ApplicationContext.Users.Where(x => x.Id == ReceivedUserId).FirstOrDefault();
                int SentuserID = Convert.ToInt32(HttpContext != null ? HttpContext.Session != null ? HttpContext.Session.GetInt32("userID") ?? default(int) : 2 : 2);
                ChatScreenModel chatScreenModel = new ChatScreenModel();
                chatScreenModel.UserName = userReceive.UserName;
                chatScreenModel.chats = chat_ApplicationContext.ChatModels.FromSqlInterpolated($"EXEC [dbo].[getChat] @ReceiveUserId = {ReceivedUserId}, @SendUserId = {SentuserID}").ToList();
                return PartialView("_chatScreen",chatScreenModel);
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }
        public async Task<IActionResult> SaveChat(int ReceiveUserId,string message)
        {
            Chat chat = new Chat();
            chat.ReceiveUserId = ReceiveUserId;
            chat.Chat1 = message;
            chat.SendUserId =Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            chat.Timestap=DateTime.Now;
            await chat_ApplicationContext.Chats.AddAsync(chat);
            await chat_ApplicationContext.SaveChangesAsync();
            await  hubContext.Clients.All.SendAsync("ReceiveMessage");
            return Ok();
        }
    }
}