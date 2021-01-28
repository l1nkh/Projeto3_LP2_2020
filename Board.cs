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
        private Piece[,] board;
        private Piece[] blackPieceSet;
        private Piece[] whitePieceSet;

        /// <summary>
        /// Create our board that creates it with the correct space.
        /// </summary>
        public Board(Piece[,] boardArray,
                     Piece[] blackPieceSet, Piece[] whitePieceSet)
        {
            // Create "board" with correct dimensions
            board = boardArray;
            // Create sets of pieces for each player/color
            this.blackPieceSet = blackPieceSet;
            this.whitePieceSet = whitePieceSet;
        }

        /// <summary>
        /// Checks if a piece as possible spaces to move to.
        /// </summary>
        /// <param name="serialNumber">Number identifying a specific piece in
        /// the player's set.</param>
        /// <param name="turnBlack">Shows which player's turn it is.</param>
        /// <returns>Boolean, true if the piece has spaces it can move to, false
        /// if not.</returns>
        public bool CanMove(int serialNumber, bool turnBlack)
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
        /// <returns>Pos of the requested piece</returns>
        private Position GetPosition(int serialNumber, bool turnBlack)
        {
            if (turnBlack)
                return blackPieceSet[serialNumber].Pos;
            else
                return whitePieceSet[serialNumber].Pos;
        }

        /// <summary>
        /// Search the spaces around a piece to see if any is null or if any
        /// can be eaten (if they are pieces of the adversary)
        /// </summary>
        /// <param name="startPosition">Pos of the piece searching for
        /// space</param>
        /// <returns>Boolean, true if there is any free space, false if
        /// not</returns>
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
                    else if(c == 1)
                    {
                        if (turnBlack)
                        {
                            if ((board[startPosition.X, startPosition.Y -1].State ==
                                State.White &&
                                board[startPosition.X, startPosition.Y -2] == null) ||
                                (board[startPosition.X, startPosition.Y +1].State ==
                                State.White &&
                                board[startPosition.X, startPosition.Y +2] == null))
                            {
                                return true;
                            }
                        }
                        else if (!turnBlack)
                        {
                            if ((board[startPosition.X, startPosition.Y -1].State ==
                                State.Black &&
                                board[startPosition.X, startPosition.Y -2] == null) ||
                                (board[startPosition.X, startPosition.Y +1].State ==
                                State.Black &&
                                board[startPosition.X, startPosition.Y +2] == null))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            // Piece is in CENTER COLUMN of the board
            // (but is not in center of the board)
            else if (startPosition.X == 1 && startPosition.Y != 2)
            {
                // Check Left and Right
                if(board[startPosition.X -1, startPosition.Y] == null ||
                    board[startPosition.X +1, startPosition.Y] == null)
                    return true;

                // if Above CENTER, check Bellow (and check to EAT)
                if (startPosition.Y < 2)
                {
                    // Piece BELLOW is empty
                    if (board[startPosition.X, startPosition.Y +1] == null)
                    {
                        return true;
                    }
                    else if (turnBlack)
                    {
                        if (board[startPosition.X, startPosition.Y +1].State ==
                            State.White &&
                            board[startPosition.X +2, startPosition.Y +2] == null)
                            return true;
                    }
                    else if (!turnBlack)
                    {
                        if (board[startPosition.X +1, startPosition.Y +1].State ==
                            State.Black &&
                            board[startPosition.X +2, startPosition.Y +2] == null)
                            return true;
                    }
                }

                // if Bellow CENTER, check Above (and check to EAT)
                if (startPosition.Y > 2)
                {
                    // Piece ABOVE is empty
                    if (board[startPosition.X, startPosition.Y -1] == null)
                    {
                        return true;
                    }
                    else if (turnBlack)
                    {
                        if (board[startPosition.X, startPosition.Y -1].State ==
                            State.White &&
                            board[startPosition.X +2, startPosition.Y -2] == null)
                            return true;
                    }
                    else if (!turnBlack)
                    {
                        if (board[startPosition.X +1, startPosition.Y -1].State ==
                            State.Black &&
                            board[startPosition.X +2, startPosition.Y -2] == null)
                            return true;
                    }
                }

                // If Piece is close to the TOP and BOTTOM ROWS 
                if (startPosition.Y == 1 ||
                    startPosition.Y == board.GetLength(1) -2)
                {
                    // If close to TOP ROW, check above to MOVE
                    if (startPosition.Y == 1)
                    {
                        // If close to TOP ROW, check Above
                        if (board[startPosition.X, startPosition.Y -1] == null)
                            return true;
                    }

                    // If close to BOTTOM ROW, check Bellow to MOVE
                    else if (startPosition.Y == board.GetLength(1) -2)
                    {
                        // If close to BOTTOM ROW, check Bellow
                        if (board[startPosition.X, startPosition.Y +1] == null)
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
                // Piece to the RIGHT is empty
                if (board[startPosition.X +1, startPosition.Y] == null)
                {
                    return true;
                }

                // Check if EATING is possible
                else if (turnBlack)
                {
                    if (board[startPosition.X +1, startPosition.Y].State ==
                        State.White &&
                        board[startPosition.X +2, startPosition.Y] == null)
                        return true;
                }
                else if (!turnBlack)
                {
                    if (board[startPosition.X +1, startPosition.Y].State ==
                        State.Black &&
                        board[startPosition.X +2, startPosition.Y] == null)
                        return true;
                }
            }

            // Piece is in RIGHT COLUM
            else if (startPosition.X == board.GetLength(0) -1)
            {
                // Piece to the LEFT is empty
                if (board[startPosition.X -1, startPosition.Y] == null)
                {
                    return true;
                }

                // Check if EATING is possible
                else if (turnBlack)
                {
                    if (board[startPosition.X -1, startPosition.Y].State ==
                        State.White &&
                        board[startPosition.X -2, startPosition.Y] == null)
                        return true;
                }
                else if (!turnBlack)
                {
                    if (board[startPosition.X -1, startPosition.Y].State ==
                        State.Black &&
                        board[startPosition.X -2, startPosition.Y] == null)
                        return true;
                }
            }

            // If piece is in a position that can move diagonally
            if ((startPosition.X == 0 || startPosition.X == 2) &&
                (startPosition.Y == 1 || startPosition.Y == 3))
            {
                // Variables to define diagonal movement
                int vectX, vectY;

                if (startPosition.X == 0)
                    vectX = 1;
                else
                    vectX = -1;
                if (startPosition.Y == 1)
                    vectY = -1;
                else
                    vectY = 1;

                if (board[startPosition.X + vectX, startPosition.Y + vectY] == null)
                    return true;

                if (turnBlack)
                {
                    if (
                        board[startPosition.X + vectX, startPosition.Y + vectY].State ==
                        State.White &&
                        board[startPosition.X + vectX*2, startPosition.Y + vectY*2] ==
                        null)
                        return true;
                }
                else if (!turnBlack)
                {
                    if (
                        board[startPosition.X + vectX, startPosition.Y + vectY].State ==
                        State.Black &&
                        board[startPosition.X + vectX*2, startPosition.Y + vectY*2] ==
                        null)
                        return true;
                }
            }

            // Piece is on TOP ROW & Piece bellow is empty
            if (startPosition.Y == 0 &&
                board[startPosition.X, startPosition.Y +1] == null)
            {
                return true;
            }

            // Piece is on BOTTOM ROW & Piece above is empty
            else if (startPosition.Y == board.GetLength(1) -1 &&
                board[startPosition.X, startPosition.Y -1] == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// NEEDS TO GO <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        /// </summary>
        /// <param name="pos">Position to check.</param>
        /// <returns>True if it can, false if it can't.</returns>
        private bool IsPieceInCenter(Position pos)
        {
            // Center is (2, 1).
            if ((pos.X == 2 && pos.Y == 1) || (pos.X == 1 && pos.Y == 0) ||
                (pos.X == 1 && pos.Y == 2) || (pos.X == 3 && pos.Y == 0) ||
                (pos.X == 3 && pos.Y == 2))
                return true;
            else
                return false;
        }

        /// <summary>
        /// IS PROBABLY GOING <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        /// </summary>
        /// <param name="selectedPiece">Piece that we are moving.</param>
        /// <returns>Destination position.</returns>
        public Position Move(Piece selectedPiece)
        {
            // The converted input.
            int convertedAux = 0;

            // Boolean to check if the output convertion worked..
            bool convertSuccesful = false;

            // Invalid position to say that it was a failed move.
            Position blockedPosition = new Position(0, 2);

            // Array of possible destinations.
            Position[] destinations = new Position[8];

            // Add our possible positions.
            // Upper Middle.
            destinations[0] = new Position(
                selectedPiece.Pos.X - 1, selectedPiece.Pos.Y);
            // Middle Left.
            destinations[1] = new Position(
                selectedPiece.Pos.X, selectedPiece.Pos.Y - 1);
            // Middle Right.
            destinations[2] = new Position(
                selectedPiece.Pos.X, selectedPiece.Pos.Y + 1);
            // Lower Middle.
            destinations[3] = new Position(
                selectedPiece.Pos.X + 1, selectedPiece.Pos.Y);

            // If the piece is in the center, add the diagonal options.
            if (IsPieceInCenter(selectedPiece.Pos))
            {
                // Lower Right.
                destinations[4] = new Position(
                    selectedPiece.Pos.X + 1, selectedPiece.Pos.Y + 1);

                // Upper Right.
                destinations[5] = new Position(
                    selectedPiece.Pos.X - 1, selectedPiece.Pos.Y + 1);

                // Lower Left.
                destinations[6] = new Position(
                    selectedPiece.Pos.X + 1, selectedPiece.Pos.Y - 1);

                // Upper Left.
                destinations[7] = new Position(
                    selectedPiece.Pos.X - 1, selectedPiece.Pos.Y - 1);
            }

            /*Console.WriteLine("Which direction?");

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
            }*/

            //CheckDestinations();

            // Adjust for our ForLoop.
            convertedAux--;

            // Run through all destinations.
            for(int i = 0; i <= destinations.Length; i++)
            {
                // Until we find our input.
                if(i == convertedAux)
                {
                    // Check if its not out of bounds and is occupied.
                    if(!IsOutOfBounds(destinations[i])
                        && IsOccupied(destinations[i]))
                    {
                        // Check if we can eat to see if we can jump over it.
                        if (CanEat(destinations[i]))
                        {
                            return JumpPosition(convertedAux, destinations[i]);
                        }
                        // Else, return the normal destination.
                        else
                        {
                            return destinations[i];
                        }
                    }
                    else
                    {
                        return destinations[i];
                    }
                }
            }
            return blockedPosition;
        }

        /// <summary>
        /// Returns the position after jumping and also sets the position
        /// found to null since we already got confirmation that we can make
        /// this jump.
        /// </summary>
        /// <param name="option">The option inserted.</param>
        /// <param name="position">The position to set to null.</param>
        /// <returns>Position after jumping over a piece.</returns>
        private Position JumpPosition(int option, Position position)
        {
            board[position.X, position.Y] = null;

            switch (option)
            {
                case 0:
                    return new Position(position.X - 1, position.Y);
                case 1:
                    return new Position(position.X, position.Y - 1);
                case 2:
                    return new Position(position.X, position.Y + 1);
                case 3:
                    return new Position(position.X + 1, position.Y);
                case 4:
                    return new Position(position.X + 1, position.Y + 1);
                case 5:
                    return new Position(position.X - 1, position.Y + 1);
                case 6:
                    return new Position(position.X + 1, position.Y - 1);
                default:
                    return new Position(position.X - 1, position.Y - 1);
            }
        }

        /// <summary>
        /// Method to change a piece's current position and set the origin
        /// to null.
        /// </summary>
        /// <param name="piece">Piece we are moving.</param>
        /// <param name="position">Position destination.</param>
        private void MovePiece(Piece piece, Position position)
        {
            // Place the piece in the desired location.
            board[position.X, position.Y] = piece;

            // Remove the origin.
            board[piece.Pos.X, piece.Pos.Y] = null;

            // Update our piece's position.
            piece.Pos = position;
        }

        /// <summary>
        /// Get a piece at a position.
        /// </summary>
        /// <param name="position">Position to check.</param>
        /// <returns>Piece.</returns>
        public Piece GetPiece(Position position)
        {
            return board[position.X, position.Y];
        }

        /// <summary>
        /// Checks if the position is out of the board's bounds.
        /// </summary>
        /// <param name="pos">Position to check.</param>
        /// <returns>True if its out of bounds, false otherwise.</returns>
        private bool IsOutOfBounds(Position pos)
        {
            return pos.Y > board.GetLength(1) - 1 || pos.Y < 0 ||
                    pos.X > board.GetLength(0) - 1 || pos.X < 0;
        }

        /// <summary>
        /// Method that returns if a board position is null or not.
        /// </summary>
        /// <param name="pos">Position to check.</param>
        /// <returns>True or false.</returns>
        public bool IsOccupied(Position pos)
        {
            return board[pos.X, pos.Y] != null;
        }

        /// <summary>
        /// Bool to check if it can move to a location.
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        private bool CanMoveToLocation(Position destination)
        {
            // Checks if its not out of bounds first.
            if(!IsOutOfBounds(destination))
            {
                // Then checks if its not occupied.
                if(!IsOccupied(destination))
                {
                    return true;
                }
                else
                {
                    // Checks if it can be eaten as well.
                    return CanEat(destination);
                }
            }
            return false;
        }

        /// <summary>
        /// Method that checks if the piece in the position sent can be eaten.
        /// It checks all around the position to see if it has any available
        /// spots past the direction in which we are going.
        /// </summary>
        /// <param name="currentPos">Position to check for eating.</param>
        /// <returns>True if it can be eaten or false otherwise.</returns>
        private bool CanEat(Position currentPos)
        {
            // Create the possible positions.
            Position[] possiblePositions = new Position[8];

            // Add our possible positions.

            // Upper Left.
            possiblePositions[0] = new Position(
                currentPos.X - 1, currentPos.Y - 1);

            // Upper Middle.
            possiblePositions[1] = new Position(
                currentPos.X - 1, currentPos.Y);

            // Upper Right.
            possiblePositions[2] = new Position(
                currentPos.X - 1, currentPos.Y + 1);

            // Lower right.
            possiblePositions[3] = new Position(
                currentPos.X + 1, currentPos.Y + 1);

            // Lower Middle.
            possiblePositions[4] = new Position(
                currentPos.X + 1, currentPos.Y);

            // Lower Left.
            possiblePositions[5] = new Position(
                currentPos.X + 1, currentPos.Y - 1);

            // Middle Left.
            possiblePositions[6] = new Position(
                currentPos.X, currentPos.Y - 1);

            // Middle Right.
            possiblePositions[7] = new Position(
                currentPos.X, currentPos.Y + 1);

            // Run through every possible position.
            for (int i = 0; i < possiblePositions.Length; i++)
            {
                // Check if its out of bounds first before diving deeper.
                if(!IsOutOfBounds(possiblePositions[i]))
                {
                    // Check if the piece acquired is occupied and the one we
                    // are checking is also occupied.
                    if (GetPiece(currentPos) != null
                        && GetPiece(possiblePositions[i])!= null)
                    {
                        // After this, check if its White or Black, to then
                        // check the piece in the possible position is the
                        // opponent's state.
                        //
                        // After that, just check if the position beyond is
                        // free to allow movement.
                        if (GetPiece(currentPos).State == State.White)
                        {
                            if (GetPiece(possiblePositions[i]).State == State.Black)
                            {
                                if (i == 0)
                                    if (!IsOutOfBounds(possiblePositions[3]))
                                        if (!IsOccupied(possiblePositions[3]))
                                        return true;
                                if (i == 1)
                                    if (!IsOutOfBounds(possiblePositions[4]))
                                        if (!IsOccupied(possiblePositions[4]))
                                        return true;
                                if (i == 2)
                                    if (!IsOutOfBounds(possiblePositions[5]))
                                        if (!IsOccupied(possiblePositions[5]))
                                        return true;
                                if (i == 3)
                                    if (!IsOutOfBounds(possiblePositions[0]))
                                        if (!IsOccupied(possiblePositions[0]))
                                        return true;
                                if (i == 4)
                                    if (!IsOutOfBounds(possiblePositions[1]))
                                        if (!IsOccupied(possiblePositions[1]))
                                        return true;
                                if (i == 5)
                                    if (!IsOutOfBounds(possiblePositions[2]))
                                        if (!IsOccupied(possiblePositions[2]))
                                        return true;
                                if (i == 6)
                                    if (!IsOutOfBounds(possiblePositions[7]))
                                        if (!IsOccupied(possiblePositions[7]))
                                        return true;
                                if (i == 7)
                                    if (!IsOutOfBounds(possiblePositions[6]))
                                        if (!IsOccupied(possiblePositions[6]))
                                        return true;
                            }
                        }
                        else if (GetPiece(currentPos).State == State.Black)
                        {
                            if(GetPiece(possiblePositions[i]).State == State.White)
                            {
                                if (i == 0)
                                    if (!IsOutOfBounds(possiblePositions[3]))
                                        if (!IsOccupied(possiblePositions[3]))
                                            return true;
                                if (i == 1)
                                    if (!IsOutOfBounds(possiblePositions[5]))
                                        if (!IsOccupied(possiblePositions[4]))
                                            return true;
                                if (i == 2)
                                    if (!IsOutOfBounds(possiblePositions[5]))
                                        if (!IsOccupied(possiblePositions[5]))
                                            return true;
                                if (i == 3)
                                    if (!IsOutOfBounds(possiblePositions[0]))
                                        if (!IsOccupied(possiblePositions[0]))
                                            return true;
                                if (i == 4)
                                    if (!IsOutOfBounds(possiblePositions[1]))
                                        if (!IsOccupied(possiblePositions[1]))
                                            return true;
                                if (i == 5)
                                    if (!IsOutOfBounds(possiblePositions[2]))
                                        if (!IsOccupied(possiblePositions[2]))
                                            return true;
                                if (i == 6)
                                    if (!IsOutOfBounds(possiblePositions[7]))
                                        if (!IsOccupied(possiblePositions[7]))
                                            return true;
                                if (i == 7)
                                    if (!IsOutOfBounds(possiblePositions[6]))
                                        if (!IsOccupied(possiblePositions[6]))
                                            return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}