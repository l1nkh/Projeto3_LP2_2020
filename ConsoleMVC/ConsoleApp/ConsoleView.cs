using System;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.ConsoleApp
{
    public class ConsoleView
    {
        private bool turnBlack;
        private Player player;
        private Controller controller;
        public Position ChosenPiece {get; private set;}
        public Position DestinationPos {get; set;}
        public ConsoleView(Controller controller)
        {
            player = new Player();
            this.controller = controller;
        }
        public void Start()
        {
            Console.WriteLine("Welcome to the game Felli!");
            Console.WriteLine();
            Console.WriteLine(
                "1) Start Game" +
                "\n2) Help" +
                "\n3ESC) Quit (Valid mid-game)");
        }

        public void GetInput(GameState gameState)
        {
            if(Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch(gameState)
                {
                    case GameState.Menu:
                        switch(key)
                        {
                            case ConsoleKey.D1:
                                break;
                            case ConsoleKey.D2:
                                break;
                            case ConsoleKey.Escape:
                                controller.Quit();
                                break;
                            default:
                                Console.WriteLine("Unknown option");
                                break;
                        }
                        break;
                    case GameState.TurnSelection:
                        switch(key)
                        {
                            case ConsoleKey.D1:
                                turnBlack = true;
                                gameState = GameState.SelectPiece;
                                break;
                            case ConsoleKey.D2:
                                turnBlack = false;
                                gameState = GameState.SelectPiece;
                                break;
                            case ConsoleKey.Escape:
                                controller.Quit();
                                break;
                            default:
                                Console.WriteLine("Unknown option");
                                break;
                        }
                        break;
                    case GameState.SelectPiece:
                        switch(key)
                        {
                            case ConsoleKey.D1:
                                if(controller.CheckPiece(1))
                                {
                                    controller.GetPosition();
                                    gameState = GameState.SelectDirection;
                                }   
                                break;
                            case ConsoleKey.D2:
                                if(controller.CheckPiece(2))
                                {
                                    controller.GetPosition();
                                    gameState = GameState.SelectDirection;
                                } 
                                break;
                            case ConsoleKey.D3:
                                if(controller.CheckPiece(3))
                                {
                                    controller.GetPosition();
                                    gameState = GameState.SelectDirection;
                                } 
                                break;
                            case ConsoleKey.D4:
                                if(controller.CheckPiece(4))
                                {
                                    controller.GetPosition();
                                    gameState = GameState.SelectDirection;
                                } 
                                break;
                            case ConsoleKey.D5:
                                if(controller.CheckPiece(5))
                                {
                                    controller.GetPosition();
                                    gameState = GameState.SelectDirection;
                                } 
                                break;
                            case ConsoleKey.D6:
                                if(controller.CheckPiece(6))    
                                {
                                    controller.GetPosition();
                                    gameState = GameState.SelectDirection;
                                } 
                                break;
                            case ConsoleKey.Escape:
                                controller.Quit();
                                break;
                            default:
                                Console.WriteLine("Unknown option");
                                break;
                        }
                        break;
                    case GameState.SelectDirection:
                        switch(key)
                        {
                            case ConsoleKey.D1:
                                // check direction
                                {
                                    // change position
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                }
                                break;
                            case ConsoleKey.D2:
                                // check direction
                                {
                                    // change position
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                }
                                break;
                            case ConsoleKey.D3:
                                // check direction
                                {
                                    // change position
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                }
                                break;
                            case ConsoleKey.D4:
                                // check direction
                                {
                                    // change position
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                }
                                break;
                            case ConsoleKey.D5:
                                // check direction
                                {
                                    // change position
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                }
                                break;
                            case ConsoleKey.D6:
                                // check direction
                                {
                                    // change position
                                    turnBlack = !turnBlack;
                                    gameState = GameState.SelectPiece;
                                }
                                break;
                            case ConsoleKey.Escape:
                                controller.Quit();
                                break;
                            default:
                                Console.WriteLine("Unknown option");
                                break;
                        }
                        break;
                    case GameState.VictoryScreen:
                        Console.WriteLine("Mete ai o texto para ele ver se ganhou ou perdeu");
                        switch(key)
                        {
                            case ConsoleKey.Spacebar:
                                //controller.VictoryScreen();
                                gameState = GameState.Menu;
                                break;
                            case ConsoleKey.Escape:
                                controller.Quit();
                                break;
                            default:
                                Console.WriteLine("Unknown option");
                                break;
                        }
                        break;
                }
            }
        }

        public void Finish()
        {
            Console.WriteLine("Bye!!!");
        }

        private void GameStart()
        {
            Console.WriteLine("Who plays first?");
            Console.WriteLine();
            Console.WriteLine(
                "1) Black" +
                "\n2) White");
        }

        private void GameLoop()
        {
            // BoardRender();
            Console.WriteLine("Which piece you want to move?");
            ChosenPiece = controller.GetPosition();
            Console.WriteLine("Where do you want to move it");
            DestinationPos = controller.GetDirection();
        }

        private void EndGame()
        {

        }
    }
}