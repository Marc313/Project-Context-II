using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlotScale : MonoBehaviour, IDropHandler
{
    public Token.Side side = Token.Side.Citizen;

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot slot = eventData.pointerDrag.GetComponent<InventorySlot>();
        slot.isDropped = true;
        Debug.Log("Drop");

        // Voeg score toe
        float addValue = side == Token.Side.Citizen ? -0.1f : 0.1f;
        FindObjectOfType<UIManager>().AddToBalanceValue(addValue);

/*        if (slot.item.side == side)
        {
            // Score voor side
        }*/
    }
}
