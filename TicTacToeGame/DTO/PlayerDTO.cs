using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame.DTO
{
    class PlayerDTO
    {
        //Player Data Transfer Object
        public int id { set; get; }
        public string name { set; get; }
        public int score { set; get; }
        public int status { set; get; }

        public PlayerDTO()
        {

        }
        public PlayerDTO(Player player)
        {
            id = player.id;
            name = player.name;
            score = player.score;
            status = 1;
        }
    }
}
