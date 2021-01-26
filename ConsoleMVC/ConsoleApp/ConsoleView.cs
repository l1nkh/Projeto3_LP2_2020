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
        private Player player;
        private Controller controller;

        // I dont think this is necessary <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        public Position ChosenPiece {get; private set;}
        public Position DestinationPos {get; set;}

        public ConsoleView(Controller controller)
        {
            player = new Player();
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
                "\n1) Start Game" +
                "\n2) Help" +
                "\nESC) Quit (Valid mid-game)");
        }

        /// <summary>
        /// Message requesting users to specify which player goes first
        /// </summary>
        private void RequestTurnOrder()
        {
            Console.WriteLine("Which color plays first?");
            Console.WriteLine(
                "\n1) Black" +
                "\n2) White");
        }

        /// <summary>
        /// Message requesting which Piece the user wants to move
        /// </summary>
        private void RequestPiece()
        {
            Console.WriteLine("Which piece do you want to move?");
        }

        /// <summary>
        /// Message requesting user to specify direction of movement
        /// </summary>
        private void RequestDirection()
        {
            Console.WriteLine("To which direction do you want to move " +
                            "the piece?");
            Console.WriteLine(
                "(consider piece's current position as the '-')\n");

            Console.WriteLine("\t1  2  3");
            Console.WriteLine("\t \\ | /");
            Console.WriteLine("\t   -");
            Console.WriteLine("\t / | \\");
            Console.WriteLine("\t4  5  6");
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
                                RequestPiece();
                                break;
                            // 'White' plays first
                            case ConsoleKey.D2:
                                turnBlack = false;
                                // Move to SelectPiece
                                gameState = GameState.SelectPiece;
                                RequestPiece();
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
                                if(controller.CheckPiece(1, turnBlack))
                                {
                                    validPieceNum = 1;
                                    //controller.GetPosition(1);
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D2:
                                if(controller.CheckPiece(2, turnBlack))
                                {
                                    validPieceNum = 2;
                                    //controller.GetPosition(2);
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D3:
                                if(controller.CheckPiece(3, turnBlack))
                                {
                                    validPieceNum = 3;
                                    //controller.GetPosition(3);
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D4:
                                if(controller.CheckPiece(4, turnBlack))
                                {
                                    validPieceNum = 4;
                                    //controller.GetPosition(4);
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D5:
                                if(controller.CheckPiece(5, turnBlack))
                                {
                                    validPieceNum = 5;
                                    //controller.GetPosition(5);
                                    gameState = GameState.SelectDirection;
                                    RequestDirection();
                                }
                                break;
                            case ConsoleKey.D6:
                                if(controller.CheckPiece(6, turnBlack))
                                {
                                    validPieceNum = 6;
                                    //controller.GetPosition(6);
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
                                if (controller.CheckDirection(
                                    validPieceNum, turnBlack, 1))
                                {
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                    RequestPiece();
                                }
                                break;
                            case ConsoleKey.D2:
                                // check direction & change position
                                if (controller.CheckDirection(
                                    validPieceNum, turnBlack, 2))
                                {
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                    RequestPiece();
                                }
                                break;
                            case ConsoleKey.D3:
                                // check direction & change position
                                if (controller.CheckDirection(
                                    validPieceNum, turnBlack, 3))
                                {
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                    RequestPiece();
                                }
                                break;
                            case ConsoleKey.D4:
                                // check direction & change position
                                if (controller.CheckDirection(
                                    validPieceNum, turnBlack, 4))
                                {
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                    RequestPiece();
                                }
                                break;
                            case ConsoleKey.D5:
                                // check direction & change position
                                if (controller.CheckDirection(
                                    validPieceNum, turnBlack, 5))
                                {
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                    RequestPiece();
                                }
                                break;
                            case ConsoleKey.D6:
                                // check direction & change position
                                if (controller.CheckDirection(
                                    validPieceNum, turnBlack, 6))
                                {
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                    RequestPiece();
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

                        // >>>> ISTO NÃO DEVE SER AQUI <<<<
                        Console.WriteLine("Match Over");
                        // Text anouncing winner

                        switch(key)
                        {
                            // Restart Game
                            case ConsoleKey.Spacebar:
                                //controller.VictoryScreen();
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