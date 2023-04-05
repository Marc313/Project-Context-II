using UnityEngine;

public class ThrowEvent : MonoBehaviour
{
    [SerializeField] private Thrower thrower;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerLogic logic;

    public void ActivateAIThrower()
    {
        thrower?.Activate();

        if (thrower != null && thrower is ClickThrower)
        {
            (thrower as ClickThrower).trajectory.Disable();
        }
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

    public void HidePropie()
    {
        if (logic != null)
        {
            logic.HideProp();
        }
    }
}
