using System;
using System.Collections.Generic;
using TicTacToeGame;
using Toolbox.ConsoleTools;
using ConsoleHelper;
using System.Linq;

namespace TicTacToeConsole
{
    class Program
    {
        static Player _player1;
        static Player _player2;
        static Game _game;
        static List<Player> _knownPlayers;

        static void Main(string[] args)
        {
            #region Nickname Input

            _knownPlayers = new List<Player>();

            _player1 = GetPersonInfo(1, _knownPlayers.Select(x => x.Nickname).ToArray());
            _knownPlayers.Add(_player1);

            _player2 = GetPersonInfo(2, _knownPlayers.Select(x => x.Nickname).ToArray());
            _knownPlayers.Add(_player2);

            #endregion

            bool playAgain = false;
            do
            {
                PlayGame();

                Console.WriteLine();


                var menu = new ConsoleMenu<bool>("Wollt Ihr noch eine Runde spielen?", new List<ConsoleMenuItem>
                {
                    new ConsoleMenuItem<bool>("Ja", x =>
                    {
                        playAgain = x;
                    }, true),
                    new ConsoleMenuItem<bool>("Nein", x =>
                    {
                        playAgain = x;
                    }, false)
                });
                menu.RunConsoleMenu();


            } while (playAgain);


        }

        private static Player GetPersonInfo(int playerNumber, string[] notAllowed)
        {
            var person = new Player
            {
                Nickname = InputHelper.GetUserText(
                    $"Bitte Nickname für Spieler {playerNumber} eingeben",
                    "Sie haben ja gar nichts eingegeben.",
                    notAllowed,
                    "{{input}} ist bereits vergeben.",
                    true)
            };
            return person;
        }


        private static void PlayGame()
        {
            Random random = new Random();
            _game = new Game(_player1, _player2, _knownPlayers[random.Next(_knownPlayers.Count)]);

            while (!_game.GameEnd)
            {
                Draw();

                string inputx = Console.ReadLine();
                string inputy = Console.ReadLine();

                if (inputx == "3" || inputx == "2" || inputx == "1")
                {
                    if (inputy == "3" || inputy == "2" || inputy == "1")
                    {
                        _game.PlayerInput(Convert.ToInt32(inputx) - 1, Convert.ToInt32(inputy) - 1);
                    }
                }
            }

            Draw();

            if (_game.Winner == null)
            {
                Console.WriteLine("\nUnentschieden!");
            }
            else
            {
                Console.WriteLine("\n" + _game.Winner.Nickname + " hat das Spiel gewonnen!");
            }
        }

        private static void Draw()
        {
            #region Draw
            #region DrawField
            Console.Clear();
            Console.WriteLine("TicTacToeConsole\n");
            Console.WriteLine("    1   2   3");
            Console.WriteLine("  +-----------+");
            Console.WriteLine($"1" + $" |   |   |   |");
            Console.WriteLine("  |---+---+---|");
            if(!_game.GameEnd)
            {
                Console.Write($"2" + $" |   |   |   |");
                Console.Write("\tPlayer " + _game.Current.Nickname + "\n");
            }
            else
                Console.WriteLine($"2" + $" |   |   |   |");
            Console.WriteLine("  |---+---+---|");
            Console.WriteLine($"3" + $" |   |   |   |");
            Console.WriteLine("  +-----------+\n");
            #endregion

            #region PutInFields

            int cursorIndex1 = 0;
            int cursorIndexRows1 = 2;

            for (int i = 0; i < 3; i++)
            {
                cursorIndexRows1 += 2;

                for (int j = 0; j < 3; j++)
                {
                    cursorIndex1 += 4;

                    Console.SetCursorPosition(cursorIndex1, cursorIndexRows1);

                    Console.Write($"{_game.Fields[i, j]}".Replace('1', 'X').Replace('2', 'O'));
                }
                cursorIndex1 = 0;
            }


            Console.SetCursorPosition(0, 11);
            #endregion
            #endregion
        }

        private static bool GetYesNoDecsisionFromUser(string prompt, string failMessage)
        {
            bool firstLoop = true;

            do
            {
                if (!firstLoop)
                    Console.WriteLine(failMessage);

                Console.Write($"{prompt} (j/n): ");
                var input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case 'j': return true;
                    case 'n': return false;
                }

                firstLoop = true;
            } while (true);

        }
    }
}
