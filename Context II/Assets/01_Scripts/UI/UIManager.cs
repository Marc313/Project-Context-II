using MarcoHelpers;
using newDialogue;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Cursor")]
    public Texture2D CursorSprite;

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
    private float actualBalanceBarValue;

    [Header("Articles")]
    public GameObject ArticleMenu;
    public TMP_Text articleTitle;
    public TMP_Text articleContent;
    private bool inArticleMenu;

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
        CursorSetup();

        SwitchToSequence();
        Invoke(nameof(Test), 0.05f);
        Invoke(nameof(Test), 0.1f);

        actualBalanceBarValue = balanceBar.value;
    }

    public void Test ()
    {
        ToggleScaleMenu();
        FillInventorySlots();
    }

    private void OnEnable()
    {
        EventSystem.Subscribe(EventName.WEEGSCHAAL_BALANCED, SwitchToArticleMenu);
        EventSystem.Subscribe(EventName.ITEM_OBTAINED, ShowItemObtainScreen);
        EventSystem.Subscribe(EventName.ARTICLE_CHANGE, DisplayArticle);
        EventSystem.Subscribe(EventName.MENU_OPENED, EnableCursor);
        EventSystem.Subscribe(EventName.MENU_CLOSED, DisableCursor);
    }

    private void OnDisable()
    {
        EventSystem.Unsubscribe(EventName.WEEGSCHAAL_BALANCED, SwitchToArticleMenu);
        EventSystem.Unsubscribe(EventName.ITEM_OBTAINED, ShowItemObtainScreen);
        EventSystem.Unsubscribe(EventName.ARTICLE_CHANGE, DisplayArticle);
        EventSystem.Unsubscribe(EventName.MENU_OPENED, EnableCursor);
        EventSystem.Unsubscribe(EventName.MENU_CLOSED, DisableCursor);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            captureCount++;
            ScreenCapture.CaptureScreenshot("Assets/Screenshot" + captureCount + ".jpg");
        }

        if (Input.GetKeyDown(KeyCode.Q) && !inArticleMenu)
        {
            ToggleScaleMenu();
            FillInventorySlots();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SwitchToArticleMenu();
        }

        if (IsMouseOnUI())
        {
            Cursor.lockState = CursorLockMode.None;
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

    public void AddToBalanceValue(float _value, Token.Side _side)
    {
        int multiplier = _side == Token.Side.Citizen ? -1 : 1;
        actualBalanceBarValue += _value * multiplier;
        balanceBar.value = Mathf.Clamp01(actualBalanceBarValue);
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

    // You can now also do this during a conversation! Bad!
    private void ToggleScaleMenu()
    {
        bool isMenuActive = !ScaleMenu.activeSelf;
        ScaleMenu.SetActive(isMenuActive);
        if (isMenuActive)
        {
            EventSystem.RaiseEvent(EventName.MENU_OPENED);
        }
        else
        {
            EventSystem.RaiseEvent(EventName.MENU_CLOSED);
        }
    }

    private void FillInventorySlots()
    {
        List<Item> playerItems = player.inventory.items;
        Color transparent = new Color(0, 0, 0, 0);

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Image image = slot.GetComponent<Image>();
            Image tokenImage = slot.itemIcon;

            if (i < playerItems.Count)
            {
                tokenImage.gameObject.SetActive(true);
                Token item = (Token)playerItems[i];
                slot.item = item;

                if (item != null && image != null)
                {
                    //image.color = item.side == Token.Side.CEO ? Color.cyan : Color.green;
                    tokenImage.color = item.color;
                }
                else
                {
                    image.color = Color.black;
                    tokenImage.color = transparent;
                }
            }
        }
    }

    private void DisplayArticle(object _article)
    {
        sArticle article = _article as sArticle;
        articleTitle.text = article.title;
        articleContent.text = article.content;
    }

    private void SwitchToArticleMenu(object _value = null)
    {
        if (ArticleMenu == null) return;

        inArticleMenu = true;
        ArticleMenu.SetActive(true);
        ScaleMenu.SetActive(false);
        EventSystem.RaiseEvent(EventName.MENU_OPENED);
    }

    private void CursorSetup()
    {

        //set the cursor origin to its centre. (default is upper left corner)
        Vector2 cursorOffset = new Vector2(CursorSprite.width / 2, CursorSprite.height / 2);

        //Sets the cursor to the Crosshair sprite with given offset 
        //and automatic switching to hardware default if necessary
        Cursor.SetCursor(CursorSprite, cursorOffset, CursorMode.Auto);
    }

    private void EnableCursor(object value)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableCursor(object _value)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public static bool IsMouseOnUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
