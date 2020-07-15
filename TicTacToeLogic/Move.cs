using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeGame
{
    public class Move
    {
        public Move(int indexX, int indexY, int score)
        {
            XIndex = indexX;
            YIndex = indexY;
            Score = score;
        }

        public int XIndex { get; }

        public int YIndex { get; }

        public int Score { get; }
       
    }
}
