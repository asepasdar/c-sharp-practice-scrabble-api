using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrabbleAPI.Models
{
    public class Room
    {
        public Room() { }
        public int id { get; set; }
        public int user_rm { get; set; }
        public int user_guest { get; set; }
        public int status { get; set; }
        public int ready_p1 { get; set; }
        public int ready_p2 { get; set; }
        public DateTime time_created { get; set; }
        public DateTime time_updated { get; set; }
    }
}
