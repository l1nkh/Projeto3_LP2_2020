using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto3_LP2_2020.Common
{
    /// <summary>
    /// Class that consists of pieces and works as a board for the game.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Bi-dimensional array where we will store the pieces of the game.
        /// </summary>
        private readonly Piece[,] board;
        private readonly Piece[] blackPieceSet;
        private readonly Piece[] whitePieceSet;

        /// <summary>
        /// /// Create our board that creates it with the correct space.
        /// </summary>
        /// <param name="boardArray">Board with all pieces.</param>
        /// <param name="blackPieceSet">Board with black pieces.</param>
        /// <param name="whitePieceSet">Board with white pieces.</param>
        public Board(
            Piece[,] boardArray, Piece[] blackPieceSet, Piece[] whitePieceSet)
        {
            // Create "board" with correct dimensions
            board = boardArray;

            // Create sets of pieces for each player/color
            this.blackPieceSet = blackPieceSet;
            this.whitePieceSet = whitePieceSet;

            // Assign the appropriate values for each piece in boardArray and 
            // fill both PieceSets
            AssignStates();
        }

        /// <summary>
        /// Set the initial position of the method. Used in AssignStates().
        /// </summary>
        /// <param name="x">Row.</param>
        /// <param name="y">Column.</param>
        /// <param name="state">White, Black or Blocked.</param>
        /// <param name="serialNumber">Piece specific number.</param>
        private void SetInitialLocation(
                                int x, int y, State state, int serialNumber)
        {
            Piece piece = new Piece(state, serialNumber)
            {
                Pos = new Position(x, y),
            };

            // Add piece to the board in its initial position.
            board[x, y] = piece;

            // Add piece to the appropriate collection
            if (piece.State == State.Black)
                blackPieceSet[serialNumber] = piece;
            else if (piece.State == State.White)
                whitePieceSet[serialNumber] = piece;
        }

        /// <summary>
        /// Method to assign the initial states of the game.
        /// </summary>
        private void AssignStates()
        {
            // Since the default state of the board is always the same,
            // there just isn't any other way to go around this.

            // From top-to-down.
            // BLACK SIDE
            SetInitialLocation(0, 0, State.Black, 0);
            SetInitialLocation(1, 0, State.Black, 1);
            SetInitialLocation(2, 0, State.Black, 2);
            SetInitialLocation(0, 1, State.Black, 3);
            SetInitialLocation(1, 1, State.Black, 4);
            SetInitialLocation(2, 1, State.Black, 5);

            // WHITE SIDE
            SetInitialLocation(0, 3, State.White, 0);
            SetInitialLocation(1, 3, State.White, 1);
            SetInitialLocation(2, 3, State.White, 2);
            SetInitialLocation(0, 4, State.White, 3);
            SetInitialLocation(1, 4, State.White, 4);
            SetInitialLocation(2, 4, State.White, 5);

            // BLOCKED
            SetInitialLocation(0, 2, State.Blocked, 0);
            SetInitialLocation(2, 2, State.Blocked, 0);

            // Center coordinate [2, 1] is free (null)
        }

        /// <summary>
        /// Checks if a piece as possible spaces to move to.
        /// </summary>
        /// <param name="serialNumber">Number identifying a specific piece in
        /// the player's set.</param>
        /// <param name="turnBlack">Shows which player's turn it is.</param>
        /// <returns>Boolean, true if the piece has spaces it can move to, false
        /// if not.</returns>
        public bool HasMove(int serialNumber, bool turnBlack)
        {
            Position position = GetPosition(serialNumber, turnBlack);
            return SearchFreeSpace(position, turnBlack);
        }

        /// <summary>
        /// Get the Pos property of a specific piece.
        /// </summary>
        /// <param name="serialNumber">Number identifying a specific piece in
        /// the player's set.</param>
        /// <param name="turnBlack">Shows which player's turn it is.</param>
        /// <returns>Pos of the requested piece.</returns>
        public Position GetPosition(int serialNumber, bool turnBlack)
        {
            if (turnBlack)
                return blackPieceSet[serialNumber].Pos;
            else
                return whitePieceSet[serialNumber].Pos;
        }

        /// <summary>
        /// Gets the movement "vector" according to the user's input and makes
        /// transformation if valid.
        /// </summary>
        /// <param name="pos">Position of the selected piece.</param>
        /// <param name="direction">Direction option as inputted by the
        /// player.</param>
        /// <returns>Boolean, true if transformation was done, false if
        /// not.</returns>
        public bool CanDoMove(Position pos, int direction)
        {
            Position vector = GetVector(pos, direction);
            return AttemptMove(vector, pos);
        }

        /// <summary>
        /// Creates a "vector" according to player's input and the selected
        /// piece's position.
        /// </summary>
        /// <param name="pos">Selected piece's position.</param>
        /// <param name="direction">Direction option as inputted by the
        /// player.</param>
        /// <returns>Position with values describing the movement
        /// "vector".</returns>
        public Position GetVector(Position pos, int direction)
        {
            Position vect = new Position(0, 0);

            // If center piece
            if (pos.X == 1 && pos.Y == 2)
            {
                if (direction == 1)
                {
                    vect.X = -1;
                    vect.Y = -1;
                }

                if (direction == 2)
                {
                    vect.X = 0;
                    vect.Y = -1;
                }

                if (direction == 3)
                {
                    vect.X = 1;
                    vect.Y = -1;
                }

                if (direction == 4)
                {
                    vect.X = -1;
                    vect.Y = 1;
                }

                if (direction == 5)
                {
                    vect.X = 0;
                    vect.Y = 1;
                }

                if (direction == 6)
                {
                    vect.X = 1;
                    vect.Y = 1;
                }
            }

            // If close to center on center column
            else if (pos.X == 1 && (pos.Y == 1 || pos.Y == 3))
            {
                if (direction == 1 || direction == 4)
                {
                    vect.X = -1;
                    vect.Y = 0;
                }
                else if (direction == 3 || direction == 6)
                {
                    vect.X = 1;
                    vect.Y = 0;
                }
                else if (direction == 2)
                {
                    vect.X = 0;
                    vect.Y = -1;
                }
                else if (direction == 5)
                {
                    vect.X = 0;
                    vect.Y = 1;
                }
            }

            // If on diagonals of board
            else if ((pos.X == 0 || pos.X == 2) && pos.Y != 2)
            {
                if (pos.X == 0)
                {
                    if (pos.Y == 0)
                    {
                        if (direction == 3)
                        {
                            vect.X = 1;
                            vect.Y = 0;
                        }
                        else if (direction == 6 || direction == 5)
                        {
                            vect.X = 0;
                            vect.Y = 1;
                        }
                    }

                    if (pos.Y == 1)
                    {
                        if (direction == 3)
                        {
                            vect.X = 1;
                            vect.Y = 0;
                        }
                        else if (direction == 6 || direction == 5)
                        {
                            vect.X = 1;
                            vect.Y = 1;
                        }
                        else if (direction == 1 || direction == 2)
                        {
                            vect.X = 0;
                            vect.Y = -1;
                        }
                    }

                    if (pos.Y == 3)
                    {
                        if (direction == 3 || direction == 2 )
                        {
                            vect.X = 1;
                            vect.Y = -1;
                        }
                        else if (direction == 6)
                        {
                            vect.X = 1;
                            vect.Y = 0;
                        }
                        else if (direction == 4 || direction == 5)
                        {
                            vect.X = 0;
                            vect.Y = 1;
                        }
                    }

                    if (pos.Y == 4)
                    {
                        if (direction == 3 || direction == 2)
                        {
                            vect.X = 0;
                            vect.Y = -1;
                        }
                        else if (direction == 6)
                        {
                            vect.X = 1;
                            vect.Y = 0;
                        }
                    }
                }

                if (pos.X == 2)
                {
                    if (pos.Y == 0)
                    {
                        if (direction == 1)
                        {
                            vect.X = -1;
                            vect.Y = 0;
                        }
                        else if (direction == 4 || direction == 5)
                        {
                            vect.X = 0;
                            vect.Y = 1;
                        }
                    }

                    if (pos.Y == 1)
                    {
                        if (direction == 3 || direction == 2)
                        {
                            vect.X = 0;
                            vect.Y = -1;
                        }
                        else if (direction == 1)
                        {
                            vect.X = -1;
                            vect.Y = 0;
                        }
                        else if (direction == 4 || direction == 5)
                        {
                            vect.X = -1;
                            vect.Y = 1;
                        }
                    }

                    if (pos.Y == 3)
                    {
                        if (direction == 4)
                        {
                            vect.X = -1;
                            vect.Y = 0;
                        }
                        else if (direction == 1 || direction == 2)
                        {
                            vect.X = -1;
                            vect.Y = -1;
                        }
                        else if (direction == 6 || direction == 5)
                        {
                            vect.X = 0;
                            vect.Y = 1;
                        }
                    }

                    if (pos.Y == 4)
                    {
                        if (direction == 4)
                        {
                            vect.X = -1;
                            vect.Y = 0;
                        }
                        else if (direction == 1 || direction == 2)
                        {
                            vect.X = 0;
                            vect.Y = -1;
                        }
                    }
                }
            }

            // If on top center of board
            else if (pos.Y == 0 && pos.X == 1)
            {
                if (direction == 4 || direction == 1)
                {
                    vect.X = -1;
                    vect.Y = 0;
                }
                else if (direction == 5)
                {
                    vect.X = 0;
                    vect.Y = 1;
                }
                else if (direction == 6 && direction == 3)
                {
                    vect.X = 1;
                    vect.Y = 0;
                }
            }

            // If on bottom center of board
            else if ((pos.Y == board.GetLength(1) - 1) && pos.X == 1)
            {
                if (direction == 4 || direction == 1)
                {
                    vect.X = -1;
                    vect.Y = 0;
                }
                else if (direction == 2)
                {
                    vect.X = 0;
                    vect.Y = -1;
                }
                else if (direction == 6 && direction == 3)
                {
                    vect.X = 1;
                    vect.Y = 0;
                }
            }

            return vect;
        }

        /// <summary>
        /// Checks if any move is possible and does it if so.
        /// </summary>
        /// <param name="vector">Specifies the direction were a space must be
        /// searched.</param>
        /// <param name="startingPos">Value with the position of the selected
        /// piece.</param>
        /// <returns>Boolean, true if transformation was done with success,
        /// false if not.</returns>
        public bool AttemptMove(Position vector, Position startingPos)
        {
            if (board[startingPos.X + vector.X, startingPos.Y + vector.Y] == null)
            {
                board[startingPos.X, startingPos.Y].Pos = new Position(
                    startingPos.X + vector.X, startingPos.Y + vector.Y);

                board[startingPos.X + vector.X, startingPos.Y + vector.Y] =
                board[startingPos.X, startingPos.Y];
                board[startingPos.X, startingPos.Y] = null;
                return true;
            }
            else if (board[startingPos.X + vector.X, startingPos.Y + vector.Y].State
                == State.Blocked)
            {
                return false;
            }

            // If there is a diferent piece in path check it it can be eaten 
            else if (board[startingPos.X, startingPos.Y].State !=
                    board[startingPos.X + vector.X, startingPos.Y + vector.Y].State)
            {
                if (startingPos.X == 1 && startingPos.Y == 2)
                {
                    if (vector.X < 0 && board[startingPos.X - 1, startingPos.Y + (vector.Y * 2)]
                       == null)
                    {
                        // Eat piece
                        board[startingPos.X, startingPos.Y].Pos = new Position(
                            startingPos.X - 1, startingPos.Y + (vector.Y * 2));

                        board[startingPos.X - 1, startingPos.Y + (vector.Y * 2)] =
                        board[startingPos.X, startingPos.Y];
                        board[startingPos.X, startingPos.Y] = null;

                        RemovePiece(board[startingPos.X + vector.X, startingPos.Y + vector.Y]);

                        board[startingPos.X + vector.X, startingPos.Y + vector.Y] = null;
                        return true;
                    }

                    if (vector.X > 0 && board[startingPos.X + 1, startingPos.Y + (vector.Y * 2)]
                        == null)
                    {
                        // Eat piece
                        board[startingPos.X, startingPos.Y].Pos = new Position(
                            startingPos.X + 1, startingPos.Y + (vector.Y * 2));

                        board[startingPos.X + 1, startingPos.Y + (vector.Y * 2)] =
                        board[startingPos.X, startingPos.Y];
                        board[startingPos.X, startingPos.Y] = null;

                        RemovePiece(board[startingPos.X + vector.X, startingPos.Y + vector.Y]);

                        board[startingPos.X + vector.X, startingPos.Y + vector.Y] = null;
                        return true;
                }
                }

                if ((startingPos.X == 0) && (startingPos.Y == 0 || startingPos.Y == 4) &&
                        board[startingPos.X + 1, startingPos.Y + (vector.Y * 2)] == null)
                {
                        // Eat piece
                        board[startingPos.X, startingPos.Y].Pos = new Position(
                            startingPos.X + 1, startingPos.Y + (vector.Y * 2));

                        board[startingPos.X + 1, startingPos.Y + (vector.Y * 2)] =
                        board[startingPos.X, startingPos.Y];
                        board[startingPos.X, startingPos.Y] = null;

                        RemovePiece(board[startingPos.X + vector.X, startingPos.Y + vector.Y]);

                        board[startingPos.X + vector.X, startingPos.Y + vector.Y] = null;
                        return true;
                }

                if (startingPos.X == 2 && (startingPos.Y == 0 || startingPos.Y == 4)
                        && board[startingPos.X - 1, startingPos.Y + (vector.Y * 2)]
                       == null)
                {
                    // Eat piece
                    board[startingPos.X, startingPos.Y].Pos = new Position(
                        startingPos.X - 1, startingPos.Y + (vector.Y * 2));

                    board[startingPos.X - 1, startingPos.Y + (vector.Y * 2)] =
                    board[startingPos.X, startingPos.Y];
                    board[startingPos.X, startingPos.Y] = null;

                    RemovePiece(board[startingPos.X + vector.X, startingPos.Y + vector.Y]);

                    board[startingPos.X + vector.X, startingPos.Y + vector.Y] = null;
                    return true;
                }

                // Can i eat the piece?
                if (board[startingPos.X + (vector.X * 2), startingPos.Y + (vector.Y * 2)]
                        == null)
                {
                    // Eat piece                 
                    board[startingPos.X, startingPos.Y].Pos = new Position(
                        startingPos.X + (vector.X * 2), startingPos.Y + (vector.Y * 2));

                    board[startingPos.X + (vector.X * 2), startingPos.Y + (vector.Y * 2)] =
                    board[startingPos.X, startingPos.Y];
                    board[startingPos.X, startingPos.Y] = null;

                    RemovePiece(board[startingPos.X + vector.X, startingPos.Y + vector.Y]);

                    board[startingPos.X + vector.X, startingPos.Y + vector.Y] = null;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Search the spaces around a piece to see if any is null or if any
        /// can be eaten (if they are pieces of the adversary).
        /// </summary>
        /// <param name="startPosition">Pos of the piece searching for
        /// space.</param>
        /// <param name="turnBlack">Corrent turn.</param>
        /// <returns>Boolean, true if there is any free space, false if
        /// not.</returns>
        private bool SearchFreeSpace(Position startPosition, bool turnBlack)
        {
            // Piece is in CENTER spot of the board
            if (startPosition.X == 1 && startPosition.Y == 2)
            {
                // Check Row above and Row bellow
                for (int c = 0; c < board.GetLength(0); c++)
                {
                    if (board[c, 1] == null || board[c, 3] == null)
                    {
                        return true;
                    }

                    // Check spaces where it is possible to EAT pieces
                    else if (c == 1)
                    {
                        if (turnBlack)
                        {
                            if ((board[startPosition.X, startPosition.Y - 1].State ==
                                State.White &&
                                board[startPosition.X, startPosition.Y - 2] == null) ||
                                (board[startPosition.X, startPosition.Y + 1].State ==
                                State.White &&
                                board[startPosition.X, startPosition.Y + 2] == null))
                            {
                                return true;
                            }
                        }
                        else if (!turnBlack)
                        {
                            if ((board[startPosition.X, startPosition.Y - 1].State !=
                                State.Black ||
                                board[startPosition.X, startPosition.Y - 2] != null) &&
                                (board[startPosition.X, startPosition.Y + 1].State !=
                                State.Black ||
                                board[startPosition.X, startPosition.Y + 2] != null))
                            {
                                continue;
                            }

                            return true;
                        }
                    }
                }
            }

            // Piece is in CENTER COLUMN of the board
            // (but is not in center of the board)
            else if (startPosition.X == 1 && startPosition.Y != 2)
            {
                // Check Left and Right
                if (board[startPosition.X - 1, startPosition.Y] == null ||
                    board[startPosition.X + 1, startPosition.Y] == null)
                {
                    return true;
                }

                // if Above CENTER, check Bellow (and check to EAT)
                if (startPosition.Y < 2)
                {
                    // Piece BELLOW is empty
                    if (board[startPosition.X, startPosition.Y + 1] == null)
                        return true;

                    if (board[startPosition.X, startPosition.Y].State !=
                        board[startPosition.X, startPosition.Y + 1].State
                        && board[startPosition.X, startPosition.Y + 2] == null)
                    {
                        return true;
                    }
                }

                // if Bellow CENTER, check Above (and check to EAT)
                if (startPosition.Y > 2)
                {
                    // Piece ABOVE is empty
                    if (board[startPosition.X, startPosition.Y - 1] == null)
                        return true;

                    if (board[startPosition.X, startPosition.Y].State !=
                        board[startPosition.X, startPosition.Y - 1].State
                        && board[startPosition.X, startPosition.Y - 2] == null)
                    {
                        return true;
                    }
                }

                // If Piece is close to the TOP and BOTTOM ROWS 
                if (startPosition.Y == 1 ||
                    startPosition.Y == board.GetLength(1) - 2)
                {
                    // If close to TOP ROW, check above to MOVE
                    if (startPosition.Y == 1)
                    {
                        // If close to TOP ROW, check Above
                        if (board[startPosition.X, startPosition.Y - 1] == null)
                            return true;
                    }

                    // If close to BOTTOM ROW, check Bellow to MOVE
                    // If close to BOTTOM ROW, check Bellow
                    else if (startPosition.Y == board.GetLength(1) - 2 && board[startPosition.X, startPosition.Y + 1] == null)
                    {
                            return true;
                    }

                    // Check center area
                    if (board[1, 2] == null)
                            return true;
                }
            }

            // Piece is in LEFT COLUMN
            if (startPosition.X == 0)
            {
                const int vectX = 1;
                const int vectY = 0;

                // Piece to the RIGHT is empty
                if (board[startPosition.X + 1, startPosition.Y] == null ||
                    board[startPosition.X + vectX, startPosition.Y + vectY] == null)
                {
                    return true;
                }

                // Check if EATING is possible
                if (board[startPosition.X, startPosition.Y].State !=
                    board[startPosition.X + 1, startPosition.Y].State &&
                    board[startPosition.X + 2, startPosition.Y] == null)
                {
                    return true;
                }
            }

            // Piece is in RIGHT COLUM
            else if (startPosition.X == board.GetLength(0) - 1)
            {
                const int vectX = -1;
                const int vectY = 0;

                // Piece to the LEFT is empty
                if (board[startPosition.X - 1, startPosition.Y] == null ||
                    board[startPosition.X + vectX, startPosition.Y + vectY] == null)
                {
                    return true;
                }

                // Check if EATING is possible
                if (board[startPosition.X, startPosition.Y].State !=
                    board[startPosition.X - 1, startPosition.Y].State &&
                    board[startPosition.X - 2, startPosition.Y] == null)
                {
                    return true;
                }
            }

            // If piece is in a position that can move diagonally
            if ((startPosition.X == 0 || startPosition.X == 2) &&
                (startPosition.Y == 0 || startPosition.Y == 1 ||
                startPosition.Y == 3 || startPosition.Y == 4))
            {
                // Variables to define diagonal movement
                int vectX, vectY;

                if (startPosition.X == 0)
                {
                    if (startPosition.Y == 0 || startPosition.Y == 4)
                    {
                        vectX = 0;
                    }
                    else
                    {
                        vectX = 1;
                    }
                }
                else
                {
                    if (startPosition.Y == 0 || startPosition.Y == 4)
                    {
                        vectX = 0;
                    }
                    else
                    {
                        vectX = -1;
                    }
                }

                if (startPosition.Y == 0 || startPosition.Y == 1)
                    vectY = 1;
                else
                    vectY = -1;

                if (board[startPosition.X + vectX, startPosition.Y + vectY] == null)
                    return true;

                if (board[startPosition.X, startPosition.Y].State !=
                        board[startPosition.X + vectX, startPosition.Y + vectY].State)
                {
                    if (startPosition.X == 0 && (startPosition.Y == 0 || startPosition.Y == 4) &&
                        board[startPosition.X + 1, startPosition.Y + (vectY * 2)] == null)
                    {
                        return true;
                    }
                    else if (startPosition.X == 2 && (startPosition.Y == 0 ||
                        startPosition.Y == 4) &&
                        board[startPosition.X - 1, startPosition.Y + (vectY * 2)] == null)
                    {
                        return true;
                    }

                    if (board[startPosition.X + (vectX * 2), startPosition.Y + (vectY * 2)] == null)
                        return true;
                }
            }

            // Piece is on TOP ROW & Piece bellow is empty
            if (startPosition.Y == 0 &&
                board[startPosition.X, startPosition.Y + 1] == null)
            {
                return true;
            }

            // Piece is on BOTTOM ROW & Piece above is empty
            else if (startPosition.Y == board.GetLength(1) - 1 &&
                board[startPosition.X, startPosition.Y - 1] == null)
            {
                return true;
            }

            return false;
        }

        private void RemovePiece(Piece position)
        {
            if (position.State == State.Black)
            {
                for (int i = 0; i < blackPieceSet.Length; i++)
                {
                    if (blackPieceSet[i] == position)
                    {
                        blackPieceSet[i] = null;
                    }
                }
            }
            else
            {
                for (int i = 0; i < whitePieceSet.Length; i++)
                {
                    if (whitePieceSet[i] == position)
                    {
                        whitePieceSet[i] = null;
                    }
                }
            }
        }
    }
}