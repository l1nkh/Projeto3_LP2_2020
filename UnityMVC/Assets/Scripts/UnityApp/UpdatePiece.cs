using UnityEngine;
using UnityEngine.UI;

namespace Projeto3_LP2_2020.UnityApp
{
    /// <summary>
    /// this class updates the buttons text every frame.
    /// </summary>
    public class UpdatePiece : MonoBehaviour
    {
        [SerializeField] private UnityView View;

        /// <summary>
        /// Updates the button text.
        /// </summary>
        void FixedUpdate()
        {
            View.UpdatePiece(gameObject.GetComponent<Button>());
        }
    }
}
