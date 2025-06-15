using System;

namespace SevenTam
{
    public static class EventBus
    {
        public static Action OnEqualFiguresCollected;
        public static Action<Figure> OnFigureClicked;
        public static Action<FigureType> OnFigureTypeClicked;
        public static Action<bool> OnGameEnded;
        public static Action OnRestartClicked;
    }
}