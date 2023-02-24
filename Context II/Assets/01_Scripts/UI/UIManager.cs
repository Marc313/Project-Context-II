using JetBrains.Annotations;
using newDialogue;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Dialogue System")]
    public Canvas dialogueCanvas;
    public TMP_Text dialogueSequenceName;
    public TMP_Text dialogueSequenceText;
    public TMP_Text dialogueChoiceName;
    public TMP_Text dialogueChoiceText;
    public GameObject dialogueSequenceUI;
    public GameObject dialogueChoiceUI;
    [Space]
    public Button[] choiceButtons;

    [Header("Question System")]
    public GameObject QuesitionUI;
    public TMP_Text QuestionText;

    [Header("Designing")]
    public TMP_Text ProgressCounter;

    public TMP_Text dialogueName { get; set; }
    public TMP_Text dialogueText { get; set; }
    private DialogueManager dialogueManager;


    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void Start()
    {
        dialogueManager = ServiceLocator.GetService<DialogueManager>();
        SwitchToSequence();
    }

    public void ShowQuestionCanvas()
    {
        QuesitionUI.SetActive(true);
    }

    public void ChangeQuestionText(string _question)
    {
        QuestionText.text = _question;
    }

    public void SwitchToSequence()
    {
        dialogueSequenceUI.SetActive(true);
        dialogueChoiceUI.SetActive(false);

        dialogueName = dialogueSequenceName;
        dialogueText = dialogueSequenceText;
    }

    public void SwitchToChoice()
    {
        dialogueSequenceUI.SetActive(false);
        dialogueChoiceUI.SetActive(true);

        dialogueName = dialogueChoiceName;
        dialogueText = dialogueChoiceText;
    }

    // Leave to UI Manager
    public void ShowDialogueCanvas()
    {
        dialogueCanvas.gameObject.SetActive(true);
    }

    // Leave to UI Manager
    public void HideDialogueCanvas()
    {
        dialogueCanvas.gameObject.SetActive(false);
        dialogueSequenceName.text = string.Empty;
        dialogueSequenceText.text = string.Empty;
    }

    public void ConnectButtons(Choice[] choices, System.Action _onSubsequenceEnd)
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            Button button = choiceButtons[i];
            Choice choice = choices[i];
            TMP_Text optionText = button.GetComponentInChildren<TMP_Text>();

            if (optionText != null) optionText.text = choice.optionName;
            button.onClick.AddListener(() => dialogueManager.StartSequence(choice.response, _onSubsequenceEnd));
        }
    }
}
