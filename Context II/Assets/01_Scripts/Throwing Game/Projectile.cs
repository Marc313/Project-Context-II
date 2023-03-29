using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float forceMultiplier;
    [SerializeField] private float yForceMultiplier;
    [SerializeField] private float lifetime = 7;

    protected float currentLifetime;

    private Vector3 targetDirection;
    private Rigidbody rigidBody;
    [HideInInspector] public bool destroyOnImpact;

    protected virtual void Start()
    {
        currentLifetime = lifetime;
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    protected virtual void Update()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime > 0)
        {
            rigidBody.AddForce(targetDirection * forceMultiplier * Time.deltaTime * 1 / currentLifetime * lifetime, ForceMode.Acceleration);
            rigidBody.AddForce(targetDirection * yForceMultiplier * Time.deltaTime * (lifetime/2 - currentLifetime), ForceMode.Acceleration);
        }
    }

    public void SetTargetDirection(Vector3 _direction)
    {
       targetDirection = _direction;
    }

    /*    private void OnCollisionEnter(Collision _collision)
        {
            Projectile otherProjectile = _collision.gameObject.GetComponent<Projectile>();

            if (destroyOnImpact && otherProjectile == null)
            {
                Destroy(gameObject);
            }
        }*/

    private void OnTriggerEnter(Collider _collision)
    {
        Projectile otherProjectile = _collision.gameObject.GetComponent<Projectile>();

        if (destroyOnImpact && otherProjectile == null)
        {
            OnImpact(_collision.gameObject);
        }
    }

    protected virtual void OnImpact(GameObject _collisionObject)
    {
        Destroy(gameObject);
    }
}
