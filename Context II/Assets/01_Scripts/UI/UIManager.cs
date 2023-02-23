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
    public TMP_Text dialogueName;
    public TMP_Text dialogueText;
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

    private DialogueManager dialogueManager;


    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void Start()
    {
        dialogueManager = ServiceLocator.GetService<DialogueManager>();
        // Hide All UI?
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
    }

    public void SwitchToChoice()
    {
        dialogueSequenceUI.SetActive(false);
        dialogueChoiceUI.SetActive(true);
    }

    public void ConnectButtons(Choice _choice1, Choice _choice2, Choice _choice3)
    {
        choiceButton1.GetComponentInChildren<TMP_Text>().text = _choice1.optionName;
        choiceButton2.GetComponentInChildren<TMP_Text>().text = _choice2.optionName;
        choiceButton3.GetComponentInChildren<TMP_Text>().text = _choice3.optionName;

        //choice1.onClick += dialogueManager.startDialogue(_response1);
    }
}
