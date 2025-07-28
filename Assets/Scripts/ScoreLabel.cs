using System;
using TMPro;
using UnityEngine;
using VContainer;

public class ScoreLabel : MonoBehaviour, IScoreService, IScoreLabel
{
    [SerializeField] private TMP_Text _text;
    
    private int _value;
    private INumberProvider _numberProvider;
    private IScoreService _scoreService;

    public event Action<int> ScoreChanged;

    [Inject]
    public void Construct(INumberProvider numberProvider, IScoreService scoreService)
    {
        _numberProvider = numberProvider;
        _scoreService = scoreService;
        _value = 0;
        
        if (_text == null)
        {
            _text = GetComponentInChildren<TMP_Text>();
        }
        
        UpdateScoreText();
        
        if (_scoreService is ScoreService scoreServiceInstance)
        {
            scoreServiceInstance.ScoreChanged += OnScoreChanged;
        }
    }

    public void Initialize(INumberProvider numberProvider)
    {
        _numberProvider = numberProvider;
        _value = 0;
        UpdateScoreText();
    }
    
    public void AddScore(int cubeNumber)
    {
        var value = _numberProvider.GetPower(cubeNumber);
        _value += value;
        UpdateScoreText();
        ScoreChanged?.Invoke(_value);
    }

    public int GetCurrentScore()
    {
        return _value;
    }

    private void OnScoreChanged(int newScore)
    {
        _value = newScore;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (_text != null)
        {
            _text.text = $"{TextLabelsConst.SCORE_LABEL} {_value}";
        }
    }

    private void OnDestroy()
    {
        if (_scoreService is ScoreService scoreServiceInstance)
        {
            scoreServiceInstance.ScoreChanged -= OnScoreChanged;
        }
    }
}