using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public class FixedGrid
    {
        public readonly CellOfArrays cells;

        public int Width => m_width;
        public int Height => m_height;

        private int m_width;
        private int m_height;

        private List<int> m_emptyCells; // Optional: List to keep track of empty cells
        private FixedGridErrorHandlingModule m_errorHandlingModule = new FixedGridErrorHandlingModule();

        private const int MISMATCH_MINIMUM_GRID_VALUE = 5;

        #region Constructor

        public FixedGrid(int width, int height)
        {
            m_width = m_errorHandlingModule.IsValueMisMatch(width, "width") ? MISMATCH_MINIMUM_GRID_VALUE : width;
            m_height = m_errorHandlingModule.IsValueMisMatch(height, "height") ? MISMATCH_MINIMUM_GRID_VALUE : height;

            cells = new CellOfArrays(m_width * m_height);
            m_emptyCells = new List<int>(m_width * m_height);
        }

        #endregion

        #region Create Cells

        //Create a method called Create Cells
        public void CreateCells(byte defaultCellType, Sprite defaultCellSprite)
        {
            // Initialize cells with default values
            for (int i = 0; i < cells.cellType.Length; i++)
            {
                // Calculate the x and y position based on the index
                int x = i % m_width;
                int y = i / m_width;

                var currentCellPos = CalculateGridPosition(x, y);
                cells.cellType[i] = defaultCellType;

                cells.cellSpriteRenderers[i] = cells.cellSpriteRenderers[i] is null
                    ? SpriteRendererFactory.CreateSpriteRenderer(currentCellPos, i)
                    : cells.cellSpriteRenderers[i];

                cells.cellSpriteRenderers[i].sprite = defaultCellSprite;

                cells.cellSpriteRenderers[i].gameObject.SetActive(true);
                cells.cellPositions[i] = currentCellPos;
            }
        }

        #endregion

        #region Calculation

        // Find centered cell index with width and height
        public int GetCenteredCellIndex()
        {
            // Calculate the center index based on width and height
            int centerX = m_width / 2;
            int centerY = m_height / 2;

            return centerY * m_width + centerX;
        }

        //Create a method that Calculates Grid Positions based on x,y values. Also For centre the grid to (0,0)
        private Vector2 CalculateGridPosition(in int x, in int y)
        {
            // Calculate the position based on grid size
            float posX = x - (m_width / 2f) + 0.5f;
            float posY = y - (m_height / 2f) + 0.5f;

            return new Vector2(posX, posY);
        }

        // Create a method called SetCellType


        //Create method called LookAhead, also use GetCellType method inside
        public byte LookAhead(in int index, in int directionX, in int directionY, out int nextIndex)
        {
            var x = index % m_width;
            var y = index / m_width;

            var newX = ((x + directionX) % m_width + m_width) % m_width;
            var newY = ((y + directionY) % m_height + m_height) % m_height;

            nextIndex = newY * m_width + newX;

            return cells.cellType[nextIndex];
        }

        // Create a method that find random grid cell index that is empty. Use m_model and grid
        public int GetRandomEmptyCellIndex()
        {
            // If m_emptyCells is null or empty, populate it with empty cell indices
            m_emptyCells.Clear();

            for (int i = 0; i < cells.cellType.Length; i++)
            {
                if (cells.cellType[i] == CellType.Empty)
                {
                    m_emptyCells.Add(i);
                }
            }

            var randomIndex = UnityEngine.Random.Range(0, m_emptyCells.Count);

            return m_emptyCells[randomIndex];
        }

        #endregion

        #region Configuration

        public void SetCell(in int index, in byte cellType, Sprite sprite)
        {
            cells.cellType[index] = cellType;
            cells.cellSpriteRenderers[index].sprite = sprite;
        }

        #endregion

        // Dispose
        public void Dispose()
        {
            m_emptyCells.Clear();
            cells.Dispose(); // Clear the cell arrays to free up memory
            m_width = 0;
            m_height = 0;
        }
    }
}