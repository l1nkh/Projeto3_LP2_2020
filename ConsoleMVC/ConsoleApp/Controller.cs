using System;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.ConsoleApp
{
    public class Controller
    {
        private GameManager gameManager;
        private bool running;
        private GameState gameState;
        public Controller()
        {
            gameManager = new GameManager();
        }

        public string GetBoard()
        {
            string board = "\n";
            // Row by Row
            for (int r = 0; r<gameManager.BoardArray.GetLength(1); r++)
            {
                board += "\t";
                // Column by Column
                for (int c = 0; c<gameManager.BoardArray.GetLength(0); c++)
                {
                    if (c == 0 && (r == 1 || r == 3))
                        board += "   ";

                    if (gameManager.BoardArray[c, r] == null)
                        board += "EM";
                    else if (gameManager.BoardArray[c, r].State == State.Black)
                        board += $"B{gameManager.BoardArray[c, r].serialNumber}";
                    else if (gameManager.BoardArray[c, r].State == State.White)
                        board += $"W{gameManager.BoardArray[c, r].serialNumber}";
                    else if (gameManager.BoardArray[c, r].State == State.Blocked)
                        board += "  ";

                    if ((r == 0 || r == 4) && c < 3)
                        board += "     ";
                    else if ((r == 1 || r == 2 || r == 3) && c < 3)
                        board += "  ";
                }
                board += "\n";
            }
            return board;
        }

        /// <summary>
        /// Check if the requested piece of the player is available (not dead)
        /// </summary>
        /// <param name="pieceNum">Int identifying the specific piece of
        /// the player</param>
        /// <param name="turnBlack">Bool identifying the current player
        /// (helps find its pieces)</param>
        /// <returns>Bool, true if possible choice, false if not</returns>
        public bool CheckPiece(int pieceNum, bool turnBlack)
        {
            // Calls 'Common' method checking piece is alive and not 'stuck'
            return gameManager.IsPieceAvailable(pieceNum, turnBlack);
        }

        /// <summary>
        /// Check if the direction chosen by the player leads to an accessible
        /// space. If so, change position.
        /// </summary>
        /// <param name="pieceNum">The piece selected by the player to be
        /// moved</param>
        /// <param name="turnBlack">Bool identifying the current player
        /// (helps find its pieces)</param>
        /// <param name="directionNumber">The direction selected by the
        /// player</param>
        /// <returns>Bool, true if direction is possible, false if not</returns>
        public bool CheckForDirection(
            int pieceNum, bool turnBlack, int directionNumber)
        {
            bool validDirection = false;
            // Calls 'Common' method checking if the wanted direction from the 
            // selected piece's position is valid
                // If valid, call 'Common' method to transform its position 
                // according to direction
                // validDirection = true;
            return validDirection;
        }

        public bool CheckForWin(bool turnBlack)
        {
            // Call 'Common' method checking if there is a win
            // If valid, announce winner
            if (gameManager.CheckForWin(turnBlack))
            {
                // Write won board
                Console.WriteLine(GetBoard());
                // Announce Winner
                if (turnBlack)
                {
                    Console.WriteLine(">>> Game won by [BLACK] <<<");
                    return true;
                }
                else
                {
                    Console.WriteLine(">>> Game won by [WHITE] <<<");
                    return true;
                }
            }
            return false;
        }

        public void Run(ConsoleView consoleView)
        {
            running = true;
            gameState = GameState.Menu;
            consoleView.Start();
            while(running)
            {
                gameState = consoleView.GetInput(gameState);
            }
            // Exit Program
            Environment.Exit(0);
        }

        public void Quit()
        {
            Console.WriteLine("\nGoodbye...");
            running = false;
        }
    }
}