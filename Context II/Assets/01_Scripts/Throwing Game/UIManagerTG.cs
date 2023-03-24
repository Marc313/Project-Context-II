using TMPro;
using UnityEngine;

public class UIManagerTG : MonoBehaviour
{
    public TMP_Text windowTitle;
    public TMP_Text windowText;

    [SerializeField] private GameObject tutorialWindow;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    public void ShowDialogueCanvas()
    {
        tutorialWindow.SetActive(true);
    }

    public void HideDialogueCanvas()
    {
        tutorialWindow.gameObject.SetActive(false);
        windowTitle.text = string.Empty;
        windowText.text = string.Empty;
    }

    public void SwitchToSequence()
    {
        ShowDialogueCanvas();
    }
}
