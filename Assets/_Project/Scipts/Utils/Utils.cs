using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public static class Utils
    {
        // Create a method that takes a vec2 check it's reversed with this vec2
        public static bool IsReversed(this Vector2 vec, Vector2 other)
        {
            return Mathf.Approximately(vec.x, -other.x) && Mathf.Approximately(vec.y, -other.y);
        }
    }
}