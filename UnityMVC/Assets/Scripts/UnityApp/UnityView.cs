﻿using System;
using Projeto3_LP2_2020.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Projeto3_LP2_2020.UnityApp
{
    public class UnityView : MonoBehaviour
    {
        [SerializeField] private Container container;

        private int buttonIdentifier = 0;
        public void UpdatePiece(Button button)
        {
            string buttonPos = button.transform.GetChild(1).name;
            string buttonColor = button.transform.GetChild(2).name;
            char[] CharPos = buttonPos.ToCharArray();
            int x = (int)char.GetNumericValue(CharPos[0]);
            int y = (int)char.GetNumericValue(CharPos[1]);

            if (container.GameManager.BoardArray[x, y] != null)
            {
                buttonIdentifier = container.GameManager.BoardArray[x, y].serialNumber + 1;
            }

            if (container.GameManager.BoardArray[x, y] == null)
            {
                button.GetComponentInChildren<Text>().text = " ";
            }
            else if (!container.GameManager.BoardArray[x, y].IsAlive)
            {
                button.GetComponentInChildren<Text>().text = " ";
            }
            else
                button.GetComponentInChildren<Text>().text = buttonColor + $"{buttonIdentifier}";
        }
    }
}