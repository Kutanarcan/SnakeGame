using System.Collections;
using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public class SnakeGame
    {
        //Create a Input Pressed Event with System.Action 
        public event System.Action SnakeEatItSelf;
        public event System.Action FirstInputPressed;
        public event System.Action AteFood;

        // Add Model field
        private SnakeGameModel m_model;
        private readonly Snake m_snake = new Snake(DEFAULT_SNAKE_CAPACITY);
        private Food m_food;
        private Vector2 m_lastKnownDirection;
        private Vector2 m_nextDirection;

        private const int DEFAULT_SNAKE_CAPACITY = 50; // Default capacity for the snake body

        // Create Initialize and DeInitialize methods
        public void SetModel(SnakeGameModel model)
        {
            m_model = model;
        }

        public void CreateGrid()
        {
            m_model.grid.CreateCells(CellType.Empty, m_model.spriteHolder.emptyCell);
        }

        public void InitializeSnake()
        {
            m_model.snakeStartSize = Mathf.Max(2, m_model.snakeStartSize);
            
            m_snake.InitializeDefaultSize(m_model.snakeStartSize);
            m_snake.InitializeDefaultPosition(m_model.grid.GetCenteredCellIndex(),
                m_model.grid.GetCenteredCellIndex() + m_model.snakeStartSize - 1);

            DrawSnake();
        }

        public void InitializeFood()
        {
            m_food.index = m_model.grid.GetRandomEmptyCellIndex();

            DrawFood();
        }

        private void DrawFood()
        {
            // Set food sprite at the random index
            m_model.grid.SetCell(m_food.index, CellType.Food, m_model.spriteHolder.food);
        }

        public void Demolish()
        {
            m_model.grid.Dispose();
            m_snake.Dispose();
            m_food.Dispose();
            m_lastKnownDirection = Vector2.zero;
            m_nextDirection = Vector2.zero;
            SnakeEatItSelf = null;
            FirstInputPressed = null;
            AteFood = null;
        }

        // Create a method called DrawSnake, Set head an tail sprites first, loop the body 1-length-1 and set body sprites
        private void DrawSnake()
        {
            // Set head sprite
            m_model.grid.SetCell(m_snake.HeadIndex, CellType.Snake, m_model.spriteHolder.snakeHead);
            // Set tail sprite
            m_model.grid.SetCell(m_snake.TailIndex, CellType.Snake, m_model.spriteHolder.snakeTail);

            // Loop through the body and set body sprites
            for (int i = 1; i < m_snake.Length - 1; i++)
            {
                m_model.grid.SetCell(m_snake[i], CellType.Snake, m_model.spriteHolder.snakeBody);
            }
        }

        public void HandleInput()
        {
            for (int i = 0; i < m_model.directionKeys.Length; i++)
            {
                var dirKey = m_model.directionKeys[i];

                if (Input.GetKeyDown(dirKey.key))
                {
                    GiveNextDirectionForSnakeMovement(dirKey.direction);

                    break;
                }
            }
        }

        public void GiveNextDirectionForSnakeMovement(Vector2 dir)
        {
            var isReversed = m_lastKnownDirection.IsReversed(dir);
            m_nextDirection = isReversed ? m_nextDirection : dir;

            if (m_lastKnownDirection == Vector2.zero)
            {
                m_lastKnownDirection = m_nextDirection;
                FirstInputPressed?.Invoke();
            }
        }

        public void MoveSequence()
        {
            byte nextCellType = m_model.grid.LookAhead(m_snake.HeadIndex, (int)m_lastKnownDirection.x,
                (int)m_lastKnownDirection.y, out int nextCellIndex);

            if (nextCellType == CellType.Snake)
            {
                SnakeEatItSelf?.Invoke();
                return;
            }

            int previousTailIndex = m_snake.TailIndex;

            if (nextCellType == CellType.Food)
            {
                m_snake.Grow(m_model.grid.Width, m_model.grid.Height);
            }

            m_snake.Move(nextCellIndex);

            DrawSnake();

            if (nextCellType == CellType.Empty)
                m_model.grid.SetCell(previousTailIndex, CellType.Empty, m_model.spriteHolder.emptyCell);

            if (nextCellType == CellType.Food)
            {
                m_food.index = m_model.grid.GetRandomEmptyCellIndex();
                AteFood?.Invoke();
            }

            DrawFood();

            m_lastKnownDirection = m_nextDirection;
        }

        public IEnumerator AnimateSnakeScaleTween(MonoBehaviour source, SnakeAnimationData animationData)
        {
            for (int i = 0; i < m_snake.Length; i++)
            {
                var transform = m_model.grid.cells.cellSpriteRenderers[m_snake[i]].transform;
                source.StartCoroutine(Tweening.ScaleTween(transform, animationData));
                yield return Tweening.GetWaitForSeconds(animationData.durationBetweenScales);
            }
        }
    }
}