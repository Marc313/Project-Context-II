using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowDescriptionOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public GameObject descriptionMenu;
    [SerializeField] private TMP_Text descriptionText;

    private PropjeSelectMenu buttonMenu;
    private TMP_Text childText;

    private void Awake()
    {
        buttonMenu = FindObjectOfType<PropjeSelectMenu>();
        childText = GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
/*        descriptionMenu.SetActive(true);
        descriptionText.text = buttonMenu.GetDescription(childText);*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
/*        descriptionMenu.SetActive(false);
        descriptionText.text = string.Empty;*/
    }
}
