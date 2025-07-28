using System;

namespace Interfaces
{
    public interface IScoreService
    {
        event Action<int> ScoreChanged;
        void AddScore(int cubeNumber);
    }
} 