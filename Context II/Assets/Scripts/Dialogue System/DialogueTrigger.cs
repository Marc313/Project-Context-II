using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueEntry[] CompleteDialogue;
    public UnityEvent OnConversationEnd;

    public bool playOnStart;

    private DialogueController dialogueController;

    private void Start()
    {
        if (playOnStart) { TriggerDialogue(); }
    }

    public void TriggerDialogue()
    {
        if (dialogueController == null)
        {
            dialogueController = ServiceLocator.GetService<DialogueController>();
        }

        dialogueController.startDialogue(CompleteDialogue, OnConversationEnd);
    }

/*    // Check if the target walked into this trigger
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats target = FindObjectOfType<PlayerStats>();
        if (target != null)
        {
            TriggerDialogue();
        }
    }*/
}
