using System;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
                Controller controller = new Controller();
                ConsoleView view = new ConsoleView(controller);

                controller.Run(view);
        }
    }
}