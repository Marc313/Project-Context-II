using MarcoHelpers;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.RaiseEvent(EventName.MENU_OPENED);
    }

    private void OnDisable()
    {
        EventSystem.RaiseEvent(EventName.MENU_CLOSED);
    }
}
