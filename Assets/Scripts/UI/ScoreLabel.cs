using Interfaces;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI
{
    public class ScoreLabel : MonoBehaviour, IScoreLabel
    {
        [SerializeField] private TMP_Text _text;
    
        private int _value;
        private IScoreService _scoreService;

        [Inject]
        public void Construct(IScoreService scoreService)
        {
            _scoreService = scoreService;
            _value = 0;
        
            UpdateScoreText();
        
            _scoreService.ScoreChanged += OnScoreChanged;
        
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
            _scoreService.ScoreChanged -= OnScoreChanged;
        }
    }
}