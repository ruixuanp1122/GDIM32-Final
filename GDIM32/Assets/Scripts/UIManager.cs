using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Button confirmBtn;

    [Header("Earnings UI")]
    public Text currentEarnText;
    public Text totalEarnText;

    [Header("Held Item Icons")]
    public GameObject burgerIcon;
    public GameObject pizzaIcon;
    public GameObject cookieIcon;

    [Header("Order UI (Single Image)")]
    public Button orderIconBtn;
    public GameObject orderPanel;
    public Image orderImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        orderIconBtn.onClick.AddListener(ToggleOrderPanel);
        orderPanel.SetActive(false);
        confirmBtn.onClick.AddListener(HideDialogue);
    }

    public void ToggleOrderPanel()
    {
        orderPanel.SetActive(!orderPanel.activeSelf);
    }

    public void SetOrderImage(Sprite sprite)
    {
        if (orderImage != null && sprite != null)
        {
            orderImage.sprite = sprite;
            orderImage.SetNativeSize();
        }
    }

    public void UpdateEarningsUI(int current, int total)
    {
        currentEarnText.text = "Current: $" + current;
        totalEarnText.text = "Total: $" + total;
    }

    public void ShowHoldIcon(string itemType)
    {
        HideAllHoldIcons();
        switch (itemType)
        {
            case "Burger": burgerIcon.SetActive(true); break;
            case "Pizza": pizzaIcon.SetActive(true); break;
            case "Cookie": cookieIcon.SetActive(true); break;
            case "Burger_Cookie": burgerIcon.SetActive(true); cookieIcon.SetActive(true); break;
            case "Pizza_Cookie": pizzaIcon.SetActive(true); cookieIcon.SetActive(true); break;
        }
    }

    public void HideAllHoldIcons()
    {
        burgerIcon.SetActive(false);
        pizzaIcon.SetActive(false);
        cookieIcon.SetActive(false);
    }

    public void ShowDialogue(string text)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = text;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}