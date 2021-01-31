using System;
using System.Text;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.ConsoleApp
{
    /// <summary>
    /// Manipulates the game itself using the common library's GameManager class.
    /// </summary>
    public class Controller
    {
        private readonly GameManager gameManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Controller()
        {
            gameManager = new GameManager();
        }

        /// <summary>
        /// Draws the board.
        /// </summary>
        /// <returns>returns the board as a string..</returns>
        public string GetBoard()
        {
            StringBuilder board = new StringBuilder("----------------------------\n\n");

            // Row by Row
            for (int r = 0; r < gameManager.BoardArray.GetLength(1); r++)
            {
                board.Append("\t");

                // Column by Column
                for (int c = 0; c < gameManager.BoardArray.GetLength(0); c++)
                {
                    if (c == 0 && (r == 1 || r == 3))
                        board.Append("   ");

                    if (gameManager.BoardArray[c, r] == null)
                        board.Append("EM");
                    else if (gameManager.BoardArray[c, r].State == State.Black)
                        board.Append($"B{gameManager.BoardArray[c, r].SerialNumber + 1}");
                    else if (gameManager.BoardArray[c, r].State == State.White)
                        board.Append($"W{gameManager.BoardArray[c, r].SerialNumber + 1}");
                    else if (gameManager.BoardArray[c, r].State == State.Blocked)
                        board.Append("  ");

                    if ((r == 0 || r == 4) && c < 3)
                        board.Append("     ");
                    else if ((r == 1 || r == 3) && c < 3)
                        board.Append("  ");
                    else if (r == 2)
                        board.Append("     ");
                }

                board.Append("\n");
            }

            return board.ToString();
        }

        /// <summary>
        /// Check if the requested piece of the player is available (not dead).
        /// </summary>
        /// <param name="pieceNum">Int identifying the specific piece of
        /// the player.</param>
        /// <param name="turnBlack">Bool identifying the current player
        /// (helps find its pieces).</param>
        /// <returns>Bool, true if possible choice, false if not.</returns>
        public bool CheckPiece(int pieceNum, bool turnBlack)
        {
            // Call 'Common' method checking piece is alive and not 'stuck'
            return gameManager.IsPieceAvailable(pieceNum, turnBlack);
        }

        /// <summary>
        /// Check if the direction chosen by the player leads to an accessible
        /// space. If so, change position.
        /// </summary>
        /// <param name="pieceNum">The piece selected by the player to be
        /// moved.</param>
        /// <param name="turnBlack">Bool identifying the current player
        /// (helps find its pieces).</param>
        /// <param name="directionNumber">The direction selected by the
        /// player.</param>
        /// <returns>Bool, true if direction is possible, false if not.</returns>
        public bool CheckForDirection(
            int pieceNum, bool turnBlack, int directionNumber)
        {
            // Call 'Common' method checking if the wanted direction from the 
            // selected piece's position is valid and makes change if so.
            return gameManager.CheckDirection(pieceNum, turnBlack, directionNumber);
        }

        /// <summary>
        /// Verifies if there is a winner.
        /// </summary>
        /// <param name="turnBlack">Receives the turn.</param>
        /// <returns>returns a bool that verfies a win state or not.</returns>
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

        /// <summary>
        /// Starts the game loop.
        /// </summary>
        /// <param name="consoleView">Receives the consoleView.</param>
        public void Run(ConsoleView consoleView)
        {
            GameState gameState = GameState.Menu;
            consoleView.Start();
            while (true)
            {
                gameState = consoleView.GetInput(gameState);
            }
        }

        /// <summary>
        /// Finishes the program.
        /// </summary>
        public void Quit()
        {
            Console.WriteLine("\nGoodbye...");

            // Exit Program
            Environment.Exit(0);
        }
    }
}