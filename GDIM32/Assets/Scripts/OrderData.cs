using UnityEngine;

[CreateAssetMenu]
public class OrderData : ScriptableObject
{
    public int basePay;
    public OrderState state;
}

public enum OrderState
{
    Unaccepted,
    Accepted,
    Delivered
}