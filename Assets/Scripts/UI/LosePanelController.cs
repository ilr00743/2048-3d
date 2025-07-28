using UnityEngine;

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

    public void ShowLosePanel()
    {
        if (_losePanel != null)
        {
            _losePanel.SetActive(true);
        }
    }

    public void HideLosePanel()
    {
        if (_losePanel != null)
        {
            _losePanel.SetActive(false);
        }
    }
} 