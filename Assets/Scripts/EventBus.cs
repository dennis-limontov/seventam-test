using System;

namespace SevenTam
{
    public static class EventBus
    {
        public static Action<FigureType> OnFigureClicked;
    }
}