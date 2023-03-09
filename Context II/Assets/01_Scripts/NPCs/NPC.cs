using UnityEngine;
using newDialogue;

public class NPC : MonoBehaviour, IInteractable
{
    public InteractIndicator indicator;
    public DialogueTrigger firstDialogue;
    [SerializeField] private Token token;
    protected DialogueTrigger currentDialogue;

    private bool tokenObtained;

    protected virtual void Start()
    {
        if (firstDialogue != null)
            currentDialogue = firstDialogue;

        PlayerLogic player = FindObjectOfType<PlayerLogic>();
        if (player != null && token != null)
        {
            currentDialogue.onConversationEnd.AddListener(() => player.ObtainItem(token));
            currentDialogue.onConversationEnd.AddListener(() => currentDialogue.onConversationEnd.RemoveAllListeners());
        }
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
