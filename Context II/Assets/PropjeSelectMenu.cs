using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropjeSelectMenu : MonoBehaviour
{
    public sWordList list;
    private Button[] buttons;
    private TMP_Text[] texts;

    private List<Argument> currentWords = new List<Argument>();
    private Dictionary<TMP_Text, Argument> buttonArguments;


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
        Argument oldArgument = buttonArguments[text];
        currentWords.Remove(oldArgument);
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

    public void GetNewWord(TMP_Text text)
    {
        Argument randomWord = list.GetRandomWord();
        while (currentWords.Contains(randomWord) || list.words.Length > texts.Length)
        {
            randomWord = list.GetRandomWord();
        }

        text.text = randomWord.word;
        currentWords.Add(randomWord);
        buttonArguments.Add(text, randomWord);
    }

    public string GetDescription(TMP_Text _textObject)
    {
        Argument argument = buttonArguments[_textObject];
        return argument.description;
    }

    private void HideButtons()
    {
        EnableClickThrower();
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
