using TMPro;
using UnityEngine;

public class UIManagerTG : MonoBehaviour
{
    public TMP_Text windowTitle;
    public TMP_Text windowText;

    [SerializeField] private GameObject tutorialWindow;
    [SerializeField] private GameObject propjeSelectMenu;
    [SerializeField] private GameObject HUD;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    public void ShowDialogueCanvas()
    {
        EnablePropjeMenu(false);
        tutorialWindow.SetActive(true);
    }

    public void HideDialogueCanvas()
    {
        EnablePropjeMenu(true);
        tutorialWindow.gameObject.SetActive(false);
        windowTitle.text = string.Empty;
        windowText.text = string.Empty;
    }

    public void SwitchToSequence()
    {
        ShowDialogueCanvas();
    }

    public void EnablePropjeMenu(bool _enabled)
    {
        propjeSelectMenu.SetActive(_enabled);
    }

    public void ShowHud()
    {
        HUD.SetActive(true);
    }

    public void HideHud()
    {
        HUD.SetActive(true);
    }

}
