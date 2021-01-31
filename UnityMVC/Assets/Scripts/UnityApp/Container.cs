using UnityEngine;
using Projeto3_LP2_2020.Common;

namespace Projeto3_LP2_2020.UnityApp
{
    /// <summary>
    /// This class contains the common library.
    /// </summary>
    [CreateAssetMenu(menuName = "Container")]
    public class Container : ScriptableObject
    {
        //meter aqui todas as classes que precisar mos para o viewer e controler
        public GameManager GameManager { get; private set; }

        public GameState GameState { get; private set; }
        /// <summary>
        /// Activates the library using the GameManager
        /// class.
        /// </summary>
        private void OnEnable()
        {
            GameManager = new GameManager();
        }
    }
}   