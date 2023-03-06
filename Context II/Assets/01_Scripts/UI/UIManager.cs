using JetBrains.Annotations;
using MarcoHelpers;
using newDialogue;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Dialogue System")]
    public Canvas dialogueCanvas;
    public TMP_Text dialogueSequenceName;
    public TMP_Text dialogueSequenceText;
    public TMP_Text dialogueChoiceName;
    public TMP_Text dialogueChoiceText;
    public GameObject dialogueSequenceUI;
    public GameObject dialogueChoiceUI;
    [Space]
    public Button[] choiceButtons;

    [Header("Question System")]
    public GameObject QuesitionUI;
    public TMP_Text QuestionText;

    [Header("Designing")]
    public TMP_Text ProgressCounter;

    [Header("Tokens")]
    public GameObject ItemObtainScreen;
    public TMP_Text ItemObtainText;

    [Header("Weegschaal")]
    public GameObject ScaleMenu;
    public InventorySlot[] inventorySlots;
    public Slider balanceBar;

    private Color citizenColor;
    private Color ceoColor;

    public TMP_Text dialogueName { get; set; }
    public TMP_Text dialogueText { get; set; }
    private DialogueManager dialogueManager;
    private PlayerLogic player;

    int captureCount = 0;


    private void Awake()
    {
        ServiceLocator.RegisterService(this);
        player = FindObjectOfType<PlayerLogic>();
    }

    private void Start()
    {
        dialogueManager = ServiceLocator.GetService<DialogueManager>();
        SwitchToSequence();
    }

    private void OnEnable()
    {
        EventSystem.Subscribe(EventName.ITEM_OBTAINED, ShowItemObtainScreen);
    }

    private void OnDisable()
    {
        EventSystem.Unsubscribe(EventName.ITEM_OBTAINED, ShowItemObtainScreen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            captureCount++;
            ScreenCapture.CaptureScreenshot("Assets/Screenshot" + captureCount + ".jpg");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleScaleMenu();
            FillInventorySlots();
        }
    }

    public void ShowQuestionCanvas()
    {
        QuesitionUI.SetActive(true);
    }

    public void ChangeQuestionText(string _question)
    {
        QuestionText.text = _question;
    }

    public void SwitchToSequence()
    {
        dialogueSequenceUI.SetActive(true);
        dialogueChoiceUI.SetActive(false);

        dialogueName = dialogueSequenceName;
        dialogueText = dialogueSequenceText;
    }

    public void SwitchToChoice()
    {
        dialogueSequenceUI.SetActive(false);
        dialogueChoiceUI.SetActive(true);

        dialogueName = dialogueChoiceName;
        dialogueText = dialogueChoiceText;
    }

    // Leave to UI Manager
    public void ShowDialogueCanvas()
    {
        dialogueCanvas.gameObject.SetActive(true);
    }

    // Leave to UI Manager
    public void HideDialogueCanvas()
    {
        dialogueCanvas.gameObject.SetActive(false);
        dialogueSequenceName.text = string.Empty;
        dialogueSequenceText.text = string.Empty;
    }

    public void ConnectButtons(Choice[] choices, System.Action _onSubsequenceEnd)
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            Button button = choiceButtons[i];
            Choice choice = choices[i];
            TMP_Text optionText = button.GetComponentInChildren<TMP_Text>();

            if (optionText != null) optionText.text = choice.optionName;
            button.onClick.AddListener(() => dialogueManager.StartSequence(choice.response, _onSubsequenceEnd));
        }
    }

    public void AddToBalanceValue(float _value)
    {
        balanceBar.value += _value;
    }

    private void ShowItemObtainScreen(object _item)
    {
        Item item = (Item)_item;

        ItemObtainScreen.SetActive(true);
        ItemObtainText.text = "New token obtained: " + item.name;
        Invoke(nameof(HideItemObtainScreen), 3f);

        //Debug.Log(item.name);
    }

    private void HideItemObtainScreen()
    {
        ItemObtainScreen.SetActive(false);
    }

    // You can now also do this during a conversation!
    private void ToggleScaleMenu()
    {
        bool isMenuActive = !ScaleMenu.activeSelf;
        ScaleMenu.SetActive(isMenuActive);
        if (isMenuActive)
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void FillInventorySlots()
    {
        List<Item> playerItems = player.inventory.items;
        Color transparent = new Color(0, 0, 0, 1);

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Image image = inventorySlots[i].GetComponent<Image>();
            Image tokenImage = slot.itemIcon;
            Token token = slot.item;

            if (i < playerItems.Count)
            {
                Token item = (Token)playerItems[i];
                inventorySlots[i].item = item;

                if (item != null && image != null)
                {
                    image.color = item.side == Token.Side.CEO ? Color.cyan : Color.green;
                    //tokenImage.color = token.color;
                }
                else
                {
                    image.color = Color.black;
                    //tokenImage.color = transparent;
                }
            }
            else
            {
                image.color = Color.black;
               //tokenImage.color = transparent;
            }
        }
    }
}
