using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public virtual void OnPickUp(Player player)
    {
        if (player == null) throw new ArgumentException("Player null", "player");
        Destroy(gameObject);
    } 
}
