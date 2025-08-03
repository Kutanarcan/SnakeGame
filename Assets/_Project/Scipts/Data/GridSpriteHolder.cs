using UnityEngine;

namespace Snake.Runtime.CSharp
{
    [System.Serializable]
    public class GridSpriteHolder
    {
        public Sprite emptyCell;
        public Sprite snakeHead;
        public Sprite snakeBody;
        public Sprite snakeTail;
        public Sprite food;
    }
}