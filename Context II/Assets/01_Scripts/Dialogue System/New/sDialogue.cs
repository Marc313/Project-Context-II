using newDialogue;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialogues/Dialogue")]
public class sDialogue : ScriptableObject
{
    public List<ADialogueNode> dialogueNodes;

    [HideInInspector] public int NodeIndex;
    private ADialogueNode currentNode;

    [HideInInspector] public bool isDone = false;

    public void Play(UnityEvent _onConversationEnd = null)
    {
        Reset();
        FindObjectOfType<DialogueManager>().StartDialogue(this, _onConversationEnd);
    }

    public bool HasNext()
    {
        return NodeIndex < dialogueNodes.Count;
    }

    public void Reset()
    {
        isDone = false;
        NodeIndex = 0;
        foreach (ADialogueNode node in dialogueNodes)
        {
            node.Reset();
        }
    }
}
