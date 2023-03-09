using UnityEngine;
using UnityEngine.Events;

namespace newDialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public sDialogue completeDialogue;
        public UnityEvent onConversationEnd;

        public bool playOnStart;

        private DialogueManager dialogueManager;

        private void Start()
        {
            if (playOnStart) { TriggerDialogue(); }
        }

        public void TriggerDialogue()
        {
            completeDialogue.Play(onConversationEnd);
        }
    }
}
