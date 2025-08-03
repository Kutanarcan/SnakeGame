using System;
using Snake.Runtime.CSharp;
using UnityEngine;

namespace Snake.Runtime.Mono
{
    public class GameStarter : MonoBehaviour
    {
        //Create width and height variables and use SerializeField attribute
        [SerializeField] private int m_Width = 10;
        [SerializeField] private int m_Height = 10;
        [SerializeField] private int m_SnakeStartSize = 3;
        [SerializeField] private SnakeAnimationData m_SnakeScaleAnimationData;
        [SerializeField] private float m_SnakeAutoMoveInternal = 1f;

        [SerializeField] private GridSpriteHolder m_SpriteHolder;

        private SnakeGame m_snakeGame;
        private Timer m_autoMoveTimer;
        private SnakeGameModel m_snakeGameModel;
        private bool m_isGameRunning = false;

        private void Awake()
        {
            CreateGameInstance();
        }
        
        private void Start()
        {
            EstablishGame();
        }
        
        private void Update()
        {
            m_snakeGame.HandleInput();
            m_autoMoveTimer.Update(Time.deltaTime, m_snakeGame.MoveSequence);

            if (!m_isGameRunning && Input.GetKeyDown(KeyCode.Space))
            {
                DemolishGame();
                EstablishGame();
                GC.Collect();
            }
        }
        
        private void OnDestroy()
        {
            DemolishGame();
        }

        private void CreateGameInstance()
        {
            if (m_snakeGameModel is not null)
                return;

            m_snakeGame = new SnakeGame();
            m_snakeGameModel = new SnakeGameModel(m_SpriteHolder);
            m_snakeGameModel.snakeStartSize = m_SnakeStartSize;
            m_autoMoveTimer = new Timer(m_SnakeAutoMoveInternal);
        }

        private void EstablishGame()
        {
            m_snakeGameModel.grid = new FixedGrid(m_Width, m_Height);
            m_snakeGame.SetModel(m_snakeGameModel);
            m_snakeGame.CreateGrid();
            m_snakeGame.InitializeSnake();
            m_snakeGame.InitializeFood();

            m_SnakeScaleAnimationData.originalScale = Vector3.one;

            RegisterEvents();
        }

        private void DemolishGame()
        {
            m_autoMoveTimer.Stop();
            DeRegisterEvents();
            m_snakeGame.Demolish();
        }

        private void RegisterEvents()
        {
            m_snakeGame.SnakeEatItSelf += OnSnakeEatItSelf;
            m_snakeGame.FirstInputPressed += OnFirstInputPressed;
            m_snakeGame.AteFood += OnAteFood;
        }
        
        private void DeRegisterEvents()
        {
            m_snakeGame.SnakeEatItSelf -= OnSnakeEatItSelf;
            m_snakeGame.FirstInputPressed -= OnFirstInputPressed;
            m_snakeGame.AteFood -= OnAteFood;
        }

        private void OnAteFood()
        {
            StartCoroutine(m_snakeGame.AnimateSnakeScaleTween(this, m_SnakeScaleAnimationData));
        }

        private void OnSnakeEatItSelf()
        {
            m_isGameRunning = false;
            m_autoMoveTimer.Stop();
        }

        private void OnFirstInputPressed()
        {
            m_autoMoveTimer.Start();
            m_isGameRunning = true;
        }
    }
}