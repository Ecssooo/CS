using System;
using UnityEngine;

public class PotionExhaust : Item
{
    [SerializeField] private int _exhaustAmount;

    public int ExhaustAmount => _exhaustAmount;
    
    public PotionExhaust(int healAmount)
    {
        _exhaustAmount = healAmount;
    }
    
    public override void OnPickUp(Player player)
    {
        base.OnPickUp(player);
        Stamina staminaController = player.gameObject.GetComponent<Stamina>();
        staminaController.DecreaseStamina(_exhaustAmount);
    }
}
