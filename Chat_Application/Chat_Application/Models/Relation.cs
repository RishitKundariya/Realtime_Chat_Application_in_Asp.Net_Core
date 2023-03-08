using System;
using System.Collections.Generic;

namespace Chat_Application.Models
{
    public partial class Relation
    {
        public int Id { get; set; }
        public int FriendId { get; set; }
        public int UserId { get; set; }

        public virtual User Friend { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
