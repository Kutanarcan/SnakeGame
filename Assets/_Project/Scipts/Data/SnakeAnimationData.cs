using UnityEngine;

namespace Snake.Runtime.CSharp
{
    [System.Serializable]
    public struct SnakeAnimationData
    {
        public float duration;
        public Vector3 targetScale;
        [HideInInspector] public Vector3 originalScale;
        public float durationBetweenScales;
    }
}