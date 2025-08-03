using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public class SnakeGameModel
    {
        public FixedGrid grid;
        public readonly GridSpriteHolder spriteHolder;
        public int snakeStartSize;

        // We can change this array's location
        public readonly DirectionKey[] directionKeys =
        {
            new DirectionKey(Vector2.up, KeyCode.UpArrow),
            new DirectionKey(Vector2.down, KeyCode.DownArrow),
            new DirectionKey(Vector2.left, KeyCode.LeftArrow),
            new DirectionKey(Vector2.right, KeyCode.RightArrow)
        };

        //Get Fixed Grid from the constructor
        public SnakeGameModel(GridSpriteHolder spriteHolder)
        {
            this.spriteHolder = spriteHolder;
        }
    }
}