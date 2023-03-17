using UnityEngine;

public abstract class Thrower : MonoBehaviour
{
    public GameObject throwablePrefab;
    public Transform startPos;
    public bool destroyOnImpact;
    protected Propje currentThrowable;
    protected abstract bool countsForScore { get; }

    public void Activate()
    {
        CreateProjectile(CalculateTargetDirection());
    }

    public abstract Vector3 CalculateTargetDirection();

    public virtual void CreateProjectile(Vector3 _targetDirection)
    {
        currentThrowable = Instantiate(throwablePrefab, startPos.position, Quaternion.identity).GetComponent<Propje>();
        currentThrowable.SetTargetDirection(_targetDirection);
        currentThrowable.countsToScore = countsForScore;
        currentThrowable.destroyOnImpact = destroyOnImpact;
    }
}
