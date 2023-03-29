using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public TMP_Text textElement;
    public float textDuration;

    private void Awake()
    {
        if (textElement == null)
        textElement = GetComponentInChildren<TMP_Text>();
    }

    public void ShowTextObject(string _word)
    {
        if (_word == null && _word == string.Empty) return;

        textElement.text = _word;
        textElement.gameObject.SetActive(true);
        Invoke(nameof(DisableText), textDuration);
    }

    public void DisableText()
    {
        textElement.gameObject.SetActive(false);
    }
}
