using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public static class Tweening
    {
        // Create a dictionary that holds float,WaitForSeconds
        private static readonly Dictionary<float, UnityEngine.WaitForSeconds> waitForSecondsCache =
            new Dictionary<float, UnityEngine.WaitForSeconds>(10);
        // Create a method that returns WaitForSeconds from the dictionary or create a new one

        public static UnityEngine.WaitForSeconds GetWaitForSeconds(float seconds)
        {
            if (!waitForSecondsCache.TryGetValue(seconds, out var waitForSeconds))
            {
                waitForSeconds = new UnityEngine.WaitForSeconds(seconds);
                waitForSecondsCache[seconds] = waitForSeconds;
            }

            return waitForSeconds;
        }

        // Create a method that get target localscale vector and duration. We will lerp the scale the target scale over half of the duration, in other half of the duration we will lerp back to the original scale
        public static IEnumerator ScaleTween(Transform target, SnakeAnimationData animationData)
        {
            float halfDuration = animationData.duration / 2f;

            // Lerp to target scale
            float elapsedTime = 0f;
            while (elapsedTime < halfDuration)
            {
                target.localScale = Vector3.Lerp(animationData.originalScale, animationData.targetScale,
                    elapsedTime / halfDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure we reach the target scale
            target.localScale = animationData.targetScale;

            // Lerp back to original scale
            elapsedTime = 0f;
            while (elapsedTime < halfDuration)
            {
                target.localScale = Vector3.Lerp(animationData.targetScale, animationData.originalScale,
                    elapsedTime / halfDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure we return to the original scale
            target.localScale = animationData.originalScale;
        }

        // Create a Coroutine that gets a MonoBehaviour and Array of Components, a duration, and a target scale, durationBetweenScales
        public static IEnumerator ScaleComponentsCoroutine(MonoBehaviour source, Component[] components,
            SnakeAnimationData animationData)
        {
            foreach (var component in components)
            {
                source.StartCoroutine(ScaleTween(component.transform, animationData));
                yield return GetWaitForSeconds(animationData.durationBetweenScales);
            }
        }
    }
}