using MarcoHelpers;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [Header("Interacting")]
    public float interactRange;
    public Inventory inventory;

    private IInteractable closestInteractable;
    private bool isEnabled = true;
    private InteractIndicator[] indicators;

    private void Awake()
    {
        inventory = new Inventory();
        indicators = FindObjectsOfType<InteractIndicator>();
    }

    private void Start()
    {
        foreach(InteractIndicator indicator in indicators)
        {
            indicator.gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        EventSystem.Subscribe(EventName.MENU_OPENED, DisableSelf);
        EventSystem.Subscribe(EventName.MENU_CLOSED, EnableSelf);
    }

    public void OnDisable()
    {
        EventSystem.Unsubscribe(EventName.MENU_OPENED, DisableSelf);
        EventSystem.Unsubscribe(EventName.MENU_CLOSED, EnableSelf);
    }

    private void EnableSelf(object _value)
    {
        isEnabled = true;
    }

    private void DisableSelf(object _value)
    {
        isEnabled = false;
    }

    private void Update()
    {
        if (!isEnabled) return;

        CheckInteractables();
        InteractInput();

        if (closestInteractable != null)
        {
            InteractIndicator ind = ((NPC)closestInteractable).indicator;
            foreach (InteractIndicator indicator in indicators)
            {
                if (indicator == ind) {
                    indicator.gameObject.SetActive(true);
                }
            }
        }
    }

    public void ObtainItem(Item _item)
    {
        inventory.items.Add(_item);
        EventSystem.RaiseEvent(EventName.ITEM_OBTAINED, _item);

/*        Instantiate(_item);
        SimpleAnimations.*/
    }

    private void InteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            closestInteractable?.OnInteract();
        }
    }

    private void CheckInteractables()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);

        if (closestInteractable != null)
        {
            try
            {
                NPC npc = closestInteractable as NPC;
                npc?.indicator?.gameObject?.SetActive(false);
            }
            catch { }
        }
        closestInteractable = null;
        float closestDistance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestInteractable = interactable;
                    }
                }
            }
        }


        /*        closestInteractable = colliders.OrderBy(collider => Vector3.Distance(transform.position, collider.transform.position))
                                                                .Select(collider => collider.GetComponent<IInteractable>())
                                                                .Where(collider => collider != null)
                                                                .First();*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
