using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public static class SpriteRendererFactory
    {
        public static SpriteRenderer CreateSpriteRenderer(Vector2 position, int index)
        {
            var spriteRenderer = new GameObject($"Cell_{index}").AddComponent<SpriteRenderer>();

            spriteRenderer.transform.position = position;

            spriteRenderer.gameObject.SetActive(false);

            return spriteRenderer;
        }
    }
}