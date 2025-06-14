using UnityEngine;
using UnityEngine.UI;

namespace SevenTam
{
    public class FigureUI : MonoBehaviour
    {
        [SerializeField]
        private Image _animalView;

        [SerializeField]
        private Image _shapeView;

        private FigureType _figureType;

        public void UpdateShape(FigureType figureType)
        {
            _animalView.sprite = figureType.animalSprite;
            _shapeView.sprite = figureType.shapeSprite;
            _shapeView.color = figureType.shapeColor;
            _figureType = figureType;
        }
    }
}