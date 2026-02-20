using UnityEngine;
public class CustomerNPC : NPC
{
    [Header("Customer Config")]
    public int customerID;

    [Header("Dialogue Content")]
    public string talk_Confirm = "Hi! Is my {0} here? I've been waiting!";
    public string talk_WrongCustomer = "This isn't my order! Go find the right person!";
    public string talk_NotPickedUp = "You didn't pick up the food yet!";
    public string action_Deliver = "Thanks! Here's your tip: {0}$!";
    public string action_DeliverWithCookie = "Wow, a fortune cookie! Extra tip for you! Total tip: {0}$!";

    public override void InteractTalk()
    {
        UIManager.Instance.CloseDialogue();
        if (currentOrder == null)
        {
            UIManager.Instance.ShowDialogue("You don't have any order!");
            return;
        }

        if (currentOrder.currentState != OrderData.OrderState.PickedUp)
        {
            UIManager.Instance.ShowDialogue(talk_NotPickedUp);
            return;
        }

        if (!IsMatchCustomer())
        {
            UIManager.Instance.ShowDialogue(talk_WrongCustomer);
            return;
        }

        UIManager.Instance.ShowDialogue(string.Format(talk_Confirm, currentOrder.foodType));
    }

    public override void InteractAction()
    {
        UIManager.Instance.CloseDialogue();
        if (currentOrder == null || currentOrder.currentState != OrderData.OrderState.PickedUp || !IsMatchCustomer())
        {
            UIManager.Instance.ShowDialogue("Something's wrong! Check your order!");
            return;
        }

        int totalTip = currentOrder.baseTip;
        if (currentOrder.isCookiePicked)
        {
            totalTip += currentOrder.cookieExtraTip;
            UIManager.Instance.ShowDialogue(string.Format(action_DeliverWithCookie, totalTip));
        }
        else
        {
            UIManager.Instance.ShowDialogue(string.Format(action_Deliver, totalTip));
        }

        DeliveryManager.Instance.AddEarnings(totalTip);
        currentOrder.currentState = OrderData.OrderState.Delivered;
    }

    private bool IsMatchCustomer()
    {
        if (customerID == 1 && currentOrder == DeliveryManager.Instance.order1) return true;
        if (customerID == 2 && currentOrder == DeliveryManager.Instance.order2) return true;
        return false;
    }
}