using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [Header("Interacting")]
    public float interactRange;

    private IInteractable closestInteractable;

    private void Update()
    {
        CheckInteractables();
        InteractInput();
    }

    private void InteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            closestInteractable?.OnInteract();
        }
    }

    private void CheckInteractables()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);

        closestInteractable = null;
        float closestDistance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestInteractable = interactable;
                    }
                }
            }
        }


        /*        closestInteractable = colliders.OrderBy(collider => Vector3.Distance(transform.position, collider.transform.position))
                                                                .Select(collider => collider.GetComponent<IInteractable>())
                                                                .Where(collider => collider != null)
                                                                .First();*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}