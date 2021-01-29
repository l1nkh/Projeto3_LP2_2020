using UnityEngine;
using UnityEngine.UI;

namespace Projeto3_LP2_2020.UnityApp
{
    public class UpdatePiece : MonoBehaviour
    {
        [SerializeField] private UnityView View;

        // Update is called once per frame
        void FixedUpdate()
        {
            View.UpdatePiece(gameObject.GetComponent<Button>());
        }
    }
}
