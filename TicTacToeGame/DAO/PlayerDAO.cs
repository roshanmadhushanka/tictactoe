using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TicTacToeGame.Connection;
using System.Windows.Forms;
using TicTacToeGame.DTO;

namespace TicTacToeGame.DAO
{
    //Player Data Access Object
    class PlayerDAO
    {
        public bool create(Player player){
            DBConnector dbCon = new DBConnector();
            
            if (dbCon.openConnection() == true)
            {
                string query = "INSERT INTO `player`(`id`, `name`,`status`) VALUES (null,'" + player.name + "','1')";
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                cmd.ExecuteNonQuery();
                dbCon.closeConnection();
                return true;
            }
            return false;
        }
        public int count()
        {
            DBConnector dbCon = new DBConnector();
            
            if (dbCon.openConnection() == true)
            {
                string query = "SELECT * FROM `player`";
                int count = 0;
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    count++;
                }
                dbCon.closeConnection();
                return count;
            }
            return -1;
        }
        public List<PlayerDTO> selectAll()
        {
            string query = "SELECT * FROM player WHERE status='1'";
            List<PlayerDTO> list = new List<PlayerDTO>();
            DBConnector dbCon = new DBConnector();
            if (dbCon.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    PlayerDTO tmp = new PlayerDTO();
                    tmp.id = Int32.Parse(dataReader["id"].ToString());
                    tmp.name = dataReader["name"].ToString();
                    list.Add(tmp);
                }
                dataReader.Close();
                dbCon.closeConnection();
            }
            return list;
        }
        public bool update(PlayerDTO player)
        {
            DBConnector dbCon = new DBConnector();
            if(dbCon.openConnection() == true){
                string query = "UPDATE `player` SET `name`='" + player.name + "',`status`='" + player.status + "' WHERE id='" + player.id + "'";
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                cmd.ExecuteNonQuery();
                dbCon.closeConnection();
                return true;
            }
            return false;
        }
    }
}
