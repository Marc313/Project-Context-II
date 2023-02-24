using newDialogue;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue")]
public class sDialogue : ScriptableObject
{
    public List<ADialogueNode> dialogueNodes;

    private int NodeIndex;
    private ADialogueNode currentNode;

    [HideInInspector] public bool isDone = false;

    public void Play()
    {
        Reset();
        NextNode();
    }

    public void NextNode()
    {
        if (HasNext())
        {
            currentNode = dialogueNodes[NodeIndex];

            if (currentNode is sDialogueSequenceNode) { FindObjectOfType<DialogueManager>().StartSequence(currentNode as sDialogueSequenceNode, NextNode) ; }
            else if (currentNode is sDialogueChoiceNode) { FindObjectOfType<DialogueManager>().DisplayChoice(currentNode as sDialogueChoiceNode, NextNode); }

            NodeIndex++;
        }
        else
        {
            // End
        }
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
