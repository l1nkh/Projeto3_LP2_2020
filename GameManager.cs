namespace Projeto3_LP2_2020.Common
{
    /// <summary>
    /// Class that communicates with MVC components, works as a
    /// façade for Common
    /// </summary>
    public class GameManager
    {
        private Piece[,] boardArray;
        private Piece[] blackPieceSet;
        private Piece[] whitePieceSet;
        private Board board;
        public Piece[,] BoardArray {get => boardArray;}

        /// <summary>
        /// Create the collections of Pieces that will be shared with the Board.
        /// </summary>
        public GameManager()
        {
            // Set the dimensions for the boardArray and the pieceSets
            boardArray = new Piece[3, 5];
            blackPieceSet = new Piece[6];
            whitePieceSet = new Piece[6];

            // Assign the appropriate values for each piece in boardArray and 
            // fill both PieceSets
            board.AssignStates();

            board = new Board (boardArray, blackPieceSet, whitePieceSet);
        }

        /// <summary>
        /// Checks if the requested piece is alive and has possible movement.
        /// </summary>
        /// <param name="serialNumber">Number identifying a specific piece in
        /// the player's set.</param>
        /// <param name="turnBlack">Shows which player's turn it is.</param>
        /// <returns>Boolean, true if the piece is available to move, false
        /// if not.</returns>
        public bool IsPieceAvailable(int serialNumber, bool turnBlack)
        {
            if (turnBlack)
            {
                // If piece is alive, see if it is surrounded
                if (blackPieceSet[serialNumber].IsAlive)
                    return board.HasMove(serialNumber, turnBlack);
            }
            else
            {
                // If piece is alive, see if it is surrounded
                if (whitePieceSet[serialNumber].IsAlive)
                    return board.HasMove(serialNumber, turnBlack);
            }

            return false;
        }

        public bool CheckDirection(int serialNumber, bool turnBlack, int direction)
        {
            Position pos = board.GetPosition(serialNumber, turnBlack);

            // Check if the direction chosen is immediately invalid due to the
            // piece's position
            if ((pos.X == 0 && (direction == 1 || direction == 4)) ||
                (pos.X == 2 && (direction == 3 || direction == 6)) ||
                (pos.Y == 0 && (direction == 1 || direction == 2 || direction == 3)) ||
                (pos.Y == 1 && pos.X == 0 && (direction == 4 || direction == 5)) ||
                (pos.Y == 1 && pos.X == 2 && (direction == 5 || direction == 6)) ||
                (pos.Y == 1 && pos.X == 1 && (direction == 1 || direction == 3 || direction == 4|| direction == 6)) ||
                (pos.Y == 3 && pos.X == 0 && (direction == 1 || direction == 2)) ||
                (pos.Y == 3 && pos.X == 2 && (direction == 2 || direction == 3)) ||
                (pos.Y == 3 && pos.X == 1 && (direction == 1 || direction == 3 || direction == 4|| direction == 6)) ||
                (pos.Y == 4 && (direction == 4 || direction == 5 || direction == 6)))
            {
                return false;
            }
            else
            {
                return board.CanDoMove(pos, direction);
            }
        }

        /// <summary>
        /// Checks if the current player's adversary's pieces are all dead.
        /// </summary>
        /// <param name="turnBlack">Shows which player's turn it is.</param>
        /// <returns>Boolean, true if every adversary piece is dead, false
        /// if not.</returns>
        public bool CheckForWin(bool turnBlack)
        {
            // If its the 'Black' player's turn,
            // check the 'White' player's pieces
            if (turnBlack)
            {
                // If any piece is alive, then the game is not over.
                foreach (Piece piece in whitePieceSet)
                {
                    if (piece.IsAlive)
                        return false;
                }
            }
            // If its the 'White' player's turn,
            // check the 'Black' player's pieces
            if (!turnBlack)
            {
                // If any piece is alive, then the game is not over.
                foreach (Piece piece in blackPieceSet)
                {
                    if (piece.IsAlive)
                        return false;
                }
            }
            return true;
        }
    }
}