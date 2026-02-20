using UnityEngine;
public class CookieTable : MonoBehaviour
{
    [Header("Cookie Settings")]
    public float interactDistance = 2f;
    public string talk_NoCookieOffer = "No cookie for this order!";
    public string talk_AlreadyPicked = "You already have a cookie!";
    public string action_PickupCookie = "You picked up a fortune cookie! Extra tip!";

    private OrderData currentOrder;

    private void Update()
    {
        currentOrder = DeliveryManager.Instance.currentActiveOrder;
    }

    public void PickupCookie(Transform player)
    {
        UIManager.Instance.CloseDialogue();
        if (Vector3.Distance(transform.position, player.position) > interactDistance) return;

        if (currentOrder == null || currentOrder.currentState != OrderData.OrderState.Accepted || !currentOrder.isCookieOffered)
        {
            UIManager.Instance.ShowDialogue(talk_NoCookieOffer);
            return;
        }

        if (currentOrder.isCookiePicked)
        {
            UIManager.Instance.ShowDialogue(talk_AlreadyPicked);
            return;
        }

        currentOrder.isCookiePicked = true;
        UIManager.Instance.ShowDialogue(action_PickupCookie);
        UIManager.Instance.ShowHoldIcon(currentOrder.foodType + "_Cookie");
    }
}