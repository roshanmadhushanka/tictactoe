using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    [Serializable]
    public enum PlayerType
    {
        NONE = 0,
        BALL = 1,
        CROSS = 10
    }

    public enum Difficulty
    {
        NONE = 0,
        EASY = 1,
        NORMAL = 2,
        HARD = 3
    }

    public enum GameStat
    {
        NOT_FINISHED = 0,
        PLAYER_BALL_WIN = 1,
        PLAYER_CROSS_WIN = 2,
        TIE = 3
    }

    public enum GameMode
    {
        SINGLE_PLAYER = 0,
        MULTI_PLAYER = 1,
        MULTI_PLAYER_STANDALONE = 2
    }
}
