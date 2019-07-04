using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrabbleAPI.Models;
using scrabbleAPI.Connector;

namespace scrabbleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {

        // POST: api/Facebook
        [HttpPost]
        public ActionResult<User> LoginWithFacebook([FromBody] User user)
        {
            Conn conn = new Conn();
            return conn.LoginWithFacebook(user);
        }

        [HttpPost("{id}")]
        public ActionResult<User> LoginWithGuest(int id, [FromBody] User user)
        {
            Conn conn = new Conn();
            return conn.InsertGuest(user);
        }
    }
}
