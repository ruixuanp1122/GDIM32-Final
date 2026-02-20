using UnityEngine;

public class CustomerNPC : NPC
{
    public OrderData targetOrder;
    private int tipAmount;

    public override void InteractTalk()
    {
        if (!isPlayerInRange) return;
        
        if (DeliveryManager.Instance.currentActiveOrder == targetOrder)
        {
            switch (DeliveryManager.Instance.currentActiveOrder.orderStatus)
            {
                case OrderData.OrderStatus.PickedUp:
                    UIManager.Instance.ShowDialogue("Hi! Did you bring my food? Press E to deliver!");
                    break;
                case OrderData.OrderStatus.Delivered:
                    UIManager.Instance.ShowDialogue("Thanks again for the food!");
                    break;
                default:
                    UIManager.Instance.ShowDialogue("Waiting for my food...");
                    break;
            }
        }
        else
        {
            UIManager.Instance.ShowDialogue("This isn't my order! Wrong house!");
        }
    }

    public override void InteractAct()
    {
        if (!isPlayerInRange) return;
        if (DeliveryManager.Instance.currentActiveOrder != targetOrder) return;

        if (DeliveryManager.Instance.currentActiveOrder.orderStatus == OrderData.OrderStatus.PickedUp)
        {
            DeliverFood();
        }
        else
        {
            UIManager.Instance.ShowDialogue("No food to deliver yet!");
        }
    }

    private void DeliverFood()
    {
        tipAmount = targetOrder.baseTip;
        if (DeliveryManager.Instance.currentActiveOrder.isCookiePicked)
        {
            tipAmount += targetOrder.cookieExtraTip;
            UIManager.Instance.ShowDialogue("Yum! Fortune cookie too! Tip: $" + tipAmount);
        }
        else
        {
            UIManager.Instance.ShowDialogue("Thanks! Tip: $" + tipAmount);
        }

        DeliveryManager.Instance.AddEarnings(tipAmount);
        DeliveryManager.Instance.currentActiveOrder.orderStatus = OrderData.OrderStatus.Delivered;
        UIManager.Instance.HideAllHoldIcons();
    }
}