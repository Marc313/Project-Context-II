using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform teleportPos;

    public void OnInteract()
    {
        Debug.Log("Interact");
        // Eventueel check of in trigger is
        TeleportToHouse();
    }

    public void TeleportToHouse()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.transform.position = teleportPos.position;
        player.transform.rotation = teleportPos.rotation;
    }
}
