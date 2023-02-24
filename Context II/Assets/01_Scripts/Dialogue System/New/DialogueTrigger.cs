using UnityEngine;
using UnityEngine.Events;

namespace newDialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public sDialogue CompleteDialogue;
        public UnityEvent OnConversationEnd;

        public bool playOnStart;

        private DialogueManager dialogueManager;

        private void Start()
        {
            if (playOnStart) { TriggerDialogue(); }
        }

        public void TriggerDialogue()
        {
            CompleteDialogue.Play();
            /*if (dialogueManager == null)
            {
                dialogueManager = ServiceLocator.GetService<DialogueManager>();
            }

            dialogueManager.StartSequence(CompleteDialogue.dialogueNodes[0] as sDialogueSequenceNode, OnConversationEnd);*/
        }
    }
}
