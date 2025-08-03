using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public readonly struct DirectionKey
    {
        public readonly Vector2 direction;
        public readonly KeyCode key;

        public DirectionKey(Vector2 direction, KeyCode key)
        {
            this.direction = direction;
            this.key = key;
        }
    }
}