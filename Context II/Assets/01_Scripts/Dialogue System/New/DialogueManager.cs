using oldDialogue;
using System;
using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace newDialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Letter Animation")]
        public float dialogueSpeed = 0.05f;

        private sDialogueSequenceNode sequence;
        private System.Action onConversationEnd;

        private bool inConversation;
        private bool inSequence;

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
            if (inSequence
                && (Input.GetKeyDown(KeyCode.E)
                || Input.GetKeyDown(KeyCode.Mouse0)
                || Input.GetKeyDown(KeyCode.Return)))
            {
                NextLine();
            }
        }

        public void StartSequence(sDialogueSequenceNode _dialogue, System.Action _onConversationEnd = null)
        {
            if (_dialogue == null)
            {
                endSequence();
                return;
            };

            onConversationEnd = _onConversationEnd;
            sequence = _dialogue;

            ShowDialogueCanvas();
            uiManager.QuesitionUI.SetActive(false); //Move

            inConversation = true;
            inSequence = true;
            FindPlayerController();
            if (player != null) player.isInteracting = true;

            uiManager.dialogueName.text = _dialogue.speaker;

            NextLine();
        }

        private void NextLine()
        {
            uiManager.dialogueText.text = "";
            StopAllCoroutines();

/*            ADialogueNode currentNode;
            if (sequence.HasNext())
            {
                currentNode = sequence.Next();

                uiManager.dialogueName.text = "Name";

                if (currentNode.)
                DialogueLine line = currentNode.Next();

                if (line != null)
                {
                    StartCoroutine(DisplayLine(line));
                }
                // Else do nothing, wait until player presses button.
            }
            else
            {
                endSequence();
            }*/

            if (sequence.HasNext())
            {
                DialogueLine nextLine = sequence.Next();

                if (nextLine != null)   // Remove
                {
                    StartCoroutine(DisplayLine(nextLine));
                }
            }
            else
            {
                endSequence();
            }
        }

        private void endSequence()
        {
            inConversation = false;
            inSequence = false;
            HideDialogueCanvas();
            sequence.Reset();
            onConversationEnd?.Invoke();

            FindPlayerController();
            if (player != null) player.isInteracting = false;
        }

        IEnumerator DisplayLine(DialogueLine _line)
        {
/*            TMP_Text textBox = uiManager.dialogueText;
*/            foreach (char character in _line.textBlock)
            {
                uiManager.QuesitionUI.SetActive(false);

                uiManager.dialogueText.text += character;
                yield return new WaitForSeconds(dialogueSpeed);
            }
        }

        // Leave to UI Manager
        private void ShowDialogueCanvas()
        {
            uiManager.SwitchToSequence();
            uiManager.dialogueCanvas.gameObject.SetActive(true);
        }

        // Leave to UI Manager
        private void HideDialogueCanvas()
        {
            uiManager.dialogueCanvas.gameObject.SetActive(false);
            uiManager.dialogueSequenceName.text = string.Empty;   // string.Empty is ""
            uiManager.dialogueSequenceText.text = string.Empty;
        }

        // Bad lol
        private void FindPlayerController()
        {
            if (player == null)
                player = FindObjectOfType<PlayerMovement>();
        }

        public void DisplayChoice(sDialogueChoiceNode _choiceNode, System.Action _onSubsequenceEnd = null)
        {
            ShowDialogueCanvas();
            uiManager.SwitchToChoice();
            uiManager.dialogueName.text = _choiceNode.speaker;
            uiManager.dialogueText.text = "";
            StartCoroutine(DisplayLine(_choiceNode.choiceLine));
            uiManager.ConnectButtons(_choiceNode.choices[0], _choiceNode.choices[1], _choiceNode.choices[2], _onSubsequenceEnd);
        }
    }
}