using MessagePack;

namespace Chat_Application.Models
{
    public class ChatModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Chat { get; set; }
        public int Issent { get; set; }
    }
}
