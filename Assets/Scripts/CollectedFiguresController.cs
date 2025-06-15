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
        
        [SerializeField]
        private FigureUI _figureUIPrefab;

        private List<FigureUI> _collectedFigures = new List<FigureUI>();

        private FigureUI _figureUICrutch;

        private void OnDestroy()
        {
            EventBus.OnFigureTypeClicked -= FigureClickedHandler;
        }

        private void Start()
        {
            EventBus.OnFigureTypeClicked += FigureClickedHandler;
        }

        private void FigureClickedHandler(FigureType figureType)
        {
            FigureUI figureUI = Instantiate(_figureUIPrefab, transform);
            figureUI.UpdateShape(figureType);
            _collectedFigures.Add(figureUI);
            var equalFigures = _collectedFigures.Where(x => (x.FigureType == figureUI.FigureType)).ToList();
            if (equalFigures.Count == FIGURES_COUNT_TO_COLLECT)
            {
                EventBus.OnEqualFiguresCollected?.Invoke();
                foreach (var figure in equalFigures)
                {
                    _collectedFigures.Remove(figure);
                    Destroy(figure.gameObject);
                }
            }
            if (_collectedFigures.Count >= FIGURES_MAX_COUNT)
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
    }
}