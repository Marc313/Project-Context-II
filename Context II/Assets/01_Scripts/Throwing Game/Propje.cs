using ThrowingGame;
using UnityEngine;

public class Propje : Projectile
{
    [HideInInspector] public bool isFromPlayer = false;
    public string word { get; set; }
    public float playerSizeModifier = 1.5f;
    public NPCThrowing.Side side;

    private float time;
    private float startY;

    protected override void Start()
    {
        base.Start();

        if (isFromPlayer)
        {
            transform.localScale = transform.localScale * playerSizeModifier;
        }
/*        startY = transform.position.y;*/
    }

    protected override void Update()
    {
/*        time++;
        transform.SetYPosition(Mathf.Sin(time/ 10f) + startY);*/
        base.Update();

        if (isFromPlayer && currentLifetime > 0.0f && currentLifetime < 2.5f)
        {
            base.OnImpact(null);
        }
    }

    protected override void OnImpact(GameObject _collisionObject)
    {
        NPCThrowing citizen = _collisionObject.GetComponentInParent<NPCThrowing>();
        if (citizen != null 
            && citizen.side == side) return;

        GamemodeManager.Instance.AddCEOHitCount();
        //_collisionObject.GetComponent<ShowText>()?.ShowTextObject(word);
        _collisionObject.GetComponent<ITarget>()?.OnHit(word, isFromPlayer);
        _collisionObject.GetComponentInParent<ITarget>()?.OnHit(word, isFromPlayer);

        if (isFromPlayer)
        {
            FindObjectOfType<PropjeSelectMenu>()?.ShowButtons();
        }
        base.OnImpact(_collisionObject);
    }

}
