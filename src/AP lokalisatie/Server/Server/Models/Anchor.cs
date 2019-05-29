using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Anchor 
    {
        public long Id { get; set; }
        public string Mac { get; set; }
        public string Description { get; set; }
        public int X_Pos { get; set; }
        public int Y_Pos { get; set; }
        public Map Map { get; set; }
        public User User { get; set; }
    }
}
