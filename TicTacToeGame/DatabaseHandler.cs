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


    }
}
