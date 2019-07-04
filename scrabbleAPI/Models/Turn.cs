using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrabbleAPI.Models
{
    public class Turn
    {
        public Turn() { }
        public int id;
        public int room_id;
        public int user_id;
        public int turn;
        public int point;
        public List<WordGrid> list;
    }
}
