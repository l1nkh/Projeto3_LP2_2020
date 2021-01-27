namespace Projeto3_LP2_2020.Common
{
    public class GameManager
    {
        private Piece[,] boardArray;
        private Piece[] blackPieceSet;
        private Piece[] whitePieceSet;
        private Board board;
        public GameManager()
        {
            // Set the dimensions for the boardArray and the pieceSets
            boardArray = new Piece[3, 5];
            blackPieceSet = new Piece[6];
            whitePieceSet = new Piece[6];

            board = new Board (boardArray, blackPieceSet, whitePieceSet);
        }

        public bool IsPieceAvailable(int serialNumber, bool turnBlack)
        {
            // NEEDS UPDATE <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            // Will have to check if alive and then if not surrounded
            if (turnBlack)
                return blackPieceSet[serialNumber].IsAlive;
            else
                return whitePieceSet[serialNumber].IsAlive;
        }

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