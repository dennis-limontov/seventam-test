using UnityEngine;

namespace SevenTam
{
    public class FigureWeight : MonoBehaviour
    {
        private const float FIGURE_WEIGHT_MODIFIER = 2f;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale *= FIGURE_WEIGHT_MODIFIER;
        }
    }
}