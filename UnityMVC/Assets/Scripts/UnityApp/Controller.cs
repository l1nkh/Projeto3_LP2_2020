using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.UnityApp
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Container container;
        public bool TurnBlack{get; set;}

        /*public bool CheckPiece(int pieceNum, bool turnBlack)
        {
            // Calls 'Common' method checking piece's Status
            return container.Player.IsPieceAvailable(turnBlack, pieceNum);
        }*/

        public bool CheckForDirection(int pieceNum, bool turnBlack, int directionNumber)
        {
            bool validDirection = false;
            // Calls 'Common' method checking if the wanted direction from the 
            // selected piece's position is valid
                // If valid, call 'Common' method to transform its position 
                // according to direction
                // validDirection = true;
            return validDirection;
        }

        public bool CheckForWin()
        {
            bool gameWon = false;
            // Calls 'Common' method checking if there is a win
                // If valid, call 'Common' method announcing winner
            return gameWon;
        }

        public void Quit()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}