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
        private Action onSequenceEnd;
        private UnityEvent onConversationEnd;

        private sDialogue dialogue;

        private bool inDialogue;
        private bool inSequence;

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
                && (Input.GetKeyDown(KeyCode.E)
                || Input.GetKeyDown(KeyCode.Mouse0)
                || Input.GetKeyDown(KeyCode.Return)))
            {
                NextLine();
            }
        }

        public void StartDialogue(sDialogue _dialogue, UnityEvent _onConversationEnd = null)
        {
            onConversationEnd = _onConversationEnd;
            dialogue = _dialogue;
            NextNode();
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

        public void StartSequence(sDialogueSequenceNode _sequence, System.Action _onSequenceEnd = null)
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

            inDialogue = true;
            inSequence = true;
            FindPlayerController();
            if (player != null) player.isInteracting = true;

            uiManager.dialogueName.text = _sequence.speaker;

            NextLine();
        }
       
        public void DisplayChoice(sDialogueChoiceNode _choiceNode, System.Action _onSubsequenceEnd = null)
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
                DialogueLine nextLine = sequence.Next();

                if (nextLine != null)   // Remove
                {
                    StartCoroutine(DisplayLine(nextLine));
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
            inDialogue = false;
            sequence.Reset();
            onConversationEnd?.Invoke();

            FindPlayerController();
            if (player != null) player.isInteracting = false;
        }

        IEnumerator DisplayLine(DialogueLine _line)
        {
            TMP_Text textBox = uiManager.dialogueText;
            foreach (char character in _line.textBlock)
            {
                //uiManager.QuesitionUI.SetActive(false);

                textBox.text += character;
                yield return new WaitForSeconds(dialogueSpeed);
            }
        }

        // Bad lol
        private void FindPlayerController()
        {
            if (player == null)
                player = FindObjectOfType<PlayerController>();
        }
    }
}