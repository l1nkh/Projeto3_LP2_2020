using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.UnityApp
{
    [CreateAssetMenu(menuName = "Container")]
    public class Container : ScriptableObject
    {
        public Board Board {get; private set;}
        public Piece Piece {get; private set;}
        public GameState Gamestate {get; private set;}
        public Player Player {get; private set;}
        public Position Position {get; private set;}
        public State State {get; private set;}
        public WinChecker WinChecker {get; private set;}
        //meter aqui todas as classes que precisar mos para o viewer e controler

        private void ONEnable()
        {
            Board = new Board();
            Player = new Player();
        }
    }
}