using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Map
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public User User { get; set; }
        public ICollection<Anchor> Anchors { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
