using System;

namespace TicTacToeGame
{


    public class Game
    {


        Player _player1;
        Player _player2;
        Player _winner;
        Player _current;
        int _fullFields;
        int?[,] _fields = new int?[3, 3];

        public Game(Player player1, Player player2, Player WhoBegins)
        {
            _player1 = player1;
            _player2 = player2;

            _current = WhoBegins;
        }

        public bool PlayerInput(int x, int y)
        {
            if (!CorrectInput(x, y))
                return false;

            _fields[y, x] = (_current == _player1) ? 1 : 2;
            _current = (_current == _player1) ? _player2 : _player1;

            _fullFields++;
            if (_fullFields >= 5)
                GameEnded();

            return true;
        }

        private bool CorrectInput(int x, int y)
        {
            if (x > 3 || y > 3 || x < 0 || y < 0)
                return false;

            if (_fields[y, x] != null)
                return false;

            return true;
        }

        private void GameEnded()
        {
            int winningPlayerNumber = 0;

            for (int i = 0; i < 3; i++)
            {
                if(_fields[i,0] == _fields[i,1] && _fields[i, 0] == _fields[i, 2] && _fields[i,0] != null)
                {
                    winningPlayerNumber = Convert.ToInt32(_fields[i, 0]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (_fields[0, i] == _fields[1, i] && _fields[0, i] == _fields[2, i] && _fields[0, i] != null)
                {
                    winningPlayerNumber = Convert.ToInt32(_fields[0, i]);
                }
            }
            if (_fields[0, 0] == _fields[1, 1] && _fields[0, 0] == _fields[2, 2] && _fields[0, 0] != null)
            {
                winningPlayerNumber = Convert.ToInt32(_fields[0, 0]);
            }
            if (_fields[2, 0] == _fields[1, 1] && _fields[2, 2] == _fields[0, 2] && _fields[2, 0] != null)
            {
                winningPlayerNumber = Convert.ToInt32(_fields[2, 0]);
            }

            _winner = winningPlayerNumber == 1 
                ? _player1 
                : winningPlayerNumber == 2 
                ? _player2
                : null;
        }


        #region Props
        public Player Winner
        {
            get
            {
                return _winner;
            }
        }

        public Player Current
        {
            get
            {
                return _current;
            }
        }

        public int?[,] Fields
        {
            get
            {
                return _fields;
            }
        }

        public bool GameEnd
        {
            get
            {
                if (_winner != null || _fullFields == 9)
                    return true;
                else
                    return false;
            }
        }

        #endregion
    }
}

