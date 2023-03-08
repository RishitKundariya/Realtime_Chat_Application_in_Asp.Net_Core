using System;
using System.Collections.Generic;

namespace Chat_Application.Models
{
    public partial class User
    {
        public User()
        {
            ChatReceiveUsers = new HashSet<Chat>();
            ChatSendUsers = new HashSet<Chat>();
            RelationFriends = new HashSet<Relation>();
            RelationUsers = new HashSet<Relation>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Chat> ChatReceiveUsers { get; set; }
        public virtual ICollection<Chat> ChatSendUsers { get; set; }
        public virtual ICollection<Relation> RelationFriends { get; set; }
        public virtual ICollection<Relation> RelationUsers { get; set; }
    }
}
