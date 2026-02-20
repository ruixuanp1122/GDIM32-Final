using UnityEngine;
[RequireComponent(typeof(Collider))]
public abstract class NPC : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactDistance = 3f;
    protected OrderData currentOrder;

    private void Update()
    {
        currentOrder = DeliveryManager.Instance.currentActiveOrder;
    }

    public bool IsInInteractRange(Transform player)
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= interactDistance;
    }

    public abstract void InteractTalk();
    public abstract void InteractAction();
}