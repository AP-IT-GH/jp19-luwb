using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Tag
    {
        public long Id { get; set; }
        public string Mac { get; set; }
        public string Description { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public User User { get; set; }
        public Map Map { get; set; }
    }
}
