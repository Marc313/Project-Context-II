using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropjeSelectMenu : MonoBehaviour
{
    public Button[] buttons;
    public TMP_Text[] texts;
    public sWordList list;

    public List<string> currentWords = new List<string>();

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        texts = new TMP_Text[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];
            texts[i] = button.GetComponentInChildren<TMP_Text>();
            button.onClick.AddListener(() => ButtonClick(button));
        }
    }

    private void Start()
    {
        foreach (TMP_Text text in texts)
        {
            GetNewWord(text);
        }

        DisableClickThrower();
    }

    private void EnableClickThrower()
    {
        FindObjectOfType<ClickThrower>().enabled = true;
    }

    private void DisableClickThrower()
    {
        FindObjectOfType<ClickThrower>().enabled = false;
    }

    private void ButtonClick(Button _button)
    {
        TMP_Text text = _button.GetComponentInChildren<TMP_Text>();
        FindObjectOfType<ClickThrower>().currentWord = text.text;
        currentWords.Remove(text.text);
        GetNewWord(text);
        HideButtons();
    }

    public void ShowButtons()
    {
        DisableClickThrower();
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void HideButtons()
    {
        EnableClickThrower();
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void GetNewWord(TMP_Text text)
    {
        string randomWord = list.GetRandomWord();
        if (!currentWords.Contains(randomWord) || list.words.Length <= texts.Length)
        {
            text.text = randomWord;
            currentWords.Add(randomWord);
        }
    }
}
