using newDialogue;
using System.Security.Cryptography;
using UnityEngine;

public abstract class ADialogueNode : ScriptableObject
{
    public abstract void Play(DialogueManager _dm);
    public abstract void Reset();
}
