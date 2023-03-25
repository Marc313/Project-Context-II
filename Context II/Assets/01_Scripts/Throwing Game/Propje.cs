using UnityEngine;

public class Propje : Projectile
{
    [HideInInspector] public bool countsToScore = false;
    public string word { get; set; }

    protected override void OnImpact(GameObject _collisionObject)
    {
        if (countsToScore)
        {
            GamemodeManager.Instance.AddCEOHitCount();
            _collisionObject.GetComponent<ShowText>()?.ShowTextObject(word);
            _collisionObject.GetComponent<ITarget>()?.OnHit(word);
            _collisionObject.GetComponentInParent<ITarget>()?.OnHit(word);
            FindObjectOfType<PropjeSelectMenu>()?.ShowButtons();
        }
        base.OnImpact(_collisionObject);
    }

}
