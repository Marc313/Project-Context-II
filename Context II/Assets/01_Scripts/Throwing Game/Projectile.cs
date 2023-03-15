using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float forceMultiplier;
    [SerializeField] private float lifetime = 7;

    private float currentLifetime;

    private Vector3 targetDirection;
    private Rigidbody rigidBody;
    [HideInInspector] public bool destroyOnImpact;

    private void Start()
    {
        currentLifetime = lifetime;
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime > 0)
        rigidBody.AddForce(targetDirection * forceMultiplier * Time.deltaTime * 1/currentLifetime * lifetime, ForceMode.Acceleration);
    }

    public void SetTargetDirection(Vector3 _direction)
    {
       targetDirection = _direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyOnImpact)
        {
            Destroy(gameObject);
        }
    }
}
