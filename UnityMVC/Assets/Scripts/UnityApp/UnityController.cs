using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Projeto3_LP2_2020.Common;
using System;

namespace Projeto3_LP2_2020.UnityApp
{
    public class UnityController : MonoBehaviour
    {
        [SerializeField] private Container container;
        [SerializeField] private GameObject directions;
        [SerializeField] private GameObject pieceSelector;

        private Button previousPiece;
        private TextMeshProUGUI GuideText;
        private int previousPieceNum;
        private string previousPieceState;
        public bool TurnBlack{get; set;}
        
        private void Start()
        {
            GuideText = GameObject.Find("Guide").GetComponent<TextMeshProUGUI>();
            WriteTurn();
        }

        private void WriteTurn()
        {
            if (TurnBlack)
            {
                GuideText.text = "Player B is Your Turn";
            }
            else
                GuideText.text = "Player W is Your Turn";
        }
        public void CheckPiece(Button piece)
        {
            int pieceNum = Convert.ToInt32(piece.transform.GetChild(0).name);
            //string pieceState = piece.transform.GetChild(2).name;

            // Calls 'Common' method checking piece's Status
            if (container.GameManager.IsPieceAvailable(pieceNum, TurnBlack))
            {
                previousPieceNum = pieceNum;
                directions.SetActive(true);
                pieceSelector.SetActive(false);
                //previousPieceState = pieceState;
            }
            else
            {
                GuideText.text = "Not a valid Piece";
            }
            /*if (validPiece)
            {
                if(CheckForDirection(previousPieceNum, pieceNum))
                {
                    previousPiece.transform.GetChild(2).name = pieceState;
                    piece.transform.GetChild(2).name = previousPieceState;

                    previousPiece.transform.GetChild(0).name = $"{pieceNum}";
                    piece.transform.GetChild(0).name = $"{previousPieceNum}";

                    TurnBlack = !TurnBlack;
                    CheckForWin();
                    validPiece = false; 
                }
                else
                {
                    //couldnt move
                    validPiece = false;
                    previousPiece = piece;
                }
            }
            else
            {
                validPiece = CheckPiece(pieceNum, pieceState);
                previousPiece = piece;
            }*/
        }

        public void CheckForDirection(int pieceDirectionNum)
        {
            bool valid = true;
            // Calls 'Common' method checking if the wanted direction from the 
            // selected piece's position is valid
            // If valid, call 'Common' method to transform its position 
            // according to direction
            if (valid/*container.GameManager.IsDirectionAvaiable(previousPieceNum, pieceDirectionNum, TurnBlack)*/)
            {
                directions.SetActive(false);
                pieceSelector.SetActive(true);
                CheckForWin();
                TurnBlack = !TurnBlack;
                WriteTurn();
            }
            else
            {
                GuideText.text = "Not a valid Direction";
                directions.SetActive(false);
                pieceSelector.SetActive(true);
            }
        }
        private bool CheckForWin()
        {
            // Calls 'Common' method checking if there is a win
            if (container.GameManager.CheckForWin(TurnBlack))
            {
                if (TurnBlack)
                {
                    GuideText.text = ">>> Game won by B player <<<";
                }
                else
                    GuideText.text = ">>> Game won by W player <<<";

                return true;
            }
            // If valid, call 'Common' method announcing winner
            return false;
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