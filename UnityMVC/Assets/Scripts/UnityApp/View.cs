using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Projeto3_LP2_2020.UnityApp
{
    public class View : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject PieceNumber;
        [SerializeField] private GameObject PieceState;
        [SerializeField] private Controller controller;

        private char buttonChar;

        void Awake()
        {
        }

        void Update()
        {
            switch (PieceState.name)
            {
                case "None":
                    buttonChar = ' ';
                    PieceNumber.name = "";
                    break;
                case "Black":
                    buttonChar = 'B';
                    break;
                case "White":
                    buttonChar = 'W';
                    break;
                default:
                    break;
            }           
        }
        private void FixedUpdate()
        {
            button.GetComponentInChildren<Text>().text = buttonChar + PieceNumber.name;
        }
        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }
    }
}