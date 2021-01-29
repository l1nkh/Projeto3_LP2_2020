using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto3_LP2_2020.Common
{
    /// <summary>
    /// Position that stores a X and Y value.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Column (X).
        /// </summary>
        public int X { get; set;}
        /// <summary>
        /// Row (Y).
        /// </summary>
        public int Y { get; set;}

        /// <summary>
        /// Constructor to make a Position.
        /// </summary>
        /// <param name="x">Column (X).</param>
        /// <param name="y">Row (Y).</param>
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}