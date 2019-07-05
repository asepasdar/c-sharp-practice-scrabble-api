using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using scrabbleAPI.Models;

namespace scrabbleAPI.Connector
{
    public class Conn
    {
        private string connstring;
        public Conn()
        {
            connstring = @"server=localhost;userid=root;password=;database=scrabble";
        }

        public User LoginWithFacebook(User user)
        {
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT count(name) FROM t_user WHERE fb_id = '"+user.fb_id+"'";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    var hasil = Convert.ToInt32(commandMysql.ExecuteScalar());
                    if (hasil == 0)
                        insertUser(user);

                    return selectByFBId(user);
                }
            }
        }
        public bool insertUser(User user)
        {
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "INSERT INTO t_user VALUES (" +
                        "NULL, '"+user.fb_id+ "', " +
                        "'" + user.name + "', " +
                        "'" + user.device_id + "', " +
                        "'" + user.fb_id + "-"+user.device_id+"', " +
                        "NOW(), " +
                        "NOW())";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();

                    if (commandMysql.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
            }
        }
        public User InsertGuest(User user)
        {
            User returnUser = new User();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "INSERT INTO t_user VALUES (" +
                        "NULL, '" + user.fb_id + "', " +
                        "'" + user.name + "', " +
                        "'" + user.device_id + "', " +
                        "'Guest-" + user.device_id + "', " +
                        "NOW(), " +
                        "NOW())";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    if (commandMysql.ExecuteNonQuery() > 0)
                    {
                        var id = Convert.ToInt32(commandMysql.LastInsertedId);
                        returnUser = selectUser(id);
                    }

                    return returnUser;
                }
            }
        }
        public User checkUser(User user)
        {
            User returnUser = new User();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT id, name, device_id, token FROM t_user WHERE name = '" + user.name + "')";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnUser.id = Convert.ToInt32(reader["id"]);
                            returnUser.name = Convert.ToString(reader["name"]);
                            returnUser.device_id = Convert.ToString(reader["device_id"]);
                            returnUser.token = Convert.ToString(reader["token"]);
                        }
                        connMysql.Close();
                        return returnUser;
                    }
                }
            }
        }
        public bool updateUser(int id, User user)
        {
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "UPDATE `t_user` SET `device_id`='" + user.device_id + "' WHERE  `id`=" + id;
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    if (commandMysql.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
            }
        }
        public User selectUser(int id)
        {
            User returnUser = new User();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT id, fb_id, name, device_id, token, created_at FROM t_user WHERE id = '" + id + "'";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnUser.id = Convert.ToInt32(reader["id"]);
                            returnUser.fb_id = Convert.ToString(reader["fb_id"]);
                            returnUser.name = Convert.ToString(reader["name"]);
                            returnUser.device_id = Convert.ToString(reader["device_id"]);
                            returnUser.token = Convert.ToString(reader["token"]);
                            returnUser.created_at = Convert.ToDateTime(reader["created_at"]);
                        }
                        connMysql.Close();
                        return returnUser;
                    }
                }
            }
        }
        public User selectByFBId(User user)
        {
            User returnUser = new User();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT id, fb_id, name, device_id, token FROM t_user WHERE id = '" + user.id + "' OR fb_id = '"+user.fb_id+"'";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnUser.id = Convert.ToInt32(reader["id"]);
                            returnUser.fb_id = Convert.ToString(reader["fb_id"]);
                            returnUser.name = Convert.ToString(reader["name"]);
                            returnUser.device_id = Convert.ToString(reader["device_id"]);
                            returnUser.token = Convert.ToString(reader["token"]);
                        }
                        connMysql.Close();
                        return returnUser;
                    }
                }
            }
        }

        public Room selectRoom(int id)
        {
            Room returnRoom = new Room();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT * FROM t_room WHERE id = " + id + "";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnRoom.id = Convert.ToInt32(reader["id"]);
                            returnRoom.user_rm = Convert.ToInt32(reader["user_rm"]);
                            returnRoom.user_guest = Convert.ToInt32(reader["user_guest"]);
                            returnRoom.status = Convert.ToInt32(reader["status"]);
                            returnRoom.time_created = Convert.ToDateTime(reader["time_created"]);
                            returnRoom.time_updated = Convert.ToDateTime(reader["time_updated"]);
                            returnRoom.ready_p1 = Convert.ToInt32(reader["ready_p1"]);
                            returnRoom.ready_p2 = Convert.ToInt32(reader["ready_p2"]);
                        }
                        connMysql.Close();
                        return returnRoom;
                    }
                }
            }
        }
        public bool updateRoom(Room room)
        {
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "UPDATE `t_room` SET `user_guest` = '" + room.user_guest + "', `status` = '" + room.status + "'  WHERE  `id`= " + room.id;
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    if (commandMysql.ExecuteNonQuery() > 0)
                        return true;
                }
            }
            return false;
        }

        public bool syncStart(Room room)
        {
            DateTime returnDate = DateTime.Now.AddSeconds(10);
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    if(room.user_rm != 0)
                        commandMysql.CommandText = "UPDATE `t_room` SET `ready_p1` = 1  WHERE  `id`= " + room.id;
                    else if(room.user_guest != 0)
                        commandMysql.CommandText = "UPDATE `t_room` SET `ready_p2` = 1  WHERE  `id`= " + room.id;
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    if (commandMysql.ExecuteNonQuery() > 0)
                        return true;
                    else return false;
                        
                }
            }
        }
        public Room createRoom(Room room)
        {
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "INSERT INTO t_room VALUES (NULL, '" + room.user_rm + "', '1', '1', NOW(), NOW(), 0, 0)";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    commandMysql.ExecuteNonQuery();
                    int id = Convert.ToInt32(commandMysql.LastInsertedId);
                    if (id != 0)
                        return selectRoom(id);
                    else
                        return new Room();
                }
            }

        }
        public bool DeleteRoom(int id)
        {
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "DELETE FROM `t_room` WHERE `id`=" + id;
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    if (commandMysql.ExecuteNonQuery() > 0)
                        return true;
                }
            }
            return false;
        }
        public Turn selectTurn(int id)
        {
            Turn returnTurn = new Turn();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT * FROM t_turn WHERE id = " + id + "";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnTurn.id = Convert.ToInt32(reader["id"]);
                            returnTurn.room_id = Convert.ToInt32(reader["room_id"]);
                            returnTurn.user_id = Convert.ToInt32(reader["user_id"]);
                            returnTurn.turn = Convert.ToInt32(reader["turn"]);
                            returnTurn.point = Convert.ToInt32(reader["point"]);
                        }
                        returnTurn.list = selectDetailTurn(id);
                        connMysql.Close();
                        return returnTurn;
                    }
                }
            }
        }
        public List<WordGrid> selectDetailTurn(int id)
        {
            List<WordGrid> allDetailTurn = new List<WordGrid>();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT * FROM t_detail_turn WHERE id_turn = " + id;
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();

                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allDetailTurn.Add(new WordGrid
                            {
                                col = Convert.ToInt32(reader["col"]),
                                row = Convert.ToInt32(reader["row"]),
                                data = Convert.ToString(reader["huruf"]),
                            });
                        }
                    }
                }
                connMysql.Close();
            }

            return allDetailTurn;
        }
        public Turn getEnemyPoint(int turnNo, Turn turn)
        {
            Turn returnTurn = new Turn();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT * FROM t_turn WHERE room_id = " + turn.room_id + " AND user_id = " + turn.user_id + " AND turn = " + turnNo + " ORDER BY id DESC LIMIT 0,1";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnTurn.id = Convert.ToInt32(reader["id"]);
                            returnTurn.room_id = Convert.ToInt32(reader["room_id"]);
                            returnTurn.user_id = Convert.ToInt32(reader["user_id"]);
                            returnTurn.turn = Convert.ToInt32(reader["turn"]);
                            returnTurn.point = Convert.ToInt32(reader["point"]);
                        }
                        returnTurn.list = selectDetailTurn(returnTurn.id);
                        connMysql.Close();
                        return returnTurn;
                    }
                }
            }
        }
        public Turn insertTurn(Turn turn)
        {
            Turn returnTurn = new Turn();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT count(id) FROM t_turn WHERE room_id = '"+turn.room_id+"' AND user_id = '"+turn.user_id+"' AND turn = '"+turn.turn+"'";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    if (Convert.ToInt32(commandMysql.ExecuteScalar()) > 0)
                        return returnTurn;

                    commandMysql.CommandText = "INSERT INTO `t_turn` (`room_id`, `user_id`, `turn`, `point`) VALUES ('" + turn.room_id + "', '" + turn.user_id + "', '" + turn.turn + "', '" + turn.point + "')";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    
                    if (commandMysql.ExecuteNonQuery() == 0)
                    {
                        connMysql.Close();
                        return returnTurn;
                    }

                    int id = Convert.ToInt32(commandMysql.LastInsertedId);

                    if (turn.list != null)
                    {
                        foreach (WordGrid word in turn.list)
                        {
                            commandMysql.CommandText = "INSERT INTO `t_detail_turn` (`id_turn`, `row`, `col`, `huruf`) VALUES (" + id + ", " + word.row + ", " + word.col + ", '" + word.data + "')";
                            commandMysql.CommandType = System.Data.CommandType.Text;
                            commandMysql.Connection = connMysql;
                            if (commandMysql.ExecuteNonQuery() == 0)
                            {
                                connMysql.Close();
                                return returnTurn;
                            }
                        }
                    }
                    returnTurn = selectTurn(id);
                    connMysql.Close();
                    return returnTurn;
                }
            }
        }
        public List<Room> RoomList()
        {
            List<Room> allRoom = new List<Room>();
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT * FROM t_room WHERE STATUS = 1 ORDER BY id DESC";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();

                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allRoom.Add(new Room
                            {
                                id = Convert.ToInt32(reader["id"]),
                                user_rm = Convert.ToInt32(reader["user_rm"]),
                                user_guest = reader["user_guest"] == null ? 0 : Convert.ToInt32(reader["user_guest"]),
                                status = Convert.ToInt32(reader["status"]),
                                time_created = Convert.ToDateTime(reader["time_created"])
                            }); ;
                        }
                    }
                }
                connMysql.Close();
            }

            return allRoom;
        }

        public bool isToken(string token)
        {
            if (token == "")
                return false;
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT COUNT(token) AS hasil FROM t_access WHERE token = '" + token + "'";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    using (MySqlDataReader reader = commandMysql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["hasil"]) > 0)
                                return true;
                            else
                                return false;
                        }
                    }
                }
            }
            return false;
        }

        public string checkConfig(string param)
        {
            if (param == "")
                return "";
            using (MySqlConnection connMysql = new MySqlConnection(connstring))
            {
                using (MySqlCommand commandMysql = connMysql.CreateCommand())
                {
                    commandMysql.CommandText = "SELECT value FROM t_settings WHERE param = '" + param + "'";
                    commandMysql.CommandType = System.Data.CommandType.Text;
                    commandMysql.Connection = connMysql;
                    connMysql.Open();
                    var myValue = Convert.ToString(commandMysql.ExecuteScalar());

                    return myValue;
                }
            }
        }
    }
}
