using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    public OrderData currentOrder;
    public int totalMoney;

    void Awake()
    {
        Instance = this;
    }

    public void StartOrder(OrderData order)
    {
        currentOrder = order;
        currentOrder.state = OrderState.Accepted;
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        Debug.Log("Money: " + totalMoney);
    }
}