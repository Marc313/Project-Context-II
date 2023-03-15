using UnityEngine;

public class ClickThrower : Thrower
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Activate();
        }
    }

    public override Vector3 CalculateTargetDirection()
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

        return (destination - startPos.position).normalized;
    }
}
