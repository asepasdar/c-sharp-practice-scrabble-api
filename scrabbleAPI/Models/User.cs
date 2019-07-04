using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrabbleAPI.Models
{
    public class User
    {
        public User() { }
        public int id { get; set; }
        public string fb_id { get; set; }
        public string name { get; set; }
        public string device_id { get; set; }
        public string token { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
