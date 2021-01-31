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
        /// Gets the state of the piece.
        /// </summary>
        public State State { get; }

        /// <summary>
        /// Gets piece specific Number.
        /// </summary>
        public int SerialNumber => serialNumber;

        private readonly int serialNumber;

        /// <summary>
        /// Gets or sets the position of the piece.
        /// </summary>
        public Position Pos { get; set; }

        /// <summary>
        /// Piece.
        /// </summary>
        /// <param name="selectedState">White, Black or Blocked.</param>
        /// <param name="serialNumber">Piece number.</param>
        public Piece(State selectedState, int serialNumber)
        {
            this.serialNumber = serialNumber;
            State = selectedState;
        }
    }
}