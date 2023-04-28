using UnityEngine;

[CreateAssetMenu(menuName = "Article")]
public class sArticle : ScriptableObject
{
    public string title;
    [Multiline] public string content;
    [Multiline] public string endingText;
}
