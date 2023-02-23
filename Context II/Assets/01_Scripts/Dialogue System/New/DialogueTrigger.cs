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
            CompleteDialogue.Reset();
            if (playOnStart) { TriggerDialogue(); }
        }

        public void TriggerDialogue()
        {
            if (dialogueManager == null)
            {
                dialogueManager = ServiceLocator.GetService<DialogueManager>();
            }

            dialogueManager.startDialogue(CompleteDialogue, OnConversationEnd);
        }
    }
}
