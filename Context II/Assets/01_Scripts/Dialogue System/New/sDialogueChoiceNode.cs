using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Choice Node")]
public class sDialogueChoiceNode : ADialogueNode
{
    public DialogueLine choiceLine;
    public Choice[] choices = new Choice[3];

    public bool isLineShown = false;
    public bool isChoiceClicked = false;

    public override DialogueLine Next()
    {
        if (!isLineShown)
        {
            isLineShown = true;
            var ui = FindObjectOfType<UIManager>();
            ui.SwitchToChoice();
            ui.ConnectButtons(choices[0], choices[1], choices[2]);

            return choiceLine;
        }

        return null;
    }

    public override bool HasNext()
    {
        return !isChoiceClicked;
    }

    public override void Reset()
    {
        isLineShown= false;
        isChoiceClicked= false;

        sDialogueSequenceNode[] sequences = choices.Select(c => c.response).Where(c => c!= null).ToArray();
        Debug.Log(sequences.ToString());
        foreach (sDialogueSequenceNode sequence in sequences)
        {
            sequence.Reset();
        }
    }
}
