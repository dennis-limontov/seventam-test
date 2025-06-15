using System;
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

        public static bool operator ==(FigureType lhs, FigureType rhs)
        {
            return ((lhs.animalSprite == rhs.animalSprite)
                && (lhs.shapeSprite == rhs.shapeSprite)
                && (lhs.shapeColor == rhs.shapeColor));
        }

        public static bool operator !=(FigureType lhs, FigureType rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FigureType)) return false;
            return this == (FigureType)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(animalSprite, shapeSprite, shapeColor);
        }
    }
}