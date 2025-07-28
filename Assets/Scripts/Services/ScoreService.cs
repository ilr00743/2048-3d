using System;
using UnityEngine;

public class ScoreService : IScoreService
{
    private int _currentScore;
    private INumberProvider _numberProvider;

    public event Action<int> ScoreChanged;

    public ScoreService(INumberProvider numberProvider)
    {
        _numberProvider = numberProvider;
        _currentScore = 0;
    }

    public void AddScore(int cubeNumber)
    {
        var value = _numberProvider.GetPower(cubeNumber);
        _currentScore += value;
        ScoreChanged?.Invoke(_currentScore);
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }
} 