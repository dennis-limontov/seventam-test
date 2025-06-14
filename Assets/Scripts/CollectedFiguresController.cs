using UnityEngine;

namespace SevenTam
{
    public class CollectedFiguresController : MonoBehaviour
    {
        [SerializeField]
        private FigureUI _figureUIPrefab;

        private void OnDestroy()
        {
            EventBus.OnFigureClicked -= FigureClickedHandler;
        }

        private void Start()
        {
            EventBus.OnFigureClicked += FigureClickedHandler;
        }

        private void FigureClickedHandler(FigureType figureType)
        {
            FigureUI figureUI = Instantiate(_figureUIPrefab, transform);
            figureUI.UpdateShape(figureType);
        }
    }
}