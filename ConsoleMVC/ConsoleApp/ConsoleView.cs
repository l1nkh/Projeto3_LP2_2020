using System;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.ConsoleApp
{
    public class ConsoleView
    {
        // Identifies the current turn
        private bool turnBlack;
        // Identifies the number of the - valid - piece selected to be moved
        private int validPieceNum;
        private Controller controller;

        public ConsoleView(Controller controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Starting Message
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Welcome to the game Felli!");
            Console.WriteLine("Choose one of the following:");
            Console.WriteLine(
                "\n'1' -> Start Game" +
                "\n'2' -> Help" +
                "\nESC -> Quit (Valid mid-game)");
        }

        /// <summary>
        /// Message requesting users to specify which player goes first
        /// </summary>
        private void RequestTurnOrder()
        {
            Console.WriteLine("Which color plays first?");
            Console.WriteLine(
                "\n'1' -> Black" +
                "\n'2' -> White");
        }

        /// <summary>
        /// Message writing board and requesting which Piece the user
        /// wants to move.
        /// </summary>
        private void RequestPiece(bool turnBlack)
        {
            Console.Clear();
            if(turnBlack)
                Console.WriteLine("\n[BLACK] Player Turn\n");
            else
                Console.WriteLine("\n[WHITE] Player Turn\n");

            // Write Board
            Console.WriteLine(controller.GetBoard());

            Console.WriteLine("\nWhich piece do you want to move?");
            Console.WriteLine("(write the number of the piece)");
        }

        /// <summary>
        /// Message requesting user to specify direction of movement
        /// </summary>
        private void RequestDirection()
        {
            Console.WriteLine("To which direction do you want to move " +
                            "the piece?");
            Console.WriteLine("(write the number corresponding to the wanted " +
                            "direction for the movement)");
            Console.WriteLine(
                "(consider the piece's current position as the '-')\n");

            Console.WriteLine("\t1  2  3");
            Console.WriteLine("\t \\ | /");
            Console.WriteLine("\t   -");
            Console.WriteLine("\t / | \\");
            Console.WriteLine("\t4  5  6");
        }

        /// <summary>
        /// Message of the instructions and rules of the game
        /// </summary>
        private void ShowHelp()
        {
            Console.WriteLine("How does the game work?");
            Console.WriteLine("FelliGame is a PVP tabletop game where each" + 
            "player has 6 pieces.");
            Console.WriteLine();
            Console.WriteLine("White pieces are placed on one side of the" +
            " map, where 6 black pieces are placed on the opposite side of" +
            " the map.");
            Console.WriteLine();
            Console.WriteLine("The players play in turns.");
            Console.WriteLine();
            Console.WriteLine("The players can move the pieces (always" +
            " following the path line) in any direction where exists a free" +
            " adjacent spot. You can take out enemy pieces by jumping above" + 
            " them, and you'll land on the free spot adjacent to the previous" +
            " enemy spot. (Just like in Checkers.");
            Console.WriteLine();
            Console.WriteLine("Win conditions.\nThe player who takes all the" +
            " opponent pieces first wins!\nIf the current player blocks all" +
            " the opponent's pieces the current player wins!");
        }

        /// <summary>
        /// Checks if a player won the game, changing the
        /// gameState accordingly.
        /// </summary>
        /// <returns>Updated gameState</returns>
        private GameState UpdateGameState(bool tb)
        {
            if (controller.CheckForWin(tb))
            {
                return GameState.VictoryScreen;
            }
            else
            {
                turnBlack = !turnBlack;
                // Reset variable for the next turn's selection
                validPieceNum = 0;
                RequestPiece(turnBlack);
                return GameState.SelectPiece;
            }
        }

        /// <summary>
        /// Listens to specific input according to current gameState
        /// </summary>
        /// <param name="gameState">The current stage of the game</param>
        /// <returns>The updated gameState (if the expected actions
        /// happened)</returns>
        public GameState GetInput(GameState gameState)
        {
            if(Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch(gameState)
                {
                    // Start game / Show Help / Close Program 
                    case GameState.Menu:
                        switch(key)
                        {
                            // Move to Turn Selection (Start Game)
                            case ConsoleKey.D1:
                                gameState = GameState.TurnSelection;
                                RequestTurnOrder();
                                break;
                            // Show Help
                            case ConsoleKey.D2:
                                ShowHelp();
                                break;
                            // Close Game
                            case ConsoleKey.Escape:
                                Console.WriteLine(
                                    "Game closed by pressing [ESCAPE].");
                                controller.Quit();
                                break;

                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    // Set which color goes first
                    case GameState.TurnSelection:
                        switch(key)
                        {
                            // 'Black' plays first
                            case ConsoleKey.D1:
                                turnBlack = true;
                                // Move to SelectPiece
                                gameState = GameState.SelectPiece;
                                RequestPiece(turnBlack);
                                break;
                            // 'White' plays first
                            case ConsoleKey.D2:
                                turnBlack = false;
                                // Move to SelectPiece
                                gameState = GameState.SelectPiece;
                                RequestPiece(turnBlack);
                                break;
                            // Close Game
                            case ConsoleKey.Escape:
                                Console.WriteLine(
                                    "Game closed by pressing [ESCAPE].");
                                controller.Quit();
                                break;

                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    // If the piece is available, get its position
                    case GameState.SelectPiece:
                        switch(key)
                        {
                            case ConsoleKey.D1:
                                if(controller.CheckPiece(0, turnBlack))
                                {
                                    validPieceNum = 0;
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D2:
                                if(controller.CheckPiece(1, turnBlack))
                                {
                                    validPieceNum = 1;
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D3:
                                if(controller.CheckPiece(2, turnBlack))
                                {
                                    validPieceNum = 2;
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D4:
                                if(controller.CheckPiece(3, turnBlack))
                                {
                                    validPieceNum = 3;
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D5:
                                if(controller.CheckPiece(4, turnBlack))
                                {
                                    validPieceNum = 4;
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D6:
                                if(controller.CheckPiece(5, turnBlack))
                                {
                                    validPieceNum = 5;
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            // Close Game
                            case ConsoleKey.Escape:
                                Console.WriteLine(
                                    "Game closed by pressing [ESCAPE].");
                                controller.Quit();
                                break;

                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    // If chosen direction is valid, move to it
                    case GameState.SelectDirection:
                        switch(key)
                        {
                            case ConsoleKey.D1:
                                // check direction & change position
                                if (controller.CheckForDirection(
                                    validPieceNum, turnBlack, 1))
                                {
                                    gameState = UpdateGameState(turnBlack);
                                }
                                break;
                            case ConsoleKey.D2:
                                // check direction & change position
                                if (controller.CheckForDirection(
                                    validPieceNum, turnBlack, 2))
                                {
                                    gameState = UpdateGameState(turnBlack);
                                }
                                break;
                            case ConsoleKey.D3:
                                // check direction & change position
                                if (controller.CheckForDirection(
                                    validPieceNum, turnBlack, 3))
                                {
                                    gameState = UpdateGameState(turnBlack);
                                }
                                break;
                            case ConsoleKey.D4:
                                // check direction & change position
                                if (controller.CheckForDirection(
                                    validPieceNum, turnBlack, 4))
                                {
                                    gameState = UpdateGameState(turnBlack);
                                }
                                break;
                            case ConsoleKey.D5:
                                // check direction & change position
                                if (controller.CheckForDirection(
                                    validPieceNum, turnBlack, 5))
                                {
                                    gameState = UpdateGameState(turnBlack);
                                }
                                break;
                            case ConsoleKey.D6:
                                // check direction & change position
                                if (controller.CheckForDirection(
                                    validPieceNum, turnBlack, 6))
                                {
                                    gameState = UpdateGameState(turnBlack);
                                }
                                break;
                            // Close Game
                            case ConsoleKey.Escape:
                                Console.WriteLine(
                                    "Game closed by pressing [ESCAPE].");
                                controller.Quit();
                                break;

                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    // Announce Winner
                    case GameState.VictoryScreen:

                        switch(key)
                        {
                            // Restart Game
                            case ConsoleKey.Spacebar:
                                gameState = GameState.Menu;
                                break;
                            // Close Game
                            case ConsoleKey.Escape:
                                Console.WriteLine(
                                    "Game closed by pressing [ESCAPE].");
                                controller.Quit();
                                break;

                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                }
            }
            // Return Current (Updated) game state
            return gameState;
        }
    }
}