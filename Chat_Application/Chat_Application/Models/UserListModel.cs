
namespace Chat_Application.Models
{
    public class UserListModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int  UserID { get; set; }

        public string UserName { get; set; }
    }
}
