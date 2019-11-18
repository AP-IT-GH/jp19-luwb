using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Map> Maps { get; set; }
        [JsonIgnore]
        public ICollection<Anchor> Anchors { get; set; }
        [JsonIgnore]
        public ICollection<Tag> Tags { get; set; }
    }
}
