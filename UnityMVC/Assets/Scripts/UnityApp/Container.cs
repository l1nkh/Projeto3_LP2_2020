using UnityEngine;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.UnityApp
{
    [CreateAssetMenu(menuName = "Container")]
    public class Container : ScriptableObject
    {
        //meter aqui todas as classes que precisar mos para o viewer e controler
        public GameManager GameManager { get; private set; }

        public GameState GameState { get; private set; }

        private void OnEnable()
        {
            GameManager = new GameManager();
        }
    }
}   