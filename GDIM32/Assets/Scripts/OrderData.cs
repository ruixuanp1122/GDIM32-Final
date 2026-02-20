using UnityEngine;
[CreateAssetMenu(fileName = "OrderData", menuName = "OrderUp/OrderData", order = 0)]
public class OrderData : ScriptableObject
{
    [Header("Order Fixed Info")]
    public string customerName;
    public Sprite houseRefImage;
    public string foodType;
    public int basePay;
    public int baseTip;
    public int cookieExtraTip;

    [Header("Order Runtime State")]
    public OrderState currentState;
    public bool isCookieOffered;
    public bool isCookiePicked;

    public enum OrderState
    {
        Unaccepted,
        Accepted,
        PickedUp,
        Delivered,
        Submitted
    }

    public void ResetOrderState()
    {
        currentState = OrderState.Unaccepted;
        isCookiePicked = false;
    }
}