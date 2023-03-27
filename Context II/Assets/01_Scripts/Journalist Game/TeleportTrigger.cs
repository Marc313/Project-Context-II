using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform teleportPos;

    public void OnInteract()
    {
        // Eventueel check of in trigger is
        TeleportToHouse();
    }

    public void TeleportToHouse()
    {
        Debug.Log("Interact");
        PlayerController player = FindObjectOfType<PlayerController>();
        player.enabled = false;
        player.transform.position = teleportPos.position;
        player.enabled = true;
        player.transform.rotation = teleportPos.rotation;
    }
}
