using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrabbleAPI.Models
{
    public class WordGrid
    {
        public WordGrid() { }
        public int row { get; set; }
        public int col { get; set; }
        public string data { get; set; }
    }
}
