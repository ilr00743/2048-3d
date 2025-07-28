using System;
using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    private int _value;
    private NumberProvider _numberProvider;

    private void Awake()
    {
        _numberProvider = new NumberProvider();
        _value = 0;
        UpdateScoreText();
    }
    
    public void AddScore(int cubeNumber)
    {
        var value = _numberProvider.GetPower(cubeNumber);
        _value += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _text.text = $"{TextLabelsConst.SCORE_LABEL} {_value}";
    }
}