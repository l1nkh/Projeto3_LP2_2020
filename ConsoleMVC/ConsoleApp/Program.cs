using System;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.ConsoleApp
{
    /// <summary>
    /// Class that starts the game.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Program starts here.
        /// </summary>
        private static void Main()
        {
                Controller controller = new Controller();
                ConsoleView view = new ConsoleView(controller);

                controller.Run(view);
        }
    }
}