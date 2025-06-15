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

        public FigureType FigureType { get; private set; }

        public void MakeTransparent()
        {
            _animalView.color = Color.clear;
            _shapeView.color = Color.clear;
        }

        public void UpdateShape(FigureType figureType)
        {
            _animalView.sprite = figureType.animalSprite;
            _shapeView.sprite = figureType.shapeSprite;
            _shapeView.color = figureType.shapeColor;
            FigureType = figureType;
        }
    }
}