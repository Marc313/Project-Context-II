using System.Linq;
using UnityEngine;

public class ClickThrower : Thrower
{
    public string currentWord { get; set; }
    protected override bool isFromPlayer => true;
    [SerializeField] private bool onlyOneAllowed = true;

    private void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Mouse0) && !isPropjeFromPlayerAround())
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

    public override void CreateProjectile(Vector3 _targetDirection)
    {
        base.CreateProjectile(_targetDirection);
        currentThrowable.word = currentWord;
    }

    private bool isPropjeFromPlayerAround()
    {
        return FindObjectsOfType<Propje>().Select(p => p.isFromPlayer).Where(b => b == true).Count() >= 1;
    }
}
