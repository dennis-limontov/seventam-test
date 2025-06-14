using UnityEngine;

namespace SevenTam
{
    public struct FigureType
    {
        public Sprite animalSprite;
        public Sprite shapeSprite;
        public Color shapeColor;

        public FigureType(Sprite animalSprite, Sprite shapeSprite, Color shapeColor)
        {
            this.animalSprite = animalSprite;
            this.shapeSprite = shapeSprite;
            this.shapeColor = shapeColor;
        }
    }
}