using UnityEngine;

public class ClickThrower : MonoBehaviour
{
    public GameObject throwablePrefab;
    public Transform startPos;
    public bool destroyOnImpact;
    private Projectile currentThrowable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootProjectile();
        }
    }

    public void ShootProjectile()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 destination;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            destination = hit.point;
        }
        else
        {
            // Set the destination to 1000 units in the direction of the camera
            destination = ray.GetPoint(1000);
        }

        currentThrowable = Instantiate(throwablePrefab, startPos.position, Quaternion.identity).GetComponent<Projectile>();
        currentThrowable.SetTargetDirection((destination - startPos.position).normalized);
        currentThrowable.destroyOnImpact = destroyOnImpact;
    }
}
