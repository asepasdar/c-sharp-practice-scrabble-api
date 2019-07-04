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
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            Conn conn = new Conn();
            return conn.selectUser(id);
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            ReturnMessage message = new ReturnMessage();
            Conn mysqlGet = new Conn();
            User returnUser = new User();
            if (user == null)
                return new User();

            return mysqlGet.checkUser(user);
        }

        // PUT: api/User/5
        [HttpPost("{id}")]
        public void UpdateUser(int id, [FromBody] User user)
        {
            Conn conn = new Conn();
            conn.updateUser(id, user);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
