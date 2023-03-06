using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image itemIcon;
    public bool interactable = true;

    public Vector3 slotPosition;
    [HideInInspector] public bool isDropped;
    [HideInInspector] public Token item;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        itemIcon = GetComponentInChildren<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        slotPosition = transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (interactable) UIManager.Instance.ShowItemInfo(slot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (interactable) UIManager.Instance.HideItemInfo();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!interactable /*|| isDropped*/ || canvasGroup == null) return;

        Debug.Log("Begin Drag");

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!isDropped)
        {
            transform.position = slotPosition;
        }

    }

    /*    public void OnDrop(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }*/

    public void OnDrag(PointerEventData eventData)
    {
        if (!interactable || isDropped) return;
        Debug.Log("Drag");

        itemIcon.transform.position += (eventData.delta.x * Vector3.right + eventData.delta.y * Vector3.up);

    }
}
