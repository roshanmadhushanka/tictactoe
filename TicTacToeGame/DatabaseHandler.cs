using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;

namespace TicTacToeGame
{
    class DatabaseHandler
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;

        public DatabaseHandler()
        {
            server = "localhost";
            database = "tictactoe";
            user = "root";
            password = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        public bool openConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                return false;
            }
        }

        public bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public List<Player> selectAllPlayers()
        {
            string query = "SELECT * FROM player";
            List<Player> list = new List<Player>();
            if(this.openConnection() == true){
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while(dataReader.Read()){
                    Player tmp = new Player();
                    tmp.id = Int32.Parse(dataReader["id"].ToString());
                    tmp.name = dataReader["name"].ToString();
                    list.Add(tmp);
                }
                dataReader.Close();
                this.closeConnection();
            }
            return list;
        }

        public bool save(Object obj)
        {
            if (obj.GetType() == typeof(Player))
            {
                Player tmp = (Player)obj;
                string query = "INSERT INTO `player`(`id`, `name`, `highest_score`) VALUES (null,'"+ tmp.name +"',0)";

                if (this.openConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.closeConnection();
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Cannot connect with the server");
            }
            return false;
        }

        public bool update(Object obj)
        {
            if (obj.GetType() == typeof(Player))
            {
                Player tmp = (Player)obj;
                string query = "";
            }
            return false;
        }
    }
}
