using JetBrains.Annotations;
using newDialogue;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public Button choiceButton1;
    public Button choiceButton2;
    public Button choiceButton3;

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
        // Hide All UI?

        dialogueName = dialogueSequenceName;
        dialogueText = dialogueSequenceText;
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

    public void ConnectButtons(Choice _choice1, Choice _choice2, Choice _choice3, System.Action _onSubsequenceEnd)
    {
        // Button Text
        choiceButton1.GetComponentInChildren<TMP_Text>().text = _choice1.optionName;
        choiceButton2.GetComponentInChildren<TMP_Text>().text = _choice2.optionName;
        choiceButton3.GetComponentInChildren<TMP_Text>().text = _choice3.optionName;

        // Button OnClick Events
        choiceButton1.onClick.AddListener(() => dialogueManager.StartSequence(_choice1.response, _onSubsequenceEnd));
        choiceButton2.onClick.AddListener(() => dialogueManager.StartSequence(_choice2.response, _onSubsequenceEnd));
        choiceButton3.onClick.AddListener(() => dialogueManager.StartSequence(_choice3.response, _onSubsequenceEnd));
    }
}
