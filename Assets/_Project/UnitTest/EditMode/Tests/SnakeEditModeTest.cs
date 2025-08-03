using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Snake.Runtime.CSharp;
using UnityEngine;
using UnityEngine.TestTools;

public class SnakeEditModeTest
{
    #region Setup

    private GridSpriteHolder m_spriteHolder;
    private SnakeGameModel m_model;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        m_spriteHolder = new GridSpriteHolder();
        m_model = new SnakeGameModel(m_spriteHolder);
    }

    #endregion

    #region Grid Setup Tests

    // Test Case-1
    // Does Grid Setup Properly?
    // -> Is all Cells has default values we want when first created?
    // Does Grid Initialize Without Visual Values?
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    public void GridSetup_IsFilledWith_DefaultCellTypes(int width, int height)
    {
        // Arrange
        m_model.grid = new FixedGrid(width, height);
        m_model.snakeStartSize = 3;

        SnakeGame snakeGame = new SnakeGame();

        // Act
        snakeGame.SetModel(m_model);

        // Assert
        Assert.NotNull(m_model.grid.cells, "Grid cells container should have been created.");
        Assert.IsNotNull(m_model.grid.cells.cellType, "cellType array should be initialized.");

        for (int i = 0; i < m_model.grid.cells.cellType.Length; i++)
        {
            Assert.AreEqual(CellType.Empty, m_model.grid.cells.cellType[i], $"Cell {i} should be Empty by default.");
        }
    }

    // Test Case-2
    // Does Grid Work When Given Negative Values?
    // -> Does it throw a Warning and handle it gracefully?

    [TestCase(-5, 5)]
    [TestCase(-5, -5)]
    [TestCase(5, -5)]
    public void GridSetup_IsInitializing_WhenAnyNegativeValueGiven(int width, int height)
    {
        // Arrange
        m_model.grid = new FixedGrid(width, height);
        m_model.snakeStartSize = 3;

        SnakeGame snakeGame = new SnakeGame();

        // Act
        snakeGame.SetModel(m_model);

        // Assert
        Assert.NotNull(m_model.grid.cells, "Grid cells container should have been created.");
        Assert.IsNotNull(m_model.grid.cells.cellType, "cellType array should be initialized.");

        for (int i = 0; i < m_model.grid.cells.cellType.Length; i++)
        {
            Assert.AreEqual(CellType.Empty, m_model.grid.cells.cellType[i], $"Cell {i} should be Empty by default.");
        }
    }

    // Test Case-3
    // Does Grid Work When Given Any Zero Values?
    // -> Does it throw a Warning and handle it gracefully?

    [TestCase(5, 0)]
    [TestCase(0, 5)]
    [TestCase(0, 0)]
    public void GridSetup_IsInitializing_WhenAnyZeroValueGiven(int width, int height)
    {
        // Arrange
        m_model.grid = new FixedGrid(width, height);
        m_model.snakeStartSize = 3;

        SnakeGame snakeGame = new SnakeGame();

        // Act
        snakeGame.SetModel(m_model);

        // Assert
        Assert.NotNull(m_model.grid.cells, "Grid cells container should have been created.");
        Assert.IsNotNull(m_model.grid.cells.cellType, "cellType array should be initialized.");

        for (int i = 0; i < m_model.grid.cells.cellType.Length; i++)
        {
            Assert.AreEqual(CellType.Empty, m_model.grid.cells.cellType[i], $"Cell {i} should be Empty by default.");
        }
    }

    #endregion

    #region Snake Initialization Tests

    // Test Case-4
    // Does Snake Created Properly?
    // -> StartSize, Direction and Grid Positions
    [Test]
    public void SnakeInitialization_IsCorrectInGrid_DefaultSizeAndPosition()
    {
        // Arrange
        m_model.grid = new FixedGrid(10, 10);
        m_model.snakeStartSize = 3;

        SnakeGame snakeGame = new SnakeGame();
        snakeGame.SetModel(m_model);
        snakeGame.CreateGrid();

        snakeGame.InitializeSnake();

        // Act
        List<int> snakeSegments = new List<int>();

        for (int i = 0; i < m_model.grid.cells.cellType.Length; i++)
        {
            if (m_model.grid.cells.cellType[i] == CellType.Snake)
            {
                snakeSegments.Add(i);
            }
        }

        // Assert

        Assert.AreEqual(m_model.snakeStartSize, snakeSegments.Count, "Snake should have the default start size.");
    }

    // Test Case-5
    // Does Snake Creating With Below Two Size?
    // -> Does it handle gracefully?
    [TestCase(1)]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-5)]
    public void SnakeLenght_IsHandling_BelowTwo(int snakeStartSize)
    {
        // Arrange
        m_model.grid = new FixedGrid(10, 10);
        m_model.snakeStartSize = snakeStartSize;

        SnakeGame snakeGame = new SnakeGame();
        snakeGame.SetModel(m_model);
        snakeGame.CreateGrid();

        snakeGame.InitializeSnake();

        // Act
        List<int> snakeSegments = new List<int>();

        for (int i = 0; i < m_model.grid.cells.cellType.Length; i++)
        {
            if (m_model.grid.cells.cellType[i] == CellType.Snake)
            {
                snakeSegments.Add(i);
            }
        }

        // Assert

        Assert.AreEqual(m_model.snakeStartSize, snakeSegments.Count, "Snake should have the default start size.");
    }

    // Test Case-6
    // Does Snake Creating With Bigger Than Width Of The Grid?
    // -> Does it handle gracefully?
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(10)]
    [TestCase(30)]
    [TestCase(40)]
    public void SnakeLenght_IsHandling_WhenLenghtBiggerThenWidthOfTheGrid(int snakeStartSize, int gridWidth = 10)
    {
        // Arrange
        m_model.grid = new FixedGrid(gridWidth, 10);
        m_model.snakeStartSize = snakeStartSize;

        SnakeGame snakeGame = new SnakeGame();
        snakeGame.SetModel(m_model);
        snakeGame.CreateGrid();

        snakeGame.InitializeSnake();

        // Act
        List<int> snakeSegments = new List<int>();

        for (int i = 0; i < m_model.grid.cells.cellType.Length; i++)
        {
            if (m_model.grid.cells.cellType[i] == CellType.Snake)
            {
                snakeSegments.Add(i);
            }
        }

        // Assert

        Assert.AreEqual(m_model.snakeStartSize, snakeSegments.Count, "Snake should have the default start size.");
    }

    #endregion
}