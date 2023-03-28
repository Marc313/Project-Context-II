using UnityEngine;
using UnityEngine.Events;

public class TimeManager : Singleton<TimeManager>
{
    public UnityEvent onTimerEnd;
    public float TimePercent => currentTime / totalGameDurationSeconds;
    [SerializeField] private float totalGameDurationSeconds = 30;
    private float currentTime;
    private bool hasEnded;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (!hasEnded && currentTime > totalGameDurationSeconds)
        {
            Debug.Log("Game End");
            hasEnded = true;
            onTimerEnd?.Invoke();
        }
    }
}
