using TMPro;
using UnityEngine;

namespace ThrowingGame
{
    public abstract class NPCThrowing : MonoBehaviour, ITarget
    {
        public enum Side { Citizen = 0, Ceo = 1 };
        public abstract Side side { get; }

        [Header("UI")]
        public TMP_Text textElement;
        public float textDuration;

        [Header("References")]
        public Transform targetPos;

        private ConvinceMeter convincedBar;

        private void Awake()
        {
            if (textElement == null)
                textElement = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            convincedBar = ServiceLocator.GetService<ConvinceMeter>();
        }

        public void ShowTextObject(string _word)
        {
            textElement.text = _word;
            textElement.gameObject.SetActive(true);
            Invoke(nameof(DisableText), textDuration);
        }

        public void DisableText()
        {
            textElement.gameObject.SetActive(false);
        }

        public void OnHit(string _word)
        {
            ShowTextObject(_word);
            convincedBar.ChangeConvinceValue(side);
        }
    }
}

