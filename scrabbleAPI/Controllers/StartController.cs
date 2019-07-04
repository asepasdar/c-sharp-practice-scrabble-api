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
    public class StartController : ControllerBase
    {
        // POST: api/Start
        [HttpPost]
        public void Post([FromBody] Room room)
        {
            Conn conn = new Conn();
            conn.updateRoom(room);
        }

        [HttpPost("{id}")]
        public ActionResult<bool> SyncStart(int id, [FromBody] Room room)
        {
            Conn conn = new Conn();
            return conn.syncStart(room);
        }

        //GET : api/start
        [HttpGet]
        public ReturnMessage Configuration()
        {
            ReturnMessage msg = new ReturnMessage();
            Conn conn = new Conn();

            if (conn.checkConfig("server_state") != "online")
            {
                msg.status = "err-server";
                msg.message = "Mohon maaf server sedang offline";
            }
            else if (conn.checkConfig("game_version") != Request.Headers["game_version"])
            {
                msg.status = "err-version";
                msg.message = "Silahkan update game anda terlebih dahulu";
            }
            else
            {
                msg.status = "ok";
                msg.message = "Server online & game version sesuai";
            }
            return msg;
        }
    }
}
