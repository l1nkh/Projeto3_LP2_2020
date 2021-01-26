using System;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.ConsoleApp
{
    public class Controller
    {
        private Player player;
        private bool running;
        private GameState gameState;
        public Controller()
        {
            player = new Player();
        }

        /// <summary>
        /// Check if the requested piece of the player is available (not dead)
        /// </summary>
        /// <param name="pieceNum">Int identifying the specific piece of
        /// the player</param>
        /// <param name="turnBlack">Bool identifying the current player
        /// (and its pieces)</param>
        /// <returns>Bool, true if possible choice, false if not</returns>
        public bool CheckPiece(int pieceNum, bool turnBlack)
        {
            // Calls 'Common' method checking piece's status
            return player.IsPieceAvailable(turnBlack, pieceNum);
        }

        /// <summary>
        /// Returns a position, converting an input into a valid position.
        /// </summary>
        /// <returns>Position returned.</returns>
        public Position GetPosition(ConsoleView view)
        {
            return player.PositionForNumber(view.InputedKeys);
        }

        /// <summary>
        /// Check if the direction chosen by the player leads to an
        /// accessible space
        /// </summary>
        /// <param name="pieceNum">The piece selected by the player to be
        /// moved</param>
        /// <param name="directionNumber">The direction selected by the
        /// player</param>
        /// <returns>Bool, true if direction is possible, false if not</returns>
        public bool CheckDirection(int pieceNum, int directionNumber)
        {
            bool validDirection = false;
            // Calls 'Common' method checking if the wanted direction from the 
            // selected piece's position is valid
            return validDirection;
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