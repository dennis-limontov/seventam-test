using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SevenTam
{
    public class CollectedFiguresController : MonoBehaviour
    {
        private const int FIGURES_COUNT_TO_COLLECT = 3;
        private const int FIGURES_MAX_COUNT = 7;

        public static CollectedFiguresController Instance { get; private set; }
        
        [SerializeField]
        private FigureUI _figureUIPrefab;

        public List<FigureUI> CollectedFigures { get; private set; } = new List<FigureUI>();

        private FigureUI _figureUICrutch;

        private void OnDestroy()
        {
            EventBus.OnRestartClicked -= RestartClickedHandler;
            EventBus.OnFigureTypeClicked -= FigureClickedHandler;
        }

        private void Start()
        {
            EventBus.OnFigureTypeClicked += FigureClickedHandler;
            EventBus.OnRestartClicked += RestartClickedHandler;

            Instance = this;
        }

        private void FigureClickedHandler(FigureType figureType)
        {
            FigureUI figureUI = Instantiate(_figureUIPrefab, transform);
            figureUI.UpdateShape(figureType);
            CollectedFigures.Add(figureUI);
            var equalFigures = CollectedFigures.Where(x => (x.FigureType == figureUI.FigureType)).ToList();
            if (equalFigures.Count == FIGURES_COUNT_TO_COLLECT)
            {
                EventBus.OnEqualFiguresCollected?.Invoke();
                foreach (var figure in equalFigures)
                {
                    CollectedFigures.Remove(figure);
                    Destroy(figure.gameObject);
                }
            }
            if (CollectedFigures.Count >= FIGURES_MAX_COUNT)
            {
                EventBus.OnGameEnded?.Invoke(false);
            }
            _figureUICrutch = Instantiate(_figureUIPrefab, transform);
            _figureUICrutch.MakeTransparent();
            StartCoroutine(DestroyCrutch());
        }

        private IEnumerator DestroyCrutch()
        {
            yield return null;

            Destroy(_figureUICrutch.gameObject);
        }

        private void RestartClickedHandler()
        {
            foreach (var figureUI in CollectedFigures)
            {
                Destroy(figureUI.gameObject);
            }
            CollectedFigures.Clear();
        }
    }
}