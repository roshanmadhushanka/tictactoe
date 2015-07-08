using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.DAO;

namespace TicTacToeGame.Controller
{
    public enum SELECT
    {
        SINGLE_PLAYER = 0,
        MULTI_PLAYER = 1,
        MULTI_PLAYER_STANDALONE = 2
    }
    class PlayerController
    {
        public void save(string name)
        {
            Player tmp = new Player();
            tmp.name = name;
            PlayerDAO dao = new PlayerDAO();
            dao.save(tmp);
        }

        public int count()
        {
            return new PlayerDAO().count();
        }

    }
}
