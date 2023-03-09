using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    public bool interactable = true;

    [HideInInspector] public Vector3 slotPosition;
    [HideInInspector] public Image itemIcon;
    [HideInInspector] public GameObject turnBack;
    [HideInInspector] public bool isDropped;
    [HideInInspector] public Token item;
    private CanvasGroup canvasGroup;

    private DropSlotScale currentDropSlot;

    private void Awake()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        turnBack = transform.GetChild(1).gameObject;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        itemIcon.gameObject.SetActive(false);
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
        if (!interactable 
            || canvasGroup == null
            || item == null) return;

        //Debug.Log("Begin Drag");

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("End Drag");
        if (item == null) return;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!isDropped)
        {
            transform.position = slotPosition;
        }
        else
        {
            turnBack.gameObject.SetActive(true);
        }

    }

    /*    public void OnDrop(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }*/

    public void OnDrag(PointerEventData eventData)
    {
        if (!interactable || isDropped || item == null) return;
        // Debug.Log("Drag");

        itemIcon.transform.position += (eventData.delta.x * Vector3.right + eventData.delta.y * Vector3.up);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDropped && currentDropSlot != null)
        {
            itemIcon.transform.position = slotPosition;
            itemIcon.transform.rotation = Quaternion.identity;
            FindObjectOfType<UIManager>().AddToBalanceValue(-0.1f, currentDropSlot.side);
            turnBack.gameObject.SetActive(false);
            isDropped= false;
            currentDropSlot.RemoveCurrentToken();
            currentDropSlot= null;
        }

    }

    public void SetDropSlot(DropSlotScale _dropSlotScale)
    {
        currentDropSlot= _dropSlotScale;
    }
}
