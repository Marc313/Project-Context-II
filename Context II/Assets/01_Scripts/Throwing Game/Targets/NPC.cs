using System.Threading;
using TMPro;
using UnityEngine;

namespace ThrowingGame
{
    public abstract class NPCThrowing : MonoBehaviour, ITarget
    {
        public enum Side { Citizen = 0, Ceo = 1 };
        public abstract Side side { get; }
        public bool isPlayer;
        public bool hitAnimations = true;

        [Header("UI")]
        public TMP_Text textElement;
        public float textDuration;

        [Header("References")]
        public Transform targetPos;

        private ConvinceMeter convincedBar;
        private float randomTimer;
        private Animator anim;

        [SerializeField] private float minTimerLength = 5.0f;
        [SerializeField] private float maxTimerLength = 15.0f;

        private void Awake()
        {
            if (textElement == null)
                textElement = GetComponentInChildren<TMP_Text>();

            anim = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            convincedBar = ServiceLocator.GetService<ConvinceMeter>();
            StartTimer();
        }

        private void Update()
        {
            if (!isPlayer)
            {
                randomTimer -= Time.deltaTime;
                if (randomTimer < 0.0f)
                {
                    PlayCheeringAnimation();
                    ResetTimer();
                }
            }
        }

        private void PlayCheeringAnimation()
        {
            if (!anim.IsInTransition(0) && 
                (!anim.GetCurrentAnimatorStateInfo(0).IsName("Smack2") 
                || !anim.GetCurrentAnimatorStateInfo(0).IsName("ImpactSmall")
                || !anim.GetCurrentAnimatorStateInfo(0).IsName("ImpactLargeGut")))
            {
                anim.CrossFade("Cheering", 0.2f, 0);
            }
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

        public void OnHit(string _word, bool _isPropFromPlayer)
        {
            if (isPlayer)
            {
                AudioManager.Instance.PlayOofSound();
                Debug.Log("Player Hit");
            }

            //if (_word == null && _word == string.Empty) return;
            if (hitAnimations) PlayHitAnimation(_isPropFromPlayer);

            ShowTextObject(_word);
            if (isPlayer) _isPropFromPlayer = true;   // Propje against player should also count more
            convincedBar.ChangeConvinceValue(side, _isPropFromPlayer);

        }

        private void PlayHitAnimation(bool _isPropFromPlayer)
        {
            if (/*!anim.IsInTransition(0) &&*/ true || 
                (!anim.GetCurrentAnimatorStateInfo(0).IsName("ImpactSmall")
                || !anim.GetCurrentAnimatorStateInfo(0).IsName("ImpactLargeGut")))
            {
                string clipName = (_isPropFromPlayer && !isPlayer) ? "ImpactLargeGut" : "ImpactSmall";
                anim.CrossFade(clipName, 0.01f, 0);
            }
        }

        private void StartTimer()
        {
            randomTimer = Random.Range(1.0f, maxTimerLength);
        }

        private void ResetTimer()
        {
            randomTimer = Random.Range(minTimerLength, maxTimerLength);
        }
    }
}

