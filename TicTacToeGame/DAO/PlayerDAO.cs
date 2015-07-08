using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TicTacToeGame.Connection;
using System.Windows.Forms;

namespace TicTacToeGame.DAO
{
    class PlayerDAO
    {
        public bool save(Player player){
            DBConnector dbCon = new DBConnector();
            
            if (dbCon.openConnection() == true)
            {
                string query = "INSERT INTO `player`(`id`, `name`) VALUES (null,'" + player.name + "')";
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
                int count = 0;
                string query = "SELECT * FROM `player`";
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    count++;
                }
                return count;
            }
            return -1;
        }

        public List<Player> selectAll()
        {
            string query = "SELECT * FROM player";
            List<Player> list = new List<Player>();
            DBConnector dbCon = new DBConnector();
            if (dbCon.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Player tmp = new Player();
                    tmp.id = Int32.Parse(dataReader["id"].ToString());
                    tmp.name = dataReader["name"].ToString();
                    list.Add(tmp);
                }
                dataReader.Close();
                dbCon.closeConnection();
            }
            return list;
        }
    }
}
