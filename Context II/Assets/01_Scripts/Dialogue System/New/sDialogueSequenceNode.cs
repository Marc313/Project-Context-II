using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Sequence Node")]
public class sDialogueSequenceNode : ADialogueNode
{
    [Tooltip("Name of the current speaker")]
    public string speaker;
    public DialogueLine[] dialogueLines;

    public int lineIndex { get; private set; }

    public override DialogueLine Next()
    {
        DialogueLine currentLine = dialogueLines[lineIndex];
        lineIndex++;
        return currentLine;
    }

    public override bool HasNext()
    {
        return lineIndex < dialogueLines.Length;
    }

    /// <summary>
    /// Resets the index
    /// </summary>
    public override void Reset()
    {
        lineIndex = 0;
    }

}