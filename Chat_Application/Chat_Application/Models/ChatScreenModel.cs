namespace Chat_Application.Models
{
    public class ChatScreenModel
    {
        public ChatScreenModel()
        {
            this.chats= new List<ChatModel>();
        }
        public  List<ChatModel> chats { get; set; }

        public string UserName { get; set; }
    }
}
