using System;

public interface IScoreService
{
    event Action<int> ScoreChanged;
    void AddScore(int cubeNumber);
    int GetCurrentScore();
} 