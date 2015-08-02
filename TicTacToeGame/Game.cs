using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TicTacToeGame.DAO;
using TicTacToeGame.DTO;

namespace TicTacToeGame
{
    public class Game
    {
        public static Game current_game { set; get; }
        public Player current_player { set; get; }
        public Player playerA { set; get; }
        public Player playerB { set; get; }
        public Difficulty difficulty { set; get; }
        public GameMode game_mode { set; get; }
        public PlayerType[,] board{set; get;}
        public bool moveAllowed { set; get; }
        public bool chanceOfPlayerAI { set; get; }
        //public bool chanceOfPlayerA { set; get; }
        public bool connected { set; get; }
        public Game()
        {
            
        }
        public Game(ref Player playerA,ref Player playerB,Difficulty difficulty,GameMode gameMode)
        {
            current_game = this;
            board = new PlayerType[3,3]{{PlayerType.NONE,PlayerType.NONE,PlayerType.NONE},
                                        {PlayerType.NONE,PlayerType.NONE,PlayerType.NONE},
                                        {PlayerType.NONE,PlayerType.NONE,PlayerType.NONE}};
            this.playerA = playerA;
            this.playerA.score = 0;
            this.playerB = playerB;
            this.difficulty = difficulty;
            this.game_mode = gameMode;
            if (game_mode == GameMode.SINGLE_PLAYER)
            {
                playerA.name = "User";
                playerB.name = "Computer";
                playerA.moveAllowed = true;
                playerB.moveAllowed = false;
            }else if(game_mode == GameMode.MULTI_PLAYER_STANDALONE){
                playerA.name = "Ball";
                playerB.name = "Cross";
                playerA.moveAllowed = true;
                playerB.moveAllowed = false;
                this.difficulty = Difficulty.NONE;
            }
            else if (game_mode == GameMode.MULTI_PLAYER)
            {
                this.difficulty = Difficulty.NONE;
            }
            this.current_player = playerA;
            this.connected = false;
        }

        public void resetGameBoard(){
            board[0, 0] = PlayerType.NONE;
            board[0, 1] = PlayerType.NONE;
            board[0, 2] = PlayerType.NONE;
            board[1, 0] = PlayerType.NONE;
            board[1, 1] = PlayerType.NONE;
            board[1, 2] = PlayerType.NONE;
            board[2, 0] = PlayerType.NONE;
            board[2, 1] = PlayerType.NONE;
            board[2, 2] = PlayerType.NONE;
        }
        
        public GameStat getGameStat()
        {
            
                Provide game status
                Player Ball wins
                Player Cross wins
                Player Tie
                Not finished
             
            //Check in rows
            int sum1 = (int)board[0, 0] + (int)board[0, 1] + (int)board[0, 2];
            int sum2 = (int)board[1, 0] + (int)board[1, 1] + (int)board[1, 2];
            int sum3 = (int)board[2, 0] + (int)board[2, 1] + (int)board[2, 2];

            //Check in columns
            int sum4 = (int)board[0, 0] + (int)board[1, 0] + (int)board[2, 0];
            int sum5 = (int)board[0, 1] + (int)board[1, 1] + (int)board[2, 1];
            int sum6 = (int)board[0, 2] + (int)board[1, 2] + (int)board[2, 2];

            //Check in diagonals
            int sum7 = (int)board[0, 0] + (int)board[1, 1] + (int)board[2, 2];
            int sum8 = (int)board[0, 2] + (int)board[1, 1] + (int)board[2, 0];

           
            if (sum1 == 3 || sum2 == 3 || sum3 == 3 || sum4 == 3 || sum5 == 3 || sum6 == 3 || sum7 == 3 || sum8 == 3)
            {
                return GameStat.PLAYER_BALL_WIN;
            }
            if (sum1 == 30 || sum2 == 30 || sum3 == 30 || sum4 == 30 || sum5 == 30 || sum6 == 30 || sum7 == 30 || sum8 == 30)
            {
                return GameStat.PLAYER_CROSS_WIN;
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == PlayerType.NONE)
                        return GameStat.NOT_FINISHED;
                }
            }
            return GameStat.TIE;

        }
        
        public bool isGameFinished(){
            if (getGameStat() == GameStat.PLAYER_BALL_WIN || getGameStat() == GameStat.PLAYER_CROSS_WIN)
            {
                if (getGameStat() == GameStat.PLAYER_BALL_WIN)
                {
                    Game.current_game.playerA.score++;
                }else if(getGameStat() == GameStat.PLAYER_CROSS_WIN){
                    Game.current_game.playerB.score++;
                }
                return true;
            }
            else if(getGameStat() == GameStat.TIE){
                return true;
            }
            else
            {
                return false;
            }
        }

        public void nextPlayer()
        {
            if (this.current_player == playerA)
            {
                this.current_player = playerB;
            }
            else
            {
                this.current_player = playerA;
            }
        }

        public void swapPlayer()
        {
            if (this.playerA.moveAllowed)
            {
                this.playerA.moveAllowed = false;
                this.playerB.moveAllowed = true;
                this.current_player = playerB;
            }
            else
            {
                this.playerA.moveAllowed = true;
                this.playerB.moveAllowed = false;
                this.current_player = playerA;
            }
        }

        public void saveGameScore()
        {
            if (this.game_mode == GameMode.SINGLE_PLAYER)
            {
                if (this.playerA.score > 0)
                {
                    ScoreDAO scoreDAO = new ScoreDAO();
                    scoreDAO.create(new ScoreDTO(this.playerA.id, this.playerA.score, this.difficulty, this.game_mode));
                }
            }
            else if (this.game_mode == GameMode.MULTI_PLAYER_STANDALONE)
            {
                ScoreDAO scoreDAO = new ScoreDAO();
                if (this.playerA.score > 0)
                {
                    scoreDAO.create(new ScoreDTO(this.playerA.id, this.playerA.score, this.difficulty, this.game_mode));
                }
                if (this.playerB.score > 0)
                {
                    scoreDAO.create(new ScoreDTO(this.playerB.id, this.playerB.score, this.difficulty, this.game_mode));
                }
            }
        }
    }
}
