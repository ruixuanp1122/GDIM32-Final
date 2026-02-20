using UnityEngine;

public class CookieTable : MonoBehaviour
{
    public float interactDistance = 3f;
    private PlayerController player;
    private bool isPlayerInRange;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        isPlayerInRange = Vector3.Distance(transform.position, player.transform.position) <= interactDistance;
    }

    private void OnMouseDown()
    {
        if (isPlayerInRange && DeliveryManager.Instance.currentActiveOrder != null && DeliveryManager.Instance.currentActiveOrder.isCookieOffered)
        {
            PickUpCookie();
        }
        else if(!DeliveryManager.Instance.currentActiveOrder.isCookieOffered)
        {
            UIManager.Instance.ShowDialogue("No fortune cookie available yet!");
        }
    }

    private void PickUpCookie()
    {
        DeliveryManager.Instance.currentActiveOrder.isCookiePicked = true;
        UIManager.Instance.ShowHoldIcon(DeliveryManager.Instance.currentActiveOrder.foodType + "_Cookie");
        UIManager.Instance.ShowDialogue("Fortune cookie picked up! Extra tip!");
    }
}