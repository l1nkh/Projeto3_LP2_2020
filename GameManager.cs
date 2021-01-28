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
            AssignStates();

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
            // NEEDS UPDATE <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            // Will have to check if alive and then if not surrounded
            if (turnBlack)
                return blackPieceSet[serialNumber].IsAlive;
            else
                return whitePieceSet[serialNumber].IsAlive;
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

        /// <summary>
        /// Set the initial position of the method. Used in AssignStates().
        /// </summary>
        /// <param name="x">Row.</param>
        /// <param name="y">Column.</param>
        /// <param name="state">White, Black or Blocked.</param>
        /// <returns>Assigns the piece created in the board.</returns>
        private void SetInitialLocation(
                                int x, int y, State state, int serialNumber)
        {
            Piece piece = new Piece(state, serialNumber);
            piece.Pos = new Position(x, y);
            // Add piece to the board in its initial position.
            boardArray[x, y] = piece;

            // Add piece to the appropriate collection
            if (piece.State == State.Black)
                blackPieceSet[serialNumber] = piece;
            else if (piece.State == State.White)
                whitePieceSet[serialNumber] = piece;
        }

        /// <summary>
        /// Method to assign the initial states of the game.
        /// </summary>
        public void AssignStates()
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

            // Center coordinate [1, 2] is free (null)
        }
    }
}