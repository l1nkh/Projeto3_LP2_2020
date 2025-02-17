﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Projeto3_LP2_2020.Common;
using System;

namespace Projeto3_LP2_2020.UnityApp
{
    /// <summary>
    /// This class manipulates the game itself using the 
    /// common library.
    /// </summary>
    public class UnityController : MonoBehaviour
    {
        [SerializeField] private Container container;
        [SerializeField] private GameObject directions;
        [SerializeField] private GameObject pieceSelector;
        [SerializeField] private GameObject WinScreen;

        private TextMeshProUGUI GuideText;
        private int previousPieceNum;
        public bool TurnBlack{get; set;}
        /// <summary>
        /// Serves as the class constructor.
        /// </summary>
        private void Start()
        {
            GuideText = GameObject.Find("Guide").GetComponent<TextMeshProUGUI>();
            WriteTurn();
        }
        /// <summary>
        /// Writes the current player turm.
        /// </summary>
        private void WriteTurn()
        {
            if (TurnBlack)
            {
                GuideText.text = "Player B is Your Turn";
            }
            else
                GuideText.text = "Player W is Your Turn";
        }
        /// <summary>
        /// Sees if the piece selected is valid.
        /// </summary>
        /// <param name="piece"></param>
        public void CheckPiece(Button piece)
        {
            int pieceNum = Convert.ToInt32(piece.transform.GetChild(0).name);

            // Calls 'Common' method checking piece's Status
            if (container.GameManager.IsPieceAvailable(pieceNum, TurnBlack))
            {
                GuideText.text = "Choose a Direction";
                previousPieceNum = pieceNum;
                directions.SetActive(true); 
                pieceSelector.SetActive(false);
            }
            else
            {
                GuideText.text = "Not a valid Piece";
            }
        }
        /// <summary>
        /// Sees if the direction inputed is valid.
        /// </summary>
        /// <param name="pieceDirectionNum"></param>
        public void CheckForDirection(int pieceDirectionNum)
        {
            bool valid = true;
            // Calls 'Common' method checking if the wanted direction from the 
            // selected piece's position is valid
            // If valid, call 'Common' method to transform its position 
            // according to direction
            if (container.GameManager.CheckDirection(previousPieceNum, TurnBlack, pieceDirectionNum))
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
        /// <summary>
        /// Verifies if there is a winner.
        /// </summary>
        /// <returns></returns>
        private bool CheckForWin()
        {
            // Calls 'Common' method checking if there is a win
            if (container.GameManager.CheckForWin(TurnBlack))
            {
                WinScreen.SetActive(true);
                GameObject.Find("GameScreen").SetActive(false);
                if (TurnBlack)
                {
                    WinScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                        ">>> Game won by B player <<<";
                }
                else
                {
                    WinScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                        ">>> Game won by W player <<<";
                }
                return true;
            }
            // If valid, call 'Common' method announcing winner
            return false;
        }
        /// <summary>
        /// Ends the program.
        /// </summary>
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