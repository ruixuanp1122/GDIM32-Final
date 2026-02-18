using UnityEngine;
public abstract class NPCBase : MonoBehaviour
{
    public bool WantsBurger = false;
    public bool WantsPizza = false;
    public bool WantsCookie = false;

    public abstract void TriggerDialogue();
    public abstract void ReceiveBurger();
    public abstract void ReceivePizza();
    public abstract void ReceiveCookie();
}

