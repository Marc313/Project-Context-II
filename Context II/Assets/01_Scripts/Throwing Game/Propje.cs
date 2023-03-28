using UnityEngine;

public class Propje : Projectile
{
    [HideInInspector] public bool isFromPlayer = false;
    public string word { get; set; }

    private float time;
    private float startY;

    protected override void Start()
    {
        base.Start();

        if (isFromPlayer && currentLifetime > 0.0f && currentLifetime < 2.5f)
        {
            base.OnImpact(null);
        }
/*        startY = transform.position.y;*/
    }

    protected override void Update()
    {
/*        time++;
        transform.SetYPosition(Mathf.Sin(time/ 10f) + startY);*/
        base.Update();
    }

    protected override void OnImpact(GameObject _collisionObject)
    {
        PlayerMovement player = _collisionObject.GetComponentInParent<PlayerMovement>();
        if (player != null) return;


        GamemodeManager.Instance.AddCEOHitCount();
        _collisionObject.GetComponent<ShowText>()?.ShowTextObject(word);
        _collisionObject.GetComponent<ITarget>()?.OnHit(word, isFromPlayer);
        _collisionObject.GetComponentInParent<ITarget>()?.OnHit(word, isFromPlayer);

        if (isFromPlayer)
        {
            FindObjectOfType<PropjeSelectMenu>()?.ShowButtons();
        }
        base.OnImpact(_collisionObject);
    }

}
