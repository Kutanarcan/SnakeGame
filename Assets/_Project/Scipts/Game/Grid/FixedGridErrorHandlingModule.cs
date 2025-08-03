using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public class FixedGridErrorHandlingModule
    {
        public bool IsValueMisMatch(int value, string propertyName)
        {
            if (value > 0)
                return false;

            Debug.LogWarning(
                $"{nameof(FixedGrid)} {propertyName} cannot Below or Equal to zero. {propertyName} going to get fix.");

            return true;
        }
    }
}