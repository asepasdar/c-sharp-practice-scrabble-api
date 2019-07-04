using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrabbleAPI.Connector;
using scrabbleAPI.Models;

namespace scrabbleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        // GET: api/Game
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Game/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Game
        [HttpPost]
        public ActionResult<Turn> Post([FromBody] Turn turn)
        {
            Conn conn = new Conn();

            return conn.insertTurn(turn);
        }
        // POST: api/Game
        [HttpPost("{id}")]
        public ActionResult<Turn> GetEnemyTurn(int id, [FromBody] Turn turn)
        {
            Conn conn = new Conn();
            return conn.getEnemyPoint(id, turn);
        }


    }
}
