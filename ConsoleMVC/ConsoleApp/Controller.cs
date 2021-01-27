using System;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.ConsoleApp
{
    public class Controller
    {
        private Board board;
        private bool running;
        private GameState gameState;
        public Controller()
        {
            board = new Board();
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
            // Calls 'Common' method checking piece's Status
            return board.IsPieceAvailable(pieceNum, turnBlack);
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
        public bool CheckForDirection(int pieceNum, bool turnBlack, int directionNumber)
        {
            bool validDirection = false;
            // Calls 'Common' method checking if the wanted direction from the 
            // selected piece's position is valid
                // If valid, call 'Common' method to transform its position 
                // according to direction
                // validDirection = true;
            return validDirection;
        }

        public bool CheckForWin()
        {
            bool gameWon = false;
            // Calls 'Common' method checking if there is a win
                // If valid, call 'Common' method announcing winner
            return gameWon;
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