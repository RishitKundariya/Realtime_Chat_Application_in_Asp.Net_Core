using System;
using System.Collections.Generic;

namespace Chat_Application.Models
{
    public partial class Chat
    {
        public long Id { get; set; }
        public string Chat1 { get; set; } = null!;
        public int SendUserId { get; set; }
        public int ReceiveUserId { get; set; }
        public DateTime Timestap { get; set; }

        public virtual User ReceiveUser { get; set; } = null!;
        public virtual User SendUser { get; set; } = null!;
    }
}
