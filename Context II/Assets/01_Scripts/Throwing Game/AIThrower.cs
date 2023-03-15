using UnityEngine;

public class AIThrower : Thrower
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Throwing Offset")]
    [SerializeField] private float maxOffsetAmount = 0.5f;

    [Header("Throwing Frequency")]
    [SerializeField] private float minTimerLength = 0.5f;
    [SerializeField] private float maxTimerLength = 1.5f;
    private float randomTimer;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        randomTimer -= Time.deltaTime;
        if (randomTimer < 0.0f) 
        { 
            Activate();
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        randomTimer = Random.Range(minTimerLength, maxTimerLength);
    }

    public override void CreateProjectile(Vector3 _targetDirection)
    {
        base.CreateProjectile(_targetDirection);
        Debug.Log("AIActivate");
    }

    public override Vector3 CalculateTargetDirection()
    {
        Vector3 directionToTarget = (target.position - startPos.position).normalized;
        Vector2 perp = Vector2.Perpendicular(new Vector2(directionToTarget.x, directionToTarget.z));
        Vector3 offsetAxis = new Vector3(perp.x, 0, perp.y);
        Vector3 offset = offsetAxis * Random.Range(-maxOffsetAmount, maxOffsetAmount);

        return directionToTarget - offset;
    }
}
