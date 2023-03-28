using UnityEngine;

public class ThrowEvent : MonoBehaviour
{
    [SerializeField] private AIThrower thrower;

    public void ActivateAIThrower()
    {
        thrower?.Activate();
    }
}
