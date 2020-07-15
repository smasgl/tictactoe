using System;
using System.Collections.Generic;
using TicTacToeGame;
using Toolbox.ConsoleTools;
using System.Linq;

namespace TicTacToeConsole
{
    class Program
    {
        static Player _player1;
        static Player _player2;
        static Game _game;
        static List<Player> _knownPlayers = new List<Player>();

        static void Main()
        {
            bool playAgain = false;
            do
            {
                MenuBeginning();
                PlayGame();

                Console.WriteLine();
                var menu = new ConsoleMenu<bool>("Wollt Ihr noch eine Runde spielen?", new List<ConsoleMenuItem>
                {
                    new ConsoleMenuItem<bool>("Ja", x =>
                    {
                        playAgain = x;
                        Console.Clear();
                    }, true),
                    new ConsoleMenuItem<bool>("Nein", x =>
                    {
                        playAgain = x;
                    }, false)
                });
                menu.RunConsoleMenu();
            } while (playAgain);
        }
 
        private static void MenuBeginning()
        {
            var menuBeginning = new ConsoleMenu<string>("Gegen Wen willst du spielen?", new List<ConsoleMenuItem>
                {
                    new ConsoleMenuItem<string>("Versus HardBot", x =>
                    {
                        _player2 = new PerfectBot("Mahmoud [HardBot]");
                    }),
                    new ConsoleMenuItem<string>("Versus EasyBot", x =>
                    {
                        _player2 = new EasyBot("Robert [EasyBot]");
                    }),
                    new ConsoleMenuItem<string>("Versus Player2", x =>
                    {
                        if (_player2 != null && _player2.GetType() == typeof(HumanPlayer)) return;
                        _player2 = GetPersonInfo(2, _knownPlayers.Select(x => x.Nickname).ToArray());
                        _knownPlayers.Add(_player2);
                    })
                });
            menuBeginning.RunConsoleMenu();

            if (_player1 == null)
            {
                _player1 = GetPersonInfo(1, _knownPlayers.Select(x => x.Nickname).ToArray());
                _knownPlayers.Add(_player1);
            }
        }

        private static Player GetPersonInfo(int playerNumber, string[] notAllowed)
        {
            var person = new HumanPlayer(            
                InputHelper.GetUserText(
                    $"Bitte Nickname für Spieler {playerNumber} eingeben",
                    "Sie haben ja gar nichts eingegeben.",
                    notAllowed,
                    "{{input}} ist bereits vergeben.",
                    true));
            return person;
        }

        private static void PlayGame()
        {
            _game = new Game(_player1, _player2);

            while (!_game.GameEnd)
            {
                Draw();

                int inputx = InputHelper.GetUserInteger(null, "Sie haben ja gar nichts eingegeben.", 1, 3, "Bitte eine Zahl zwischen 1-3 eingeben.");

                int inputy = InputHelper.GetUserInteger(null, "Sie haben ja gar nichts eingegeben.", 1, 3, "Bitte eine Zahl zwischen 1-3 eingeben.");

                _game.PlayerInput(inputx - 1,inputy - 1);
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

                    Console.Write($"{_game.Fields[j, i]}".Replace('1', 'X').Replace('2', 'O'));
                }
                cursorIndex1 = 0;
            }


            Console.SetCursorPosition(0, 11);
            #endregion
            #endregion
        }

        
    }
}

