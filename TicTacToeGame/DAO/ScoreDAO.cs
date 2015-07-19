using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeGame.Connection;
using TicTacToeGame.DTO;

namespace TicTacToeGame.DAO
{
    class ScoreDAO
    {
        public bool create(ScoreDTO score){
            DBConnector dbCon = new DBConnector();
           
            if (dbCon.openConnection() == true)
            {
                string query = "INSERT INTO `score`(`id`, `player_id`,`score`,`difficulty`,`game_mode`) VALUES (null,'" + score.player_id + "','"+score.score+"','"+ (int)score.difficulty +"','"+(int)score.game_mode+"')";
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                cmd.ExecuteNonQuery();
                dbCon.closeConnection();
                return true;
            }
            return false;
        }

        public List<ScoreDTO> getScore(PlayerDTO player){
            string query = "SELECT * FROM score WHERE player_id='"+ player.id +"'";
            List<ScoreDTO> list = new List<ScoreDTO>();
            DBConnector dbCon = new DBConnector();
            if (dbCon.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    ScoreDTO tmp = new ScoreDTO();
                    tmp.id = Int32.Parse(dataReader["id"].ToString());
                    tmp.player_id = player.id;
                    tmp.score = Int32.Parse(dataReader["score"].ToString());
                    tmp.difficulty = (Difficulty)Int32.Parse(dataReader["difficulty"].ToString());
                    tmp.game_mode = (GameMode)Int32.Parse(dataReader["game_mode"].ToString());
                    list.Add(tmp);
                }
                dataReader.Close();
                dbCon.closeConnection();
            }
            return list;
        }

        public ScoreDTO getHighScore(PlayerDTO player)
        {
            string query = "SELECT max(score) FROM score WHERE player_id='" + player.id + "'";
            ScoreDTO score = null;
            DBConnector dbCon = new DBConnector();
            if (dbCon.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dbCon.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                score.score = Int32.Parse(cmd.ExecuteScalar().ToString()); 
                dataReader.Close();
                dbCon.closeConnection();
            }
            return score;
        }

        public List<ScoreDTO> getRanking()
        {
            List<ScoreDTO> score_list = new List<ScoreDTO>();
            List<PlayerDTO> player_list = new PlayerDAO().selectAll();
            foreach (var player in player_list)
            {
                //score_list.Add(new ScoreDAO().getScore)
            }
            return score_list;
        }
    }
}
