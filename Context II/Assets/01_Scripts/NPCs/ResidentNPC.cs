using UnityEngine;
using oldDialogue;

public class ResidentNPC : NPC
{
    // Voor later:
    // public DialogueTrigger[] dialogues
    // private currentDialogueIndex

    public DialogueTrigger firstRepeatDialogue;

    public DialogueTrigger correctQuestionDialogue;
    public DialogueTrigger wrongQuestionDialogue;

    public Question correspondingQuestion;

    private QuestionManager questionManager;

    public void AskCurrentQuestion()
    {
        GetQuestionManager();
        AskQuestion(questionManager.currentQuestion);
    }

    private void AskQuestion(Question _question)
    {
        if (_question = correspondingQuestion)
        {
            currentDialogue = correctQuestionDialogue;
        }
        else
        {
            currentDialogue = wrongQuestionDialogue;
        }

        currentDialogue.TriggerDialogue();
    }

    private void GetQuestionManager()
    {
        if (questionManager == null)
        {
            questionManager = ServiceLocator.GetService<QuestionManager>();
        }
    }
}
