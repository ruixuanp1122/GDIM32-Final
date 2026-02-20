using UnityEngine;
public class RestaurantOwnerNPC : NPC
{
    [Header("Dialogue Content")]
    public string talk_AcceptOrder = "Take the {0} to {1}! Base pay: {2}$! Don't forget the cookie!";
    public string talk_CompleteOrder = "Good job! Here's your base pay! {0}$!";
    public string talk_HasActiveOrder = "Finish your current order first!";
    public string action_PickupFood = "You picked up the {0}! Go go go!";

    public override void InteractTalk()
    {
        UIManager.Instance.CloseDialogue();
        UIManager.Instance.orderUIPanel.SetActive(false);

        if (currentOrder != null)
        {
            if (currentOrder.currentState == OrderData.OrderState.Delivered)
            {
                UIManager.Instance.ShowDialogue(string.Format(talk_CompleteOrder, currentOrder.basePay));
                DeliveryManager.Instance.AddEarnings(currentOrder.basePay);
                DeliveryManager.Instance.CompleteOrder();
                UIManager.Instance.HideAllHoldIcons();
            }
            else
            {
                UIManager.Instance.ShowDialogue(talk_HasActiveOrder);
            }
            return;
        }

        OrderData nextOrder = DeliveryManager.Instance.GetNextAvailableOrder();
        DeliveryManager.Instance.StartOrder(nextOrder);
        UIManager.Instance.ShowDialogue(string.Format(talk_AcceptOrder, nextOrder.foodType, nextOrder.customerName, nextOrder.basePay));
    }

    public override void InteractAction()
    {
        UIManager.Instance.CloseDialogue();
        if (currentOrder == null || currentOrder.currentState != OrderData.OrderState.Accepted)
        {
            UIManager.Instance.ShowDialogue("Accept an order first!");
            return;
        }

        currentOrder.currentState = OrderData.OrderState.PickedUp;
        UIManager.Instance.ShowDialogue(string.Format(action_PickupFood, currentOrder.foodType));
        UIManager.Instance.ShowHoldIcon(currentOrder.foodType);
    }
}