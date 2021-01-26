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
        /// Returns a position, converting an input into a valid position.
        /// </summary>
        /// <returns>Position returned.</returns>
        public Position GetPosition(ConsoleView view)
        {
            /*bool isConverted = false;
            int position = 0;

            while(!isConverted)
            {
                isConverted = int.TryParse(Console.ReadLine(),
                    out position)
                    && position > 0 && position < 16;
            }*/
            return player.PositionForNumber(view.InputedKeys);
        }

        public bool CheckPiece(int piece)
        {
            if(player.PositionForNumber(piece))
            {
                // do conjunto das peças da cor especificada, verifica o estado da
                // peça com o numero dado
                
                // verificar peça com o numero escrito, retornar verdadeiro se 
                // não estiver comida
            }
        }

        public void GetDirection()
        {
            // The converted input.
            //int convertedAux;

            // Boolean to check if the output convertion worked..
            //bool convertSuccesful = false;

            //Console.WriteLine("Which direction?");

            Console.WriteLine("Up/Middle (1) | Middle/Left (2) |" +
                " Middle/Right (3) | Lower/Middle (4)");

            // If our piece is in the middle, it can move diagonally.
            if (IsPieceInCenter(selectedPiece.Pos))
                Console.WriteLine("Lower/Right (5) | Upper/Right (6) |" +
                    " Lower/Left (7) | Upper/Left (8)");

            // Keep asking for a direction until we get a valid input.
            while(!convertSuccesful)
            {
                Console.Write("Which direction? (Insert a valid option.)");
                convertSuccesful = int.TryParse(Console.ReadLine(),
                    out convertedAux)
                    && (convertedAux > 0 && convertedAux <= 8);
            }
        }
        
        public void Run(ConsoleView consoleView)
        {
            running = true;
            gameState = GameState.Menu;
            consoleView.Start();
            while(running)
            {
                consoleView.GetInput(gameState);
            }
            consoleView.Finish();
        }

        public void Quit()
        {
            running = false;
        }
    }
}