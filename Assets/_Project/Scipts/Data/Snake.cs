using System.Collections.Generic;
using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public class Snake
    {
        private List<int> m_body; // List of indices representing the snake's body

        public Snake(int defaultCapacity)
        {
            // Initialize the snake's body with a default size
            m_body = new List<int>(defaultCapacity);
        }

        public void InitializeDefaultSize(int defaultSize)
        {
            for (int i = 0; i < defaultSize; i++)
            {
                m_body.Add(-1);
            }
        }

      public int this[int index] => m_body[index];

        public void InitializeDefaultPosition(int defaultHeadIndex, int defaultTailIndex)
        {
            // Set the head and tail of the snake to the default indices
            var difference = Mathf.Abs(defaultTailIndex - defaultHeadIndex);
            var increase = difference / (m_body.Count - 1); // Calculate the increase for each body segment

            m_body[0] = defaultHeadIndex; // Head index

            for (int i = 1; i < difference; i++)
            {
                m_body[i] = defaultHeadIndex + increase * i; // Fill the body with sequential indices
            }

            m_body[^1] = defaultTailIndex; // Tail index
        }

        public int HeadIndex => m_body[0]; // The head of the snake is the first element in the body list
        public int TailIndex => m_body[^1]; // The tail of the snake is the last element in the body list
        public int Length => m_body.Count; // The length of the snake is the size of the body list

        // Create a method that change head index
        public void Move(int newHeadIndex)
        {
            // Shift the body to the right and set the new head index
            for (int i = Length - 1; i > 0; i--)
            {
                m_body[i] = m_body[i - 1];
            }

            m_body[0] = newHeadIndex;
        }

        // Our body list is sequential. Add a method that grows the snake. Only thing we need to do is add element and give tailIndex-1. Then Swap with the tail index
        public void Grow(int width, int height)
        {
            int snakeLastIndexValue = m_body[^1];
            int snakeSecondLastIndexValue = m_body[^2];
            int difference = snakeLastIndexValue - snakeSecondLastIndexValue;

            // Calculate new tail position based on grid wrapping
            int newTailIndex = snakeLastIndexValue + difference;

            // Handle horizontal wrapping
            int lastX = snakeLastIndexValue % width;
            int lastY = snakeLastIndexValue / width;
            int diffX = difference % width;
            int diffY = difference / width;

            int newX = (lastX + diffX + width) % width;
            int newY = (lastY + diffY + height) % height;
            newTailIndex = newY * width + newX;

            m_body.Add(newTailIndex);
        }

        // Create a method for dispose this class values
        public void Dispose()
        {
            m_body.Clear(); // Clear the body list to free up memory
        }
    }
}