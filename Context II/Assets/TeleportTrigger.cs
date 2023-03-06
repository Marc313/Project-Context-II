using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        // Eventueel check of in trigger is
        TeleportToHouse();
    }

    public void TeleportToHouse()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.transform.position = new Vector3(0, 0, 80);
    }
}
