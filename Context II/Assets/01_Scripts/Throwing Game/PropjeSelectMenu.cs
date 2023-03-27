using MarcoHelpers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropjeSelectMenu : Menu
{
    public sWordList list;
    private Button[] buttons;
    private TMP_Text[] texts;

    private List<Argument> currentWords = new List<Argument>();
    private Dictionary<TMP_Text, Argument> buttonArguments = new Dictionary<TMP_Text, Argument>();


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
        FindObjectOfType<ClickThrower>()?.EnableSelf();
    }

    private void DisableClickThrower()
    {
        FindObjectOfType<ClickThrower>()?.DisableSelf();
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
        while (currentWords.Contains(randomWord) && list.words.Length > texts.Length)
        {
            randomWord = list.GetRandomWord();
        }

        text.text = randomWord.word;
        currentWords.Add(randomWord);
        if (!buttonArguments.ContainsKey(text))
        {
            buttonArguments.Add(text, randomWord);
        }
        else
        {
            buttonArguments[text] = randomWord;
        }
    }

    public string GetDescription(TMP_Text _textObject)
    {
        Argument argument = buttonArguments[_textObject];
        return argument.description;
    }

    private void HideButtons()
    {
        //EnableClickThrower();
        EventSystem.RaiseEvent(EventName.MENU_CLOSED);
        foreach (Button button in buttons)
        {
            button.GetComponent<ShowDescriptionOnHover>().descriptionMenu.SetActive(false);
            button.gameObject.SetActive(false);
        }
    }
}
