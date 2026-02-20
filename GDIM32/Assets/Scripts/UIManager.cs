using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Button confirmBtn;

    [Header("Order UI")]
    public GameObject orderUIPanel;
    public Button orderIconBtn;
    public Image houseRefImage;
    public Text customerNameText;
    public Text foodTypeText;
    public Text basePayText;

    [Header("Earnings UI")]
    public Text currentEarnText;
    public Text totalEarnText;

    [Header("Held Item Icons")]
    public GameObject burgerIcon;
    public GameObject pizzaIcon;
    public GameObject cookieIcon;

    private bool isOrderPanelOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        dialoguePanel.SetActive(false);
        orderUIPanel.SetActive(false);
        isOrderPanelOpen = false;
        HideAllHoldIcons();
        confirmBtn.onClick.AddListener(CloseDialogue);
        orderIconBtn.onClick.AddListener(ToggleOrderPanel);
    }

    public void ShowDialogue(string content)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = content;
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void RefreshOrderUI()
    {
        OrderData currentOrder = DeliveryManager.Instance.currentActiveOrder;
        if (currentOrder == null)
        {
            orderUIPanel.SetActive(false);
            orderIconBtn.gameObject.SetActive(false);
            return;
        }

        orderIconBtn.gameObject.SetActive(true);
        houseRefImage.sprite = currentOrder.houseRefImage;
        customerNameText.text = "Customer: " + currentOrder.customerName;
        foodTypeText.text = "Food: " + currentOrder.foodType;
        basePayText.text = "Base Pay: " + currentOrder.basePay + " $";

        isOrderPanelOpen = false;
        orderUIPanel.SetActive(false);
    }

    private void ToggleOrderPanel()
    {
        isOrderPanelOpen = !isOrderPanelOpen;
        orderUIPanel.SetActive(isOrderPanelOpen);
    }

    public void RefreshEarningsUI()
    {
        currentEarnText.text = "Current: " + DeliveryManager.Instance.currentDeliveryEarnings + " $";
        totalEarnText.text = "Total: " + DeliveryManager.Instance.totalEarnings + " $";
    }

    public void ShowHoldIcon(string itemType)
    {
        HideAllHoldIcons();
        switch (itemType)
        {
            case "Burger":
                burgerIcon.SetActive(true);
                break;
            case "Pizza":
                pizzaIcon.SetActive(true);
                break;
            case "Cookie":
                cookieIcon.SetActive(true);
                break;
            case "Burger_Cookie":
                burgerIcon.SetActive(true);
                cookieIcon.SetActive(true);
                break;
            case "Pizza_Cookie":
                pizzaIcon.SetActive(true);
                cookieIcon.SetActive(true);
                break;
        }
    }

    public void HideAllHoldIcons()
    {
        burgerIcon.SetActive(false);
        pizzaIcon.SetActive(false);
        cookieIcon.SetActive(false);
    }
}