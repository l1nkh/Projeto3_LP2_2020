using System;
using Projeto3_LP2_2020.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Projeto3_LP2_2020.UnityApp
{
    public class View : MonoBehaviour
    {
        [SerializeField] private Container container;
        [SerializeField] private Controller controller;
        public void UpdatePiece(Button button)
        {
            string a = button.transform.GetChild(1).name;
            char[] b = a.ToCharArray();
            int buttonIdentifier = container.GameManager.BoardArray[Convert.ToInt32(b[0]), Convert.ToInt32(b[1])].serialNumber + 1;

            if (!container.GameManager.BoardArray[Convert.ToInt32(b[0]), Convert.ToInt32(b[1])].IsAlive)
            {
                button.GetComponent<Text>().text = " ";
            }
            else if (controller.TurnBlack)
            {
                button.GetComponent<Text>().text = 'B' + $"{buttonIdentifier}";
            }
            else
                button.GetComponent<Text>().text = 'W' + $"{buttonIdentifier}";
        }
    }
}