using MarcoHelpers;
using System;
using System.Collections;
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
        private Action onSequenceEnd;
        private UnityEvent onConversationEnd;

        private sDialogue dialogue;
        private DialogueLine currentLine;

        private bool inSequence;
        private bool inTextCoroutine;

        private PlayerController player;
        private UIManagerTG uiManager { 
                get { if (ui == null) ui = ServiceLocator.GetService<UIManagerTG>(); 
                    return ui; } 
                set { ui = value; } }
        private UIManagerTG ui;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
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

        private void GetUIManager()
        {
            //convincedBar = ServiceLocator.GetService<UIManagerTG>();
        }

        private void OnInput()
        {
            if (inTextCoroutine)
            {
                StopAllCoroutines();
                inTextCoroutine= false;
                uiManager.windowText.text = currentLine.textBlock;
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

            inSequence = true;
            FindPlayerController();
            if (player != null) player.isInteracting = true;

            uiManager.windowTitle.text = _sequence.speaker;

            NextLine();
        }
       
        public void DisplayChoice(sDialogueChoiceNode _choiceNode, Action _onSubsequenceEnd = null)
        {
/*            convincedBar.ShowDialogueCanvas();
            convincedBar.SwitchToChoice();
            convincedBar.dialogueName.text = _choiceNode.speaker;
            convincedBar.dialogueText.text = "";
            StartCoroutine(DisplayLine(_choiceNode.choiceLine));
            convincedBar.ConnectButtons(_choiceNode.choices, _onSubsequenceEnd);*/
        }

        private void NextLine()
        {
            uiManager.windowText.text = "";
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
            TMP_Text textBox = uiManager.windowText;
            foreach (char character in _line.textBlock)
            {
                //convincedBar.QuesitionUI.SetActive(false);

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