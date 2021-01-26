using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto3_LP2_2020.Common
{
    /// <summary>
    /// Player class that handles the inputs.
    /// </summary>
    public class Player
    {   
        /// <summary>
        /// Convert the input into a valid position.
        /// </summary>
        /// <param name="position">Option selected from before.</param>
        /// <returns>Position to return.</returns>
        public Position PositionForNumber(int position)
        {
            switch (position)
            {
                // return each position of the board
                // then the blocked positions will be set as invalid to play
                case 1: return new Position(0, 0); 
                case 2: return new Position(0, 1);
                case 3: return new Position(0, 2);
                case 4: return new Position(1, 0);
                case 5: return new Position(1, 1);
                case 6: return new Position(1, 2);
                case 8: return new Position(2, 1);
                case 10: return new Position(3, 0);
                case 11: return new Position(3, 1);
                case 12: return new Position(3, 2);
                case 13: return new Position(4, 0);
                case 14: return new Position(4, 1);
                case 15: return new Position(4, 2);
                default: return null; // All the other positions "dont exist"
            }
        }
    }
}