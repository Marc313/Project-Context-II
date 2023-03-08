using MarcoHelpers;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private DropSlotScale[] scaleSlots;
    private Slider balanceBar;

    private void Awake()
    {
        scaleSlots = FindObjectsOfType<DropSlotScale>();
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CheckBalanceState()
    {
        FindDependencies();
        if (!balanceBar.value.NearlyEquals(0.5f, 0.01f)) return;
        if (scaleSlots == null || scaleSlots.Length <= 0) return;

        // Check if all tokens are used to form a balance
        foreach (DropSlotScale slot in scaleSlots)
        {
            if (!slot.IsCapacityReached())
            {
                return;
            }
        }

        Debug.Log(" BALANCE ");
        EventSystem.RaiseEvent(EventName.WEEGSCHAAL_BALANCED);
    }

    private void FindDependencies()
    {
        if (balanceBar == null) balanceBar = FindObjectOfType<Slider>();
        if (scaleSlots == null || scaleSlots.Length <= 0) scaleSlots = FindObjectsOfType<DropSlotScale>();
    }
}
