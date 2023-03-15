using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;
    //[HideInInspector] public float damage;
    [HideInInspector] public float lifeTime;
    [HideInInspector] public float UpdateTargetRate;
    //[HideInInspector] public GameObject ImpactParticles;

    //private ParticleSystem trailParticles;
    private Transform target;
    private Vector3 targetDirection;
    private bool isExploded;

    private void Awake()
    {
        //trailParticles = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        //trailParticles.Play();
        InvokeRepeating(nameof(SetNewTargetDirection), 0, UpdateTargetRate);
    }

    private void FixedUpdate()
    {
        if (isExploded) return;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) DestroySelf();

        if (targetDirection != null)
        {
            ShootTowards(targetDirection);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isExploded) return;

        bool condition = false;
        if (condition)
        {
            // Other effects
            isExploded= true;
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        // Cleanup
        Destroy(gameObject);
    }

    private void ShootTowards(Vector3 direction)
    {
        Vector3 movement = direction * Time.deltaTime * moveSpeed;
        transform.position += movement;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void SetNewTargetDirection()
    {
        if (target == null) return;
        targetDirection = (target.transform.position - transform.position).normalized;
        targetDirection.y = 0; // Stay on the ground
    }
}