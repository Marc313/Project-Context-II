using UnityEngine;

[CreateAssetMenu(menuName = "Words/Wordlist")]
public class sWordList : ScriptableObject
{
    public string[] words;

    public string GetRandomWord()
    {
        return words.GetRandomElement();
    }
}
