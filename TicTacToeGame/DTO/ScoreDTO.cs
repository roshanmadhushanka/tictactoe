using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame.DTO
{
    class ScoreDTO
    {
        public int id { set; get; }
        public int player_id { set; get; }
        public int score { set; get; }
        public Difficulty difficulty { set; get; }
        public GameMode game_mode { set; get; }

        public ScoreDTO()
        {

        }

        public ScoreDTO(int player_id, int score, Difficulty difficulty, GameMode game_mode)
        {
            this.player_id = player_id;
            this.score = score;
            this.difficulty = difficulty;
            this.game_mode = game_mode;
        }
    }
}
