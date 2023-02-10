using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Threading.Tasks;

// Imported from an older project that used Brackeys' tutorial for this script
public class DialogueController : MonoBehaviour
{
    //public Image dialogueBox;

    [Header("Letter Animation")]
    public float dialogueSpeed = 0.05f;
    //public float animationDelay = .2f; // Forgot what this did

    private DialogueEntry[] sentences;
    private UnityEvent OnConversationEnd;

    private int blockIndex;
    private bool inConversation;

    private PlayerMovement player;
    private UIManager uiManager;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void Start()
    {
        uiManager = ServiceLocator.GetService<UIManager>();
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
        FindPlayerController();
        if (player != null) player.isInteracting = true;

        showDialogueCanvas();
        nextSentence();
    }

    private void nextSentence()
    {
        uiManager.dialogueText.text = "";
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

    private async void endConversation()
    {
        inConversation = false;
        blockIndex = 0;
        hideDialogueCanvas();
        OnConversationEnd?.Invoke();

        await Task.Delay(1);
        FindPlayerController();
        if (player != null) player.isInteracting = false;
    }

    IEnumerator showText()
    {
        DialogueEntry currentDialogue = sentences[blockIndex - 1];

        uiManager.dialogueName.text = currentDialogue.name;
        foreach (char character in currentDialogue.textBlock)
        {
            uiManager.dialogueText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    private void showDialogueCanvas()
    {
        uiManager.dialogueCanvas.gameObject.SetActive(true);
    }

    private void hideDialogueCanvas()
    {
        uiManager.dialogueCanvas.gameObject.SetActive(false);
        uiManager.dialogueName.text = string.Empty;   // string.Empty is ""
        uiManager.dialogueText.text = string.Empty;
    }

    private void FindPlayerController()
    {
        if (player == null)
        player = FindObjectOfType<PlayerMovement>();
    }
}
