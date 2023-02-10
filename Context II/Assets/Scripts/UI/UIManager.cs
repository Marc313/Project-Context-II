using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dialogue System")]
    public Canvas dialogueCanvas;
    public TMP_Text dialogueName;
    public TMP_Text dialogueText;

    [Header("Question System")]
    public GameObject QuesitionUI;
    public TMP_Text QuestionText;

    [Header("Designing")]
    public TMP_Text ProgressCounter;


    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void Start()
    {
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
}
