using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.DAO;
using TicTacToeGame.DTO;

namespace TicTacToeGame
{
    [Serializable]
    public class Player
    {
        public int id { set; get; }
        public string name { set; get; }
        public int score { set; get; }
        public PlayerType playerType {set; get;}
        public string ip_address { set; get; }
        public bool moveAllowed { set; get; }
         
        public Player()
        {

        }

        public Player(PlayerType playerType)
        {
            this.playerType = playerType;
            this.score = 0;
        }

        public Player(PlayerType playerType, string ipAddress)
        {
            this.playerType = playerType;
            this.ip_address = ipAddress;
            this.score = 0;
        }
        
        public bool move(int x,int y)
        {
            Game tmp_game = Game.current_game;
            //Console.WriteLine(tmp_game.board[x, y].ToString());
            if (x<3 && y <3 && tmp_game.board[x, y] == PlayerType.NONE)
            {
                tmp_game.board[x, y] = this.playerType;
                //Game.current_game.nextPlayer();
                Game.current_game.swapPlayer();
                return true;
            }
            return false;
        }

        public void saveScore()
        {
            if (Game.current_game.game_mode == GameMode.SINGLE_PLAYER)
            {
                ScoreDAO scoreDAO = new ScoreDAO();
                scoreDAO.create(new ScoreDTO(this.id, this.score, Game.current_game.difficulty, Game.current_game.game_mode));
            }
        }
    }

}
