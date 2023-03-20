using UnityEngine;

[CreateAssetMenu(menuName = "Words/Wordlist")]
public class sWordList : ScriptableObject
{
    public Argument[] words;

    public Argument GetRandomWord()
    {
        return words.GetRandomElement();
    }
}

[System.Serializable]
public class Argument 
{
    public string word;
    public string description;
}
