using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.DAO;

namespace TicTacToeGame.Controller
{
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
