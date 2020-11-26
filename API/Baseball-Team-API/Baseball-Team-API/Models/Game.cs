using System;
using System.Collections.Generic;

namespace Baseball_Team_API.Models
{
    public partial class Game
    {
        public Game()
        {
            Attendee = new HashSet<Attendee>();
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime Temporal { get; set; }
        public bool Forfeit { get; set; }

        public virtual ICollection<Attendee> Attendee { get; set; }
    }
}
