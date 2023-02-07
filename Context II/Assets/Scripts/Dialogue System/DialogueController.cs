using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

// Imported from an older project that used Brackeys' tutorial for this script
public class DialogueController : MonoBehaviour
{
    // UI Elements //
    [Header("UI Elements")]
    public Canvas dialogueCanvas;
    public TMP_Text dialogueName;
    public TMP_Text dialogueText;


    //public Image dialogueBox;

    [Header("Letter Animation")]
    public float dialogueSpeed = 0.05f;
    public float animationDelay = .2f; // Forgot what this did

    private DialogueEntry[] sentences;
    private UnityEvent OnConversationEnd;

    private int blockIndex;
    private bool inConversation;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    void Update()
    {
        if (inConversation
            && (Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.Mouse0)
            || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            nextSentence();
        }
    }

    public void startDialogue(DialogueEntry[] dialogueSet, UnityEvent OnConversationEnd = null)
    {
        this.OnConversationEnd = OnConversationEnd;
        sentences = dialogueSet;

        inConversation = true;

        showDialogueCanvas();
        nextSentence();
    }

    void nextSentence()
    {
        Debug.Log("BlockIndex: " + blockIndex);
        dialogueText.text = "";
        StopAllCoroutines();

        if (blockIndex < sentences.Length)
        {
            blockIndex++;
            StartCoroutine(showText());
        }
        else
        {
            endConversation();
        }
    }

    void endConversation()
    {
        inConversation = false;
        hideDialogueCanvas();
        OnConversationEnd?.Invoke();
    }

    IEnumerator showText()
    {
        DialogueEntry currentDialogue = sentences[blockIndex - 1];

        dialogueName.text = currentDialogue.name;
        foreach (char character in currentDialogue.textBlock)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    void showDialogueCanvas()
    {
        dialogueCanvas.gameObject.SetActive(true);
    }

    void hideDialogueCanvas()
    {
        dialogueCanvas.gameObject.SetActive(false);
        dialogueName.text = string.Empty;   // string.Empty is ""
        dialogueText.text = string.Empty;
    }
}
