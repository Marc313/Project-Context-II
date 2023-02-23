using oldDialogue;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

namespace newDialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Letter Animation")]
        public float dialogueSpeed = 0.05f;

        private sDialogue dialogue;
        private UnityEvent onConversationEnd;

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

        public void startDialogue(sDialogue _dialogue, UnityEvent _onConversationEnd = null)
        {
            uiManager.QuesitionUI.SetActive(false);
            //Debug.Log("Dialogue");
            onConversationEnd = _onConversationEnd;
            dialogue = _dialogue;

            inConversation = true;
            FindPlayerController();
            if (player != null) player.isInteracting = true;

            showDialogueCanvas();
            nextSentence();
        }

        public void continueDialogue(sDialogueSequenceNode _node)
        {
            StartCoroutine(showText(_node.Next()));
        }

        private void nextSentence()
        {
            uiManager.dialogueText.text = "";
            StopAllCoroutines();

/*            ADialogueNode currentNode;
            if (dialogue.HasNext())
            {
                currentNode = dialogue.Next();

                uiManager.dialogueName.text = "Name";

                if (currentNode.)
                DialogueLine line = currentNode.Next();

                if (line != null)
                {
                    StartCoroutine(showText(line));
                }
                // Else do nothing, wait until player presses button.
            }
            else
            {
                endConversation();
            }*/

            DialogueLine nextLine = dialogue.NextDialogueLine();

            if (nextLine != null)
            {
                StartCoroutine(showText(nextLine));
            }
            // Else do nothing, wait until player presses button.


            if (dialogue.isDone)
            {
                endConversation();
            }
        }

        private void endConversation()
        {
            inConversation = false;
            //blockIndex = 0;
            hideDialogueCanvas();
            dialogue.Reset();
            onConversationEnd?.Invoke();

            FindPlayerController();
            if (player != null) player.isInteracting = false;
        }

        IEnumerator showText(DialogueLine _line)
        {
            foreach (char character in _line.textBlock)
            {
                uiManager.QuesitionUI.SetActive(false);

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
}