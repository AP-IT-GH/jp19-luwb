using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Coordinate
    {
        public long Id { get; set; }
        public string Mac_Tag { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        
    }
}
