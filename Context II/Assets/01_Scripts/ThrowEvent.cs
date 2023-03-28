using UnityEngine;

public class ThrowEvent : MonoBehaviour
{
    [SerializeField] private Thrower thrower;
    [SerializeField] private PlayerMovement movement;

    public void ActivateAIThrower()
    {
        thrower?.Activate();
    }

    public void DisableMovement()
    {
        if (movement != null)
        movement.isInteracting = true;
    }

    public void EnableMovement()
    {
        if (movement != null)
        movement.isInteracting = false;
    }
}
