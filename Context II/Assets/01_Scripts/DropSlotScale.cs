using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlotScale : MonoBehaviour, IDropHandler
{
    public int tokensNeeded = 5;
    public Token.Side side = Token.Side.Citizen;
    public List<Transform> transforms= new List<Transform>();

    private int tokenCount;

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot slot = eventData.pointerDrag.GetComponent<InventorySlot>();
        slot.isDropped = true;
        slot.SetDropSlot(this);
        slot.itemIcon.transform.position = transforms[tokenCount].position;
        slot.itemIcon.transform.rotation = transforms[tokenCount].rotation;
        tokenCount++;

        // Voeg score toe
        FindObjectOfType<UIManager>().AddToBalanceValue(0.1f, side);

/*        if (slot.item.side == side)
        {
            // Score voor side
        }*/
    }

    public void RemoveCurrentToken()
    {
        tokenCount--;
    }

    public bool IsCapacityReached()
    {
        return tokenCount >= tokensNeeded;
    }
}
