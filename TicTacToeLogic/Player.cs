using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeGame
{
    public abstract class Player
    {
        public string Nickname { get; protected set; } = "Unknown";
        internal int PlayerNumber { get; set; }
        internal Player Opponent { get; set; }

        virtual internal Move GetMove(int?[,] fields, Player current)
        {
            return null;
        }
    }



    public class HumanPlayer : Player
    {
        public HumanPlayer(string nickName)
        {
            Nickname = nickName;
        }
    }


    public class Bot : Player
    {
        public Bot(string nickName)
        {
            Nickname = nickName;
        }

        protected Move GetRecMove(int?[,] fields, Player current, bool perfect)
        {
            // Abbruchbedingungen
            var winner = Game.Check4Winner(fields);
            if (winner > -1)
            {
                //0 in winner bedeutet unentschieden
                if (winner == 0)
                    return new Move(-1, -1, 0);
                if (current.PlayerNumber != winner && !typeof(Bot).IsAssignableFrom(current.GetType()))
                    return new Move(-1, -1, 1);
                else
                    return new Move(-1, -1, -1);
            }
            // --------------------------------------------


            List<Move> moves = new List<Move>();

            for (int x = 0; x < 3; x++)
            {
                // für alle x
                for (int y = 0; y < 3; y++)
                {
                    // für alle y
                    if (fields[x, y] == null) // zug möglich?
                    {
                        fields[x, y] = current.PlayerNumber; // zug machen

                        // rekursiv mit gemachtem zug für anderen spieler move finden.

                        var score = GetRecMove(fields, current.Opponent, perfect).Score;

                        // zug punkte zuweisen und merken
                        moves.Add(new Move(x, y, score));

                        // zug rückgängig mache
                        fields[x, y] = null;
                    }
                }
            }




            // Bewerten und Returnen
            if (typeof(Bot).IsAssignableFrom(current.GetType()))
            {
                if (perfect)
                    return moves.First(move => move.Score == moves.Max(x => x.Score));
                else
                    return moves.First(move => move.Score == moves.Min(x => x.Score));

            }
            else
            {
                if (perfect)
                    return moves.First(move => move.Score == moves.Min(x => x.Score));
                else
                    return moves.First(move => move.Score == moves.Max(x => x.Score));
            }
        }

    }

    public class PerfectBot : Bot
    {
        public PerfectBot(string nickName)
            : base(nickName)
        { }

        
        internal override Move GetMove(int?[,] fields, Player current)
        {
            return GetRecMove(fields, current, true);
        }

    }

    public class EasyBot : Bot
    {
        public EasyBot(string nickName)
            : base(nickName)
        { }


        internal override Move GetMove(int?[,] fields, Player current)
        {
            return GetRecMove(fields, current, false);
        }
    }
}
