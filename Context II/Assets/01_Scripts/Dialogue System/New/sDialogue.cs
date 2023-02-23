using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue")]
public class sDialogue : ScriptableObject
{
    public List<ADialogueNode> dialogueNodes;

    public int NodeIndex { get; private set; } = 0;
    public ADialogueNode currentNode => dialogueNodes[NodeIndex];

    [HideInInspector] public bool isDone = false;

    public ADialogueNode NextNode()
    {
        NodeIndex++;
        ADialogueNode current = currentNode;
        return current;
    }

    public bool HasNext()
    {
        return NodeIndex < dialogueNodes.Count;
    }

    public DialogueLine NextDialogueLine()
    {
        if (currentNode.HasNext())
        {
            return currentNode.Next();
        }
        else
        {
            if (HasNext())
            {
                ADialogueNode node = NextNode();
                return node.Next();
            }
            else
            {
                isDone = true;
                return null;
            }
        }
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
