using MarcoHelpers;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class PropjeSelectMenu : Menu
{
    public sWordList citizenList;
    public sWordList ceoList;
    private Button[] buttons;
    private TMP_Text[] texts;

    private List<Argument> currentWords = new List<Argument>();
    private Dictionary<TMP_Text, Argument> buttonArguments = new Dictionary<TMP_Text, Argument>();
    private sWordList currentList;
    private bool isCEO;

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
        currentList = citizenList;
        foreach (TMP_Text text in texts)
        {
            GetNewWord(text);
        }

        DisableClickThrower();
    }

    private void Update()
    {
        if (buttons[0].IsActive())
        {
            DisableClickThrower();
        }
    }

    public void OnSwitch(bool _isCEO)
    {
        if (isCEO != _isCEO)
        {
            isCEO = _isCEO;
            currentList = isCEO ? ceoList : citizenList;
            currentWords = new List<Argument>();
            foreach (TMP_Text text in texts)
            {
                GetNewWord(text);
            }
            ShowButtons();
        }
    }

    private void EnableClickThrower()
    {
        var clicks = FindObjectsOfType<ClickThrower>();
        foreach (var click in clicks) {
            click.EnableSelf();
        }
    }

    private void DisableClickThrower()
    {
        var clicks = FindObjectsOfType<ClickThrower>();
        foreach (var click in clicks)
        {
            click.DisableSelf();
        }
    }

    private void ButtonClick(Button _button)
    {
        EventSystem.RaiseEvent(EventName.PROPJE_CHOSEN);

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
        Argument randomWord = currentList.GetRandomWord();
        while (currentWords.Contains(randomWord) && currentList.words.Length > texts.Length)
        {
            randomWord = currentList.GetRandomWord();
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
