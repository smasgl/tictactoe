using System;
using System.Linq;

namespace TicTacToeGame
{


    public class Game
    {
        static readonly Random random = new Random();
        private readonly Player _player1;
        private readonly Player _player2;
        private Player _winner;
        private Player _current;
        private int _fullFields;
        readonly int?[,] _fields = new int?[3, 3];

        public Game(Player player1, Player player2, bool randomBeginner = false)
        {
            _player1 = player1;
            _player1.PlayerNumber = 1;
            _player2 = player2;
            _player2.PlayerNumber = 2;
            _player1.Opponent = _player2;
            _player2.Opponent = _player1;

            if (randomBeginner)
                _current = random.Next(2) == 1 ? _player1 : _player2;
            else
                _current = player1;
        }

        public void PlayerInput(int x, int y)
        {

            if (!CorrectInput(x, y)) return;

            _fields[x, y] = (_current == _player1) ? 1 : 2;
            _current = (_current == _player1) ? _player2 : _player1;

            _fullFields++;
            if (_fullFields >= 5)
            {
                int winnerPlayerNumber = Check4Winner(_fields);

                _winner = winnerPlayerNumber == 1
                            ? _player1
                            : winnerPlayerNumber == 2
                            ? _player2
                            : null;
            }

            if (_current.GetType()!= typeof(HumanPlayer))
            {
                Move move = _current.GetMove(_fields, _current);
                PlayerInput(move.XIndex, move.YIndex);
            }           
        }

        private bool CorrectInput(int x, int y)
        {
            if (x > 3 || y > 3 || x < 0 || y < 0)
                return false;

            if (_fields[x, y] != null)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if a player has won the Game, it is a tie or it is still open.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns>-1 if still open, 0 if it is a tie, or playernumber if somebody won</returns>
        internal static int Check4Winner(int?[,] fields)
        {
            // Check if sb has won
            for (int x = 0; x < 3; x++)
            {
                if(fields[x,0] == fields[x,1] && fields[x, 0] == fields[x, 2] && fields[x,0] != null)
                {
                    return fields[x, 0].Value;
                }
            }
            for (int y = 0; y < 3; y++)
            {
                if (fields[0, y] == fields[1, y] && fields[0, y] == fields[2, y] && fields[0, y] != null)
                {
                    return fields[0, y].Value;
                }
            }
            if (fields[0, 0] == fields[1, 1] && fields[0, 0] == fields[2, 2] && fields[0, 0] != null)
            {
                return fields[0, 0].Value;
            }
            if (fields[2, 0] == fields[1, 1] && fields[2, 0] == fields[0, 2] && fields[2, 0] != null)
            {
                return fields[2, 0].Value;
            }



            // Check if all fields are full (tie)
            int emptyFields = 0;
            foreach (var item in fields)
            {
                if (item == null)
                    emptyFields++;
            }

            return emptyFields == 0
                ? 0
                : - 1;
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

        public int FullFields
        {
            get
            {
                return _fullFields;
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

