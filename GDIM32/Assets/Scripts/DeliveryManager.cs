using UnityEngine;
public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [Header("Global Order Config")]
    public OrderData order1;
    public OrderData order2;

    [Header("Runtime Data")]
    public OrderData currentActiveOrder;
    public int totalEarnings;
    public int currentDeliveryEarnings;
    public bool isFirstOrderCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        order1.ResetOrderState();
        order2.ResetOrderState();
        isFirstOrderCompleted = false;
        totalEarnings = 0;
        currentDeliveryEarnings = 0;
    }

    public void StartOrder(OrderData targetOrder)
    {
        if (currentActiveOrder != null) return;
        currentActiveOrder = targetOrder;
        currentActiveOrder.currentState = OrderData.OrderState.Accepted;
        currentDeliveryEarnings = 0;

        if (currentActiveOrder == order2)
        {
            currentActiveOrder.isCookieOffered = true;
        }

        UIManager.Instance.RefreshOrderUI();
        UIManager.Instance.RefreshEarningsUI();
    }

    public void AddEarnings(int amount)
    {
        currentDeliveryEarnings += amount;
        totalEarnings += amount;
        UIManager.Instance.RefreshEarningsUI();
    }

    public void CompleteOrder()
    {
        if (currentActiveOrder == null) return;
        currentActiveOrder.currentState = OrderData.OrderState.Submitted;

        if (currentActiveOrder == order1)
        {
            isFirstOrderCompleted = true;
        }

        currentActiveOrder.ResetOrderState();
        currentActiveOrder = null;
    }

    public OrderData GetNextAvailableOrder()
    {
        if (!isFirstOrderCompleted) return order1;
        else return order2;
    }
}