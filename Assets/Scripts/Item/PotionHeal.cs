using UnityEngine;

public class PotionHeal : Item
{
    [SerializeField] private int _healAmount;

    public int HealAmount => _healAmount;
    
    public PotionHeal(int healAmount)
    {
        _healAmount = healAmount;
    }
    
    public override void OnPickUp(Player player)
    {
        base.OnPickUp(player);
        Health healthComponent = player.gameObject.GetComponent<Health>();
        healthComponent.Regeneration(_healAmount);
    }
}
