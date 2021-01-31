namespace Projeto3_LP2_2020.Common
{
    /// <summary>
    /// Enumeration with the possible game states.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Game is in Menu.
        /// </summary>
        Menu,

        /// <summary>
        /// Game is selecting with player plays first.
        /// </summary>
        TurnSelection,

        /// <summary>
        /// Game is selecting a piece.
        /// </summary>
        SelectPiece,

        /// <summary>
        /// Game is selecting a direction.
        /// </summary>
        SelectDirection,

        /// <summary>
        /// The game as ended with a victorious player.
        /// </summary>
        VictoryScreen,
    }
}
