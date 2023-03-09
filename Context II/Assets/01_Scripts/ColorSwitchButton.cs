using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorSwitchButton : MonoBehaviour
{
    public Color defaultColor;
    public Color selectedColor;

    public UnityEvent OnSelectedSwitch;
    public UnityEvent OnDeselectedSwitch;

    private Button button;
    private bool isSelected;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    { 
        Navigation nav = button.navigation;
        nav.mode = Navigation.Mode.None;
        button.navigation = nav;

    }

    public void SwitchColor()
    {
        if (!isSelected)
        {
            ColorBlock colors = button.colors;
            colors.normalColor= selectedColor;
            button.colors = colors;

            isSelected = !isSelected;

            FindObjectOfType<DesignManager>().AddChoice();
            OnSelectedSwitch?.Invoke();
        }
        else if (isSelected)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = defaultColor;
            button.colors = colors;

            isSelected = !isSelected;
            FindObjectOfType<DesignManager>().DeleteChoice();
            OnDeselectedSwitch?.Invoke();
        }
    }
}
