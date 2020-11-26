using System;
using System.Collections.Generic;

namespace Baseball_Team_API.Models
{
    public partial class Member
    {
        public Member()
        {
            Attendee = new HashSet<Attendee>();
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Salt { get; set; }
        public byte[] Password { get; set; }
        public string AuthLevel { get; set; }

        public virtual ICollection<Attendee> Attendee { get; set; }
    }
}
