using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Measurement
    {
        public long Id { get; set; }
        public string Mac_Anchor { get; set; }
        public string Mac_Tag { get; set; }
        public double Distance { get; set; }
        public string Unix_Timestamp { get; set; }
    }
}
