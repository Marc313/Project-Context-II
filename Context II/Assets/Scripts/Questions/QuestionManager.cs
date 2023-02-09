using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public List<Question> questions = new List<Question>();

    private int questionIndex;
    [HideInInspector] public Question currentQuestion;

    private UIManager uiManager;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void Start()
    {
        uiManager = ServiceLocator.GetService<UIManager>();
        StartQuestionRound();

    }
/*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartQuestionRound();
        }
    }*/

    public void StartQuestionRound()
    {
        if (questions == null || questions.Count == 0) return;
        
        questionIndex = 0;
        // Show UI Screen
        ShowNextQuestion();
    }

    public void ShowNextQuestion()
    {
        if (questionIndex == questions.Count) { EndQuestionRound(); return; }

        currentQuestion = questions[questionIndex];
        uiManager.ChangeQuestionText("Question: " + currentQuestion.questionText);
        questionIndex++;
    }

    public void EndQuestionRound()
    {
        // Hide UI
        uiManager.QuesitionUI.SetActive(false);
    }
    
}
