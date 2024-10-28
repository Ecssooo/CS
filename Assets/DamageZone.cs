using System;
using System.Net.NetworkInformation;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            player.CurrentLife = other.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
