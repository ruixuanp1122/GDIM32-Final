using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    public float interactDistance = 3f;
    protected PlayerController player;
    protected bool isPlayerInRange;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        isPlayerInRange = distance <= interactDistance;
    }

    public abstract void InteractTalk();
    public abstract void InteractAct();
}