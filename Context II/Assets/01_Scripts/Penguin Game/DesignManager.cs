using UnityEngine;

public class DesignManager : MonoBehaviour
{
    public int counter = 0;
    public int maxChoices = 3;

    private UIManager uiManager;
    private bool isDone;

    private void Start()
    {
        uiManager = ServiceLocator.GetService<UIManager>();
        uiManager.ProgressCounter.text = $"{counter}/{maxChoices}";
    }

    public void AddChoice()
    {
        if (isDone) return;
        counter++;
        uiManager.ProgressCounter.text = $"{counter}/{maxChoices}";


        if (counter >= maxChoices)
        {
            uiManager.ProgressCounter.text = "Klaar!";
        }
    }

    public void DeleteChoice()
    {
        if (isDone) return;

        counter--;
        uiManager.ProgressCounter.text = $"{counter}/{maxChoices}";
    }
}
