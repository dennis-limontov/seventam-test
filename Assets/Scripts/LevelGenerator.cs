using UnityEngine;

namespace SevenTam
{
    public class LevelGenerator : MonoBehaviour
    {
        private const int FIGURE_COUNT_MODIFIER = 3;

        [SerializeField]
        private Sprite[] _animalsSprites;

        [SerializeField]
        private Sprite[] _shapesSprites;

        [SerializeField]
        private Color[] _shapesColors;

        [SerializeField]
        private Figure _figurePrefab;

        [SerializeField]
        private int _figureCount = 20;

        [SerializeField]
        private Transform _figureRespawnPoint;

        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            FigureType figureType;
            for (int i = 0; i < _figureCount; i++)
            {
                figureType = GenerateFigureType();

                for (int j = 0; j < FIGURE_COUNT_MODIFIER; j++)
                {
                    GenerateFigureView(figureType);
                }
            }
        }

        private Figure GenerateFigureView(FigureType figureType)
        {
            Figure figure = Instantiate(_figurePrefab, _figureRespawnPoint);
            figure.UpdateShape(figureType);
            return figure;
        }

        public FigureType GenerateFigureType()
        {
            Sprite animalSprite = _animalsSprites[Random.Range(0, _animalsSprites.Length)];
            Sprite shapeSprite = _shapesSprites[Random.Range(0, _shapesSprites.Length)];
            Color shapeColor = _shapesColors[Random.Range(0, _shapesColors.Length)];
            return new FigureType(animalSprite, shapeSprite, shapeColor);
        }
    }
}