using UnityEngine;
using UnityEngine.EventSystems;

namespace SevenTam
{
    public class Figure : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private SpriteRenderer _animalView;

        private SpriteRenderer _shapeView;

        private PolygonCollider2D _polygonCollider;

        public FigureType FigureType { get; private set; }

        public void UpdateShape(FigureType figureType)
        {
            _animalView.sprite = figureType.animalSprite;
            _shapeView = gameObject.AddComponent<SpriteRenderer>();
            _shapeView.sprite = figureType.shapeSprite;
            _shapeView.color = figureType.shapeColor;
            _polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
            FigureType = figureType;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventBus.OnFigureClicked?.Invoke(this);
            EventBus.OnFigureTypeClicked?.Invoke(FigureType);
        }
    }
}