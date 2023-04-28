using MarcoHelpers;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : Singleton<TimeManager>
{
    public UnityEvent OnTimerEnds;
    public UnityEvent OnCutsceneEnd;
    public float TimePercent => currentTime / totalGameDurationSeconds;
    [SerializeField] private float totalGameDurationSeconds = 30;
    private float currentTime;
    private bool hasEnded;
    private bool playCutscene = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (!hasEnded && currentTime > totalGameDurationSeconds)
        {
            EventSystem.RaiseEvent(EventName.MENU_OPENED);
            Debug.Log("Game End");
            hasEnded = true;

            OnTimerEnds?.Invoke();

            if (playCutscene)
            {
                FindObjectOfType<EndCutscene>().Play(OnCutsceneEnd);

            }
            else
            {
                OnCutsceneEnd?.Invoke();
            }
        }
    }
}
