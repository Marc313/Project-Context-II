using MarcoHelpers;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace newDialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Letter Animation")]
        public float dialogueSpeed = 0.05f;

        private sDialogueSequenceNode sequence;
        private Action onSequenceEnd;
        private UnityEvent onConversationEnd;

        private sDialogue dialogue;
        private DialogueLine currentLine;

        private bool inSequence;
        private bool inTextCoroutine;

        private PlayerController player;
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
                && (Input.GetKeyDown(KeyCode.Mouse0)
                || Input.GetKeyDown(KeyCode.Return)))
            {
                OnInput();
            }
        }

        private void OnInput()
        {
            if (inTextCoroutine)
            {
                StopAllCoroutines();
                inTextCoroutine= false;
                uiManager.dialogueText.text = currentLine.textBlock;
            }
            else
            {
                NextLine();
            }
        }

        public void StartDialogue(sDialogue _dialogue, UnityEvent _onConversationEnd = null)
        {
            onConversationEnd = _onConversationEnd;
            dialogue = _dialogue;
            NextNode();
            EventSystem.RaiseEvent(EventName.MENU_OPENED);
        }

        public void NextNode()
        {
            if (dialogue.HasNext())
            {
                ADialogueNode currentNode = dialogue.dialogueNodes[dialogue.NodeIndex];
                currentNode.Play(this);

                dialogue.NodeIndex++;
            }
            else
            {
                EndDialogue();
            }
        }

        public void StartSequence(sDialogueSequenceNode _sequence, Action _onSequenceEnd = null)
        {
            if (_sequence == null)
            {
                EndSequence();
                return;
            };

            onSequenceEnd = _onSequenceEnd;
            sequence = _sequence;

            uiManager.ShowDialogueCanvas();
            uiManager.SwitchToSequence();
            uiManager.QuesitionUI.SetActive(false); //Move

            inSequence = true;
            FindPlayerController();
            if (player != null) player.isInteracting = true;

            uiManager.dialogueName.text = _sequence.speaker;

            NextLine();
        }
       
        public void DisplayChoice(sDialogueChoiceNode _choiceNode, Action _onSubsequenceEnd = null)
        {
            uiManager.ShowDialogueCanvas();
            uiManager.SwitchToChoice();
            uiManager.dialogueName.text = _choiceNode.speaker;
            uiManager.dialogueText.text = "";
            StartCoroutine(DisplayLine(_choiceNode.choiceLine));
            uiManager.ConnectButtons(_choiceNode.choices, _onSubsequenceEnd);
        }

        private void NextLine()
        {
            uiManager.dialogueText.text = "";
            StopAllCoroutines();

            if (sequence.HasNext())
            {
                currentLine = sequence.Next();

                if (currentLine != null)   // Remove
                {
                    StartCoroutine(DisplayLine(currentLine));
                }
            }
            else
            {
                EndSequence();
            }
        }

        private void EndSequence()
        {
            onSequenceEnd?.Invoke();
            inSequence = false;
            NextNode();
        }

        private void EndDialogue()
        {
            uiManager.HideDialogueCanvas();
            sequence.Reset();
            onConversationEnd?.Invoke();

            EventSystem.RaiseEvent(EventName.MENU_CLOSED);

/*            FindPlayerController();
            if (player != null) player.isInteracting = false;*/
        }

        IEnumerator DisplayLine(DialogueLine _line)
        {
            inTextCoroutine = true;
            TMP_Text textBox = uiManager.dialogueText;
            foreach (char character in _line.textBlock)
            {
                //uiManager.QuesitionUI.SetActive(false);

                textBox.text += character;
                yield return new WaitForSeconds(dialogueSpeed);
            }
            inTextCoroutine = false;
        }

        // Bad lol
        private void FindPlayerController()
        {
            if (player == null)
                player = FindObjectOfType<PlayerController>();
        }
    }
}