using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Threading.Tasks;

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
    //public float animationDelay = .2f; // Forgot what this did

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
            && (Input.GetKeyDown(KeyCode.E)
            || Input.GetKeyDown(KeyCode.Mouse0)
            || Input.GetKeyDown(KeyCode.Return)))
        {
            nextSentence();
        }
    }

    public void startDialogue(DialogueEntry[] dialogueSet, UnityEvent OnConversationEnd = null)
    {
        Debug.Log("Dialogue");
        this.OnConversationEnd = OnConversationEnd;
        sentences = dialogueSet;

        inConversation = true;
        FindObjectOfType<PlayerController>().isInteracting = true;

        showDialogueCanvas();
        nextSentence();
    }

    void nextSentence()
    {
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

    async void endConversation()
    {
        inConversation = false;
        blockIndex = 0;
        hideDialogueCanvas();
        OnConversationEnd?.Invoke();

        await Task.Delay(1);
        FindObjectOfType<PlayerController>().isInteracting = false;
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
