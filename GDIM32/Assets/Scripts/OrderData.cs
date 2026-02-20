using UnityEngine;

[CreateAssetMenu(fileName = "OrderData", menuName = "OrderUp/OrderData")]
public class OrderData : ScriptableObject
{
    public string customerName;
    public Sprite orderSprite;
    public string foodType;
    public int basePay;
    public int baseTip;
    public int cookieExtraTip;

    [Header("Order State")]
    public OrderStatus orderStatus;
    public bool isCookieOffered;
    public bool isCookiePicked;

    public enum OrderStatus
    {
        Unaccepted,
        Accepted,
        PickedUp,
        Delivered,
        Submitted
    }
}