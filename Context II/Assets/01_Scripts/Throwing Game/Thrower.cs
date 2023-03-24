using MarcoHelpers;
using UnityEngine;

public abstract class Thrower : MonoBehaviour
{
    public GameObject throwablePrefab;
    public Transform startPos;
    public bool destroyOnImpact;

    protected Propje currentThrowable;
    protected abstract bool countsForScore { get; }
    protected bool isActive;

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

    public void OnEnable()
    {
        EventSystem.Subscribe(EventName.MENU_OPENED, DisableSelf);
        EventSystem.Subscribe(EventName.MENU_CLOSED, EnableSelf);
    }

    public void OnDisable()
    {
        EventSystem.Unsubscribe(EventName.MENU_OPENED, DisableSelf);
        EventSystem.Unsubscribe(EventName.MENU_CLOSED, EnableSelf);
    }

    public void EnableSelf(object _value = null)
    {
        isActive = true;
    }

    public void DisableSelf(object _value = null)
    {
        isActive = false;
    }
}
