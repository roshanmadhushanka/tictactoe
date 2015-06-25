using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class AI
    {
        Game game;
        PlayerType[,] board;
        
        public AI(ref Game game)
        {
            this.game = game;
            this.board = game.board;
        }
        public void makeMove()
        {
            bool madeMove = false;
            bool goodToMov = false;
            playWinner(false,ref goodToMov,ref madeMove);
            playDefensive(ref madeMove);
            playOffensive(ref madeMove);
            playRandom(ref madeMove);
        }

        public void playWinner(bool verify,ref bool goodToMov,ref bool madeMove)
        {
            if (madeMove)
            {
                return;
            }

            if (game.difficulty == Difficulty.EASY)
            {
                return;
            }

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

            int movVal = 2*(int)game.current_player.playerType;

            if (sum1 == movVal)
            {
                //1st row
                if (!verify)
                {
                    game.current_player.move(0, 0);
                    game.current_player.move(0, 1);
                    game.current_player.move(0, 2);
                    madeMove = true;
                }
                else
                {
                    goodToMov = true;
                }
                return;
            }

            if(sum2 == movVal){
                //2nd row
                if (!verify)
                {
                    game.current_player.move(1, 0);
                    game.current_player.move(1, 1);
                    game.current_player.move(1, 2);
                    madeMove = true;
                }
                else
                {
                    goodToMov = true;
                }
                return;
            }

            if (sum3 == movVal)
            {
                //3rd row
                if (!verify)
                {
                    game.current_player.move(2, 0);
                    game.current_player.move(2, 1);
                    game.current_player.move(2, 2);
                    madeMove = true;
                }
                else
                {
                    goodToMov = true;
                }
                return;
            }

            if (sum4 == movVal)
            {
                //1st column
                if (!verify)
                {
                    game.current_player.move(0, 0);
                    game.current_player.move(1, 0);
                    game.current_player.move(2, 0);
                    madeMove = true;
                }
                else
                {
                    goodToMov = true;
                }
                return;
            }


            if (sum5 == movVal)
            {
                //2nd column
                if (!verify)
                {
                    game.current_player.move(0, 1);
                    game.current_player.move(1, 1);
                    game.current_player.move(2, 1);
                    madeMove = true;
                }
                else
                {
                    goodToMov = true;
                }
                return;
            }


            if (sum6 == movVal)
            {
                //3rd column
                if (!verify)
                {
                    game.current_player.move(0, 2);
                    game.current_player.move(1, 2);
                    game.current_player.move(2, 2);
                    madeMove = true;
                }
                else
                {
                    goodToMov = true;
                }
                return;
            }

            if (sum7 == movVal)
            {
                //diagonal
                if (!verify)
                {
                    game.current_player.move(0, 0);
                    game.current_player.move(1, 1);
                    game.current_player.move(2, 2);
                    madeMove = true;
                }
                else
                {
                    goodToMov = true;
                }
                return;
            }

            if (sum8 == movVal)
            {
                //diagonal
                if (!verify)
                {
                    game.current_player.move(0, 2);
                    game.current_player.move(1, 1);
                    game.current_player.move(2, 0);
                    madeMove = true;
                }
                else
                {
                    goodToMov = true;
                }
                return;
            }
            return;
        }

        public void playDefensive(ref bool madeMove)
        {
            if (madeMove)
            {
                return;
            }

            if (game.difficulty == Difficulty.EASY || game.difficulty == Difficulty.NORMAL)
            {
                return;
            }

            if (game.difficulty == Difficulty.HARD && game.current_player.move(1, 1))
            {
                //Middle element block for hard
                madeMove = true;
                return;
            }
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

            int movVal;
            if (game.current_player.playerType == PlayerType.CROSS)
            {
                movVal = 2*(int)PlayerType.BALL;
            }
            else
            {
                movVal = 2 * (int)PlayerType.CROSS;
            }

            if (sum1 == movVal)
            {
                //1st row
                game.current_player.move(0, 0);
                game.current_player.move(0, 1);
                game.current_player.move(0, 2);
                madeMove = true;
                return;
            }

            if (sum2 == movVal)
            {
                //2nd row
                game.current_player.move(1, 0);
                game.current_player.move(1, 1);
                game.current_player.move(1, 2);
                madeMove = true;
                return;
            }

            if (sum3 == movVal)
            {
                //3rd row
                game.current_player.move(2, 0);
                game.current_player.move(2, 1);
                game.current_player.move(2, 2);
                madeMove = true;
                return;
            }

            if (sum4 == movVal)
            {
                //1st column
                game.current_player.move(0, 0);
                game.current_player.move(1, 0);
                game.current_player.move(2, 0);
                madeMove = true;
                return;
            }


            if (sum5 == movVal)
            {
                //2nd column
                game.current_player.move(0, 1);
                game.current_player.move(1, 1);
                game.current_player.move(2, 1);
                madeMove = true;
                return;
            }


            if (sum6 == movVal)
            {
                //3rd column
                game.current_player.move(0, 2);
                game.current_player.move(1, 2);
                game.current_player.move(2, 2);
                madeMove = true;
                return;
            }

            if (sum7 == movVal)
            {
                //diagonal
                game.current_player.move(0, 0);
                game.current_player.move(1, 1);
                game.current_player.move(2, 2);
                madeMove = true;
                return;
            }

            if (sum8 == movVal)
            {
                //diagonal
                game.current_player.move(0, 2);
                game.current_player.move(1, 1);
                game.current_player.move(2, 0);
                madeMove = true;
                return;
            }
            return;   
        }

        public void playOffensive(ref bool madeMove){
            if (madeMove)
            {
                return;
            }

            PlayerType opponent;
            if (game.current_player.playerType == PlayerType.CROSS)
            {
                opponent = PlayerType.BALL;
            }
            else
            {
                opponent = PlayerType.CROSS;
            }
            
            if (game.difficulty == Difficulty.HARD)
            {
                /*
                 * M|_|_
                 * _|_|_
                 * _|_|X
                 */
                if (game.board[2, 2] == opponent && game.current_player.move(0, 0))
                {
                    madeMove = true;
                    return;
                }
                /*
                 * _|_|M
                 * _|_|_
                 * X|_|_
                 */
                if (game.board[2, 0] == opponent && game.current_player.move(0, 2))
                {
                    madeMove = true;
                    return;
                }

                if (game.board[0, 2] == opponent && game.current_player.move(2, 0))
                {
                    madeMove = true;
                    return;
                }

                if (game.board[0, 0] == opponent && game.current_player.move(2, 2))
                {
                    madeMove = true;
                    return;
                }

                //Verifying moves for furether
                bool goodToMove = false;
                if (game.board[0, 2] == opponent && game.board[2, 0] == opponent && game.board[0, 1] == PlayerType.NONE)
                {
                    game.board[0, 1] = game.current_player.playerType;
                    playWinner(true,ref goodToMove,ref madeMove);
                    game.board[0, 1] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(0, 1);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[0, 2] == opponent && game.board[2, 0] == opponent && game.board[2, 1] == PlayerType.NONE)
                {
                    game.board[2, 1] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[2, 1] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(2, 1);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[0, 0] == opponent && game.board[2, 2] == opponent && game.board[0, 1] == PlayerType.NONE)
                {
                    game.board[0, 1] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[0, 1] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(0, 1);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[0, 0] == opponent && game.board[2, 2] == opponent && game.board[2, 1] == PlayerType.NONE)
                {
                    game.board[2, 1] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[2, 1] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(2, 1);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[1, 0] == opponent && game.board[2, 2] == opponent && game.board[0, 1] == PlayerType.NONE)
                {
                    game.board[0, 1] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[0, 1] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(0, 1);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[0, 0] == opponent && game.board[2, 1] == opponent && game.board[1, 0] == PlayerType.NONE)
                {
                    game.board[1, 0] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[1, 0] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(1, 0);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[0, 2] == opponent && game.board[2, 1] == opponent && game.board[1, 2] == PlayerType.NONE)
                {
                    game.board[1, 2] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[1, 2] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(1, 2);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[1, 2] == opponent && game.board[2, 0] == opponent && game.board[2, 1] == PlayerType.NONE)
                {
                    game.board[2, 1] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[2, 1] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(2, 1);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[2, 0] == opponent && game.board[0, 1] == opponent && game.board[1, 2] == PlayerType.NONE)
                {
                    game.board[1, 2] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[1, 2] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(1, 2);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[0, 0] == opponent && game.board[2, 1] == opponent && game.board[1, 0] == PlayerType.NONE)
                {
                    game.board[1, 0] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[1, 0] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(1, 0);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[0, 1] == opponent && game.board[1, 0] == opponent && game.board[0, 0] == PlayerType.NONE)
                {
                    game.board[0, 0] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[0, 0] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(0, 0);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[0, 1] == opponent && game.board[1, 2] == opponent && game.board[0, 2] == PlayerType.NONE)
                {
                    game.board[0, 2] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[0, 2] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(0, 2);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[1, 2] == opponent && game.board[2, 1] == opponent && game.board[2, 2] == PlayerType.NONE)
                {
                    game.board[2, 2] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[2, 2] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(2, 2);
                        madeMove = true;
                        return;
                    }
                }

                if (game.board[1, 0] == opponent && game.board[2, 1] == opponent && game.board[2, 0] == PlayerType.NONE)
                {
                    game.board[2, 0] = game.current_player.playerType;
                    playWinner(true, ref goodToMove, ref madeMove);
                    game.board[2, 0] = PlayerType.NONE;

                    if (goodToMove)
                    {
                        game.current_player.move(2, 0);
                        madeMove = true;
                        return;
                    }
                }

                return;

            }
        }

        public void playRandom(ref bool madeMove){
            if (madeMove)
            {
                return;
            }

            PlayerType opponent;
            if (game.current_player.playerType == PlayerType.CROSS)
            {
                opponent = PlayerType.BALL;
            }
            else
            {
                opponent = PlayerType.CROSS;
            }

            if (game.board[1, 1] == opponent && game.current_player.move(2, 0) || game.board[1, 1] == opponent && game.current_player.move(2, 2) || game.board[1, 1] == opponent && game.current_player.move(0, 0) || game.board[1, 1] == opponent && game.current_player.move(0, 2))
            {
                madeMove = true;
                return;
            }

            while (!madeMove)
            {
                System.Random objRandom = new Random();
                int x = objRandom.Next(0, 3);
                int y = objRandom.Next(0, 3);
                if (game.current_player.move(x, y))
                {
                    madeMove = true;
                    return;
                }
            }
        }

    }
}
