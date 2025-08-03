using System;
using UnityEngine;

namespace Snake.Runtime.CSharp
{
    public class Timer
    {
        private float m_interval;
        private float m_timer;
        private bool m_isRunning;

        public Timer(float interval)
        {
            m_interval = interval;
            m_timer = 0f;
        }

        public void Start()
        {
            m_isRunning = true;
        }

        public void Update(float deltaTime, Action onIntervalReached = null)
        {
            if (!m_isRunning)
            {
                return;
            }

            m_timer += deltaTime;

            if (m_timer >= m_interval)
            {
                m_timer = 0f;
                onIntervalReached?.Invoke();
            }
        }

        // Create a method that stops the timer
        public void Stop()
        {
            m_isRunning = false;
            m_timer = 0f; // Reset the timer when stopped
        }
    }
}