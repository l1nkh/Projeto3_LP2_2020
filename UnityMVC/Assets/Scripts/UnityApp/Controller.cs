using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Projeto3_LP2_2020.Common;
using System;

namespace Projeto3_LP2_2020.UnityApp
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Container container;
        [SerializeField] private GameObject directions;
        [SerializeField] private GameObject pieceSelector;

        private Button previousPiece;
        private TextMeshProUGUI GuideText;

        private bool validPiece = true;
        private bool validDirection;
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
            //validPiece = container.Player.IsPieceAvailable(TurnBlack, pieceNum);

            if (validPiece)
            {
                previousPieceNum = pieceNum;
                directions.SetActive(true);
                pieceSelector.SetActive(false);
                //previousPieceState = pieceState;
            }
            else
            {
                /*guide.SetActive(true);
                guide.GetComponent<TextMeshProUGUI>().text = "Not a valid Piece";*/
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
        private bool CheckPiece(int pieceNum, string pieceState)
        {
            ;
            // Calls 'Common' method checking piece's Status

            //return container.Player.IsPieceAvailable(TurnBlack, pieceNum);
            return true;

        }

        public void CheckForDirection(int PieceNum)
        {
             validDirection = true;
            // Calls 'Common' method checking if the wanted direction from the 
            // selected piece's position is valid
            // If valid, call 'Common' method to transform its position 
            // according to direction
            // validDirection = true;
            //return validDirection;

            if (validDirection)
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