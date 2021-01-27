using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto3_LP2_2020.Common
{
    /// <summary>
    /// A piece in the game that contains a State and a Position.
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// The state of the piece.
        /// </summary>
        public State State { get; private set;}

        public bool IsAlive{get;}
        public readonly int serialNumber;

        /// <summary>
        /// The position of the piece.
        /// </summary>
        public Position Pos { get; set; }

        /// <summary>
        /// Piece.
        /// </summary>
        /// <param name="selectedState">White, Black or Blocked.</param>
        public Piece(State selectedState, int serialNumber)
        {
            this.serialNumber = serialNumber;
            IsAlive = true;
            State = selectedState;
        }
    }
}