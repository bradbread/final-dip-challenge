using System;
using System.Collections.Generic;

namespace Baseball_Team_API.Models
{
    public partial class Attendee
    {
        public int EventId { get; set; }
        public string Username { get; set; }
        public decimal? Paid { get; set; }

        public virtual Game Event { get; set; }
        public virtual Member UsernameNavigation { get; set; }
    }
}
