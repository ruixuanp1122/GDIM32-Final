using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public OrderData order1;
    public OrderData order2;
    public OrderData currentActiveOrder;

    [Header("Earnings")]
    public int currentDeliveryEarnings;
    public int totalEarnings;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        ResetEarnings();
    }

    public void StartOrder(OrderData order)
    {
        currentActiveOrder = order;
        currentActiveOrder.orderStatus = OrderData.OrderStatus.Accepted;
        currentDeliveryEarnings = 0;
        UIManager.Instance.SetOrderImage(order.orderSprite);
        UIManager.Instance.UpdateEarningsUI(currentDeliveryEarnings, totalEarnings);
    }

    public void AddEarnings(int amount)
    {
        currentDeliveryEarnings += amount;
        totalEarnings += amount;
        UIManager.Instance.UpdateEarningsUI(currentDeliveryEarnings, totalEarnings);
    }

    public void CompleteOrder()
    {
        if (currentActiveOrder != null)
        {
            currentActiveOrder.orderStatus = OrderData.OrderStatus.Submitted;
            currentActiveOrder = null;
        }
    }

    private void ResetEarnings()
    {
        currentDeliveryEarnings = 0;
        totalEarnings = 0;
        UIManager.Instance.UpdateEarningsUI(0, 0);
    }
}