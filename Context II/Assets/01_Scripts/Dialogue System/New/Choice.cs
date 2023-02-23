using UnityEngine;

[System.Serializable]
public class Choice
{
    [Tooltip("How the option is displayed. Keep this short!")]
    public string optionName;
    public sDialogueSequenceNode response;
}