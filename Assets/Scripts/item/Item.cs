using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public virtual void OnPickUp(Player player)
    {
        Destroy(gameObject);
    } 
}
