using UnityEngine;

public class RestaurantOwnerNPC : NPC
{
    public OrderData order1;
    public OrderData order2;
    private bool isFirstOrderCompleted = false;

    public override void InteractTalk()
    {
        if (!isPlayerInRange) return;

        if (DeliveryManager.Instance.currentActiveOrder == null)
        {
            if (!isFirstOrderCompleted)
            {
                UIManager.Instance.ShowDialogue("Welcome! First order: Burger for Customer1! Press E to accept!");
            }
            else
            {
                UIManager.Instance.ShowDialogue("Great job! Next order: Pizza for Customer2! Press E to accept! \nWant a fortune cookie? (Press E to pick up later!)");
                order2.isCookieOffered = true;
            }
        }
        else
        {
            switch (DeliveryManager.Instance.currentActiveOrder.orderStatus)
            {
                case OrderData.OrderStatus.Accepted:
                    UIManager.Instance.ShowDialogue("Food's on the table! Press E to pick it up!");
                    break;
                case OrderData.OrderStatus.Delivered:
                    UIManager.Instance.ShowDialogue("Back already? Press E to get your base pay!");
                    break;
                default:
                    UIManager.Instance.ShowDialogue("Hurry up with the delivery!");
                    break;
            }
        }
    }

    public override void InteractAct()
    {
        if (!isPlayerInRange) return;

        if (DeliveryManager.Instance.currentActiveOrder == null)
        {
            AcceptNewOrder();
        }
        else
        {
            if (DeliveryManager.Instance.currentActiveOrder.orderStatus == OrderData.OrderStatus.Accepted)
            {
                PickUpFood();
            }
            else if (DeliveryManager.Instance.currentActiveOrder.orderStatus == OrderData.OrderStatus.Delivered)
            {
                ClaimBasePay();
            }
        }
    }

    private void AcceptNewOrder()
    {
        OrderData targetOrder = !isFirstOrderCompleted ? order1 : order2;
        DeliveryManager.Instance.StartOrder(targetOrder);
        targetOrder.orderStatus = OrderData.OrderStatus.Accepted;
        UIManager.Instance.ShowDialogue("Order accepted! Check Order UI for the house photo!");
    }

    private void PickUpFood()
    {
        OrderData currentOrder = DeliveryManager.Instance.currentActiveOrder;
        currentOrder.orderStatus = OrderData.OrderStatus.PickedUp;
        UIManager.Instance.ShowHoldIcon(currentOrder.foodType);
        UIManager.Instance.ShowDialogue("Food picked up! Don't forget the fortune cookie!");
    }

    private void ClaimBasePay()
    {
        OrderData currentOrder = DeliveryManager.Instance.currentActiveOrder;
        DeliveryManager.Instance.AddEarnings(currentOrder.basePay);
        currentOrder.orderStatus = OrderData.OrderStatus.Submitted;
        UIManager.Instance.ShowDialogue("Base pay: $" + currentOrder.basePay + "! Total earnings updated!");
        if (!isFirstOrderCompleted) isFirstOrderCompleted = true;
        DeliveryManager.Instance.currentActiveOrder = null;
    }
}