using Chat_Application.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat_Application.SignalRChat
{
    public class ChatHub : Hub
    {
       // private Chat_ApplicationContext chat_ApplicationContext { get; set; }
        //private IHttpContextAccessor httpContextAccessor { get; set; }
        //public ChatHub(Chat_ApplicationContext _ApplicationContext, IHttpContextAccessor _httpContextAccessor)
        //{
        //    //chat_ApplicationContext = _ApplicationContext;
        //    //httpContextAccessor = _httpContextAccessor;
        //}

        public async Task SendMessage(string ReceivedUserId, string message)
        {
            //int? SenderUserId = httpContextAccessor.HttpContext.Session.GetInt32("userID");
            await  Clients.All.SendAsync("ReceiveMessage", ReceivedUserId, message);
        }
    }
}
