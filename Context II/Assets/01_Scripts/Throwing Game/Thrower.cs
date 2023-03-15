using UnityEngine;

public abstract class Thrower : MonoBehaviour
{
    public GameObject throwablePrefab;
    public Transform startPos;
    public bool destroyOnImpact;
    protected Projectile currentThrowable;

    public void Activate()
    {
        CreateProjectile(CalculateTargetDirection());
    }

    public abstract Vector3 CalculateTargetDirection();

    public virtual void CreateProjectile(Vector3 _targetDirection)
    {
        currentThrowable = Instantiate(throwablePrefab, startPos.position, Quaternion.identity).GetComponent<Projectile>();
        currentThrowable.SetTargetDirection(_targetDirection);
        currentThrowable.destroyOnImpact = destroyOnImpact;
    }
}
