using UnityEngine;

namespace SevenTam
{
    public class FigureFreezing : MonoBehaviour
    {
        private const string FREEZING_NAME = "Freezing";
        private const int FREEZING_SPRITE_SORTING_ORDER = 3;

        private GameObject _freezing;

        private Figure _figure;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            EventBus.OnDefrostingCounterChanged += DefrostingCounterChangedHandler;
            _figure = GetComponent<Figure>();
            _figure.enabled = false;
        }

        private void OnDestroy()
        {
            EventBus.OnDefrostingCounterChanged -= DefrostingCounterChangedHandler;
            _figure.enabled = true;
        }

        private void DefrostingCounterChangedHandler(int counter)
        {
            if (counter == 0)
            {
                Destroy(_freezing);
                Destroy(this);
            }
        }

        public void SetFreezing(Sprite freezingSprite, float scale)
        {
            _freezing = new GameObject(FREEZING_NAME, typeof(SpriteRenderer));
            _freezing.transform.SetParent(transform, false);
            _freezing.transform.localScale = Vector3.one * scale;
            _spriteRenderer = _freezing.GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = freezingSprite;
            _spriteRenderer.sortingOrder = FREEZING_SPRITE_SORTING_ORDER;
        }
    }
}