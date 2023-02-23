using System.Security.Cryptography;
using UnityEngine;

public abstract class ADialogueNode : ScriptableObject
{
    public abstract DialogueLine Next();
    public abstract bool HasNext();
    public abstract void Reset();
}
