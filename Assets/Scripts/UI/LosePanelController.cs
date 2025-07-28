using UnityEngine;

namespace UI
{
    public class LosePanelController : MonoBehaviour
    {
        [SerializeField] private GameObject _losePanel;

        private void Start()
        {
            if (_losePanel != null)
            {
                _losePanel.SetActive(false);
            }
        }

        public void Show()
        {
            if (_losePanel != null)
            {
                _losePanel.SetActive(true);
            }
        }

        public void Hide()
        {
            if (_losePanel != null)
            {
                _losePanel.SetActive(false);
            }
        }
    }
} 