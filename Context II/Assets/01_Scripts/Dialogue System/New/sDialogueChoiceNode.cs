using newDialogue;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Choice Node")]
public class sDialogueChoiceNode : ADialogueNode
{
    public string speaker;
    public DialogueLine choiceLine;
    public Choice[] choices = new Choice[3];

    private bool isLineShown = false;

    public override void Play(DialogueManager _dm)
    {
        _dm.DisplayChoice(this);
    }

    public override void Reset()
    {
        isLineShown= false;

        sDialogueSequenceNode[] sequences = choices.Select(c => c.response).Where(c => c!= null).ToArray();
        Debug.Log(sequences.ToString());
        foreach (sDialogueSequenceNode sequence in sequences)
        {
            sequence.Reset();
        }
    }

}
