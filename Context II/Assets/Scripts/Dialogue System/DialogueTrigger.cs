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

/*    // Check if the player walked into this trigger
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        if (player != null)
        {
            TriggerDialogue();
        }
    }*/
}
