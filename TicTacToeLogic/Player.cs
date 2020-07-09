using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeGame
{
    public class Player
    {
        string _nickName;
        bool _currentPlayer;

        public Player()
        {
        }

        public string Nickname
        {
            get
            {
                return _nickName;
            }
            set
            {
                _nickName = value;
            }
        }

        public bool Bot
        {
            get
            {
                return false;
            }
        }
    }
}
