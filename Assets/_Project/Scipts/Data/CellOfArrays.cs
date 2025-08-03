using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public class CellOfArrays
    {
        public byte[] cellType;
        public SpriteRenderer[] cellSpriteRenderers;
        public Vector2[] cellPositions;

        public CellOfArrays(in int size)
        {
            cellType = new byte[size];
            cellSpriteRenderers = new SpriteRenderer[size];
            cellPositions = new Vector2[size];
        }

        public void Dispose()
        {
            if (cellType != null)
            {
                for (int i = 0; i < cellType.Length; i++)
                {
                    cellType[i] = 0; // Reset cell type to default
                }
            }

            if (cellSpriteRenderers != null)
            {
                for (int i = 0; i < cellSpriteRenderers.Length; i++)
                {
                    if (cellSpriteRenderers[i] != null)
                    {
                        Object.Destroy(cellSpriteRenderers[i].gameObject);
                        cellSpriteRenderers[i] = null; // Clear reference
                    }
                }
            }

            if (cellPositions != null)
            {
                for (int i = 0; i < cellPositions.Length; i++)
                {
                    cellPositions[i] = Vector2.zero; // Reset positions
                }
            }
            
            cellType = null; // Clear reference to the array
            cellSpriteRenderers = null; // Clear reference to the array
            cellPositions = null; // Clear reference to the array
        }
    }
}