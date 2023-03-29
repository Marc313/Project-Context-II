using MarcoHelpers;
using ThrowingGame;
using UnityEngine;

public abstract class Thrower : MonoBehaviour
{
    public GameObject throwablePrefab;
    public Transform startPos;
    public bool destroyOnImpact;
    public NPCThrowing.Side side;

    protected Propje currentThrowable;
    protected bool isActive;
    protected abstract bool isFromPlayer { get; }

    [Header("Character Animation")]
    [SerializeField] private Animator anim;

    public void Activate()
    {
        CreateProjectile(CalculateTargetDirection());
    }

    public abstract Vector3 CalculateTargetDirection();

    public void PlayThrowAnimation()
    {
        if (/*!anim.IsInTransition(0) &&*/
            !anim.GetCurrentAnimatorStateInfo(0).IsName("ImpactSmall")
            || !anim.GetCurrentAnimatorStateInfo(0).IsName("ImpactLargeGut"))
             anim.CrossFade("Smack2", 0.05f, 0);
    }

    public virtual void CreateProjectile(Vector3 _targetDirection)
    {
        currentThrowable = Instantiate(throwablePrefab, startPos.position, Quaternion.identity).GetComponent<Propje>();
        currentThrowable.SetTargetDirection(_targetDirection);
        currentThrowable.isFromPlayer = isFromPlayer;
        currentThrowable.destroyOnImpact = destroyOnImpact;
        currentThrowable.side = side;
        Invoke(nameof(EnableCollider), .3f);
    }

    public void EnableCollider()
    {
        currentThrowable.GetComponent<Collider>().enabled = true;
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
        //if (FindObjectOfType<PropjeSelectMenu>()?.GetComponentInChildren<UnityEngine.UI.Button>() == null)
        isActive = true;
    }

    public void DisableSelf(object _value = null)
    {
        isActive = false;
    }
}
