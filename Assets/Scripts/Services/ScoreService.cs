using System;
using Interfaces;

namespace Services
{
    public class ScoreService : IScoreService
    {
        private readonly INumberProvider _numberProvider;
        private int _currentScore;

        public event Action<int> ScoreChanged;

        public ScoreService(INumberProvider numberProvider)
        {
            _numberProvider = numberProvider;
            _currentScore = 0;
        }

        public void AddScore(int cubeNumber)
        {
            var value = cubeNumber / 2;
            _currentScore += value;
            ScoreChanged?.Invoke(_currentScore);
        }
    }
} 