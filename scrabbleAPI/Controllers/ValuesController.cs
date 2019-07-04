using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using scrabbleAPI.Models;
using scrabbleAPI.Connector;

namespace scrabbleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Room>> Get()
        {
            Conn mysqlGet = new Conn();
            if (mysqlGet.isToken(Request.Headers["token"]))
                return mysqlGet.RoomList();
            else
            {
                return new Room[] { };
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Room> Get(int id)
        {
            Conn mysqlGet = new Conn();
            Room room = new Room();
            if (mysqlGet.isToken(Request.Headers["token"]))
                room = mysqlGet.selectRoom(id);
            else
                return room;

            return room;
        }

        // POST api/values untuk create room
        [HttpPost]
        public ActionResult<Room> Post([FromBody]Room room)
        {
            Conn mysqlGet = new Conn();
            if (mysqlGet.isToken(Request.Headers["token"]))
                return mysqlGet.createRoom(room);
            else
                return new Room();
        }
        // PUT api/values
        [HttpPost("{id}")]
        public ActionResult<Room> JoinRoom(int id, [FromBody]Room room)
        {
            Conn conn = new Conn();
            Room returnRoom = new Room();
            if (id == room.id && conn.updateRoom(room))
                returnRoom = conn.selectRoom(room.id);

            return returnRoom;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            ReturnMessage msg = new ReturnMessage();
            Conn conn = new Conn();
            if (conn.isToken(Request.Headers["token"]))
                conn.DeleteRoom(id);

            //Webrequest delete method attaches no DownloadHandler or UploadHandler
            //Need to check after this function
            return true;
        }
    }
}
