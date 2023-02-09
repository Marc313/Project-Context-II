using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public DialogueTrigger firstDialogue;
    protected DialogueTrigger currentDialogue;

    protected virtual void Start()
    {
        if (firstDialogue != null)
            currentDialogue = firstDialogue;
    }

    [HideInInspector]
    public void OnInteract()
    {
        if (currentDialogue == null) return;
        currentDialogue.TriggerDialogue();
    }

    public void SetCurrentDialogue(DialogueTrigger _newDialogue)
    {
        currentDialogue = _newDialogue;
    }
}
