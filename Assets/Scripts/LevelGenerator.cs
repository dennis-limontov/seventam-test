using System.Collections;
using System.Collections.Generic;
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
        [Range(0f, 1f)]
        private float _figureIsHeavyChance = 0.3f;

        [SerializeField]
        private int _figureCount = 20;

        [SerializeField]
        private Transform _figureRespawnPoint;

        [SerializeField]
        private float _figureGeneratePause = 0.05f;

        private List<Figure> _figures = new List<Figure>();

        private Coroutine _generateCoroutine;

        private void OnDestroy()
        {
            EventBus.OnRestartClicked -= RestartClickedHandler;
            EventBus.OnFigureClicked -= FigureClickedHandler;
            EventBus.OnEqualFiguresCollected -= EqualFiguresCollectedHandler;
        }

        private void Start()
        {
            EventBus.OnEqualFiguresCollected += EqualFiguresCollectedHandler;
            EventBus.OnFigureClicked += FigureClickedHandler;
            EventBus.OnRestartClicked += RestartClickedHandler;

            _generateCoroutine = StartCoroutine(Generate(false));
        }

        private void EqualFiguresCollectedHandler()
        {
            if (_figures.Count == 0)
            {
                EventBus.OnGameEnded?.Invoke(true);
            }
        }

        private void FigureClickedHandler(Figure figure)
        {
            _figures.Remove(figure);
            Destroy(figure.gameObject);
        }

        private void RestartClickedHandler()
        {
            StopCoroutine(_generateCoroutine);
            _generateCoroutine = StartCoroutine(Generate(false));
        }

        public IEnumerator Generate(bool isLevelInProgress)
        {
            FigureType figureType;
            Dictionary<FigureType, int> uniqueCollectedFigures = new Dictionary<FigureType, int>();
            int remainedFiguresCount = _figureCount, figuresCount = _figures.Count;

            foreach (var figure in _figures)
            {
                Destroy(figure.gameObject);
            }
            _figures.Clear();

            if (isLevelInProgress)
            {
                remainedFiguresCount = figuresCount;

                foreach (var collectedFigure in CollectedFiguresController.Instance.CollectedFigures)
                {
                    if (!uniqueCollectedFigures.TryAdd(collectedFigure.FigureType, 1))
                    {
                        uniqueCollectedFigures[collectedFigure.FigureType]++;
                    }
                }

                foreach (var uniqueCollectedFigure in uniqueCollectedFigures)
                {
                    for (int i = 0; i < FIGURE_COUNT_MODIFIER - uniqueCollectedFigure.Value; i++)
                    {
                        _figures.Add(GenerateFigureView(uniqueCollectedFigure.Key));
                        yield return new WaitForSeconds(_figureGeneratePause);
                        remainedFiguresCount--;
                    }
                }

                remainedFiguresCount /= FIGURE_COUNT_MODIFIER;
            }

            for (int i = 0; i < remainedFiguresCount; i++)
            {
                figureType = GenerateFigureType();

                for (int j = 0; j < FIGURE_COUNT_MODIFIER; j++)
                {
                    _figures.Add(GenerateFigureView(figureType));
                    yield return new WaitForSeconds(_figureGeneratePause);
                }
            }
        }

        public FigureType GenerateFigureType()
        {
            Sprite animalSprite = _animalsSprites[Random.Range(0, _animalsSprites.Length)];
            Sprite shapeSprite = _shapesSprites[Random.Range(0, _shapesSprites.Length)];
            Color shapeColor = _shapesColors[Random.Range(0, _shapesColors.Length)];
            return new FigureType(animalSprite, shapeSprite, shapeColor);
        }

        private Figure GenerateFigureView(FigureType figureType)
        {
            Figure figure = Instantiate(_figurePrefab, _figureRespawnPoint);
            figure.UpdateShape(figureType);
            if (_figureIsHeavyChance >= Random.Range(0f, 1f))
            {
                figure.gameObject.AddComponent<FigureWeight>();
            }
            return figure;
        }

        public void OnRefreshClicked()
        {
            StopCoroutine(_generateCoroutine);
            _generateCoroutine = StartCoroutine(Generate(true));
        }
    }
}