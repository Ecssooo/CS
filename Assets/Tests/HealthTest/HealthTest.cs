using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTest
{
    [Test]
    public void InitComponent()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        
        playerHealth.InitComponent(100);

        Assert.AreEqual(playerHealth.CurrentHealth, 100);
    }

    [Test]
    public void ThrowsTakeDamage()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        playerHealth.InitComponent(100);

        Assert.Throws<ArgumentException>(() => playerHealth.TakeDamage(-10));
    }

    [Test]
    public void TakeDamageCorrectly()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        playerHealth.InitComponent(100);
        
        playerHealth.TakeDamage(10);
        Assert.AreEqual(90, playerHealth.CurrentHealth);
    }
    
    [Test]
    public void TakeZeroDamageCorrectly()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        playerHealth.InitComponent(100);
        
        playerHealth.TakeDamage(0);
        
        Assert.AreEqual(100, playerHealth.CurrentHealth);
    }
    
    [Test]
    public void ThrowsRegeneration()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        playerHealth.InitComponent(100);

        Assert.Throws<ArgumentException>(() => playerHealth.Regeneration(-10));
    }
    
    [Test]
    public void HealCorrectly()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        playerHealth.InitComponent(100);
        
        playerHealth.TakeDamage(10);
        playerHealth.Regeneration(5);
        
        Assert.AreEqual(95, playerHealth.CurrentHealth);
    }
    
    [Test]
    public void HealMaxCorrectly()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        playerHealth.InitComponent(100);
        
        playerHealth.TakeDamage(10);
        playerHealth.Regeneration(11);
        
        Assert.AreEqual(100, playerHealth.CurrentHealth);
    }
    
    [Test]
    public void HealZeroCorrectly()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        playerHealth.InitComponent(100);
        
        playerHealth.TakeDamage(10);
        playerHealth.Regeneration(0);
        
        Assert.AreEqual(90, playerHealth.CurrentHealth);
    }
    
    
    [Test]
    public void CheckIfReallyDead()
    {
        GameObject player = new GameObject();
        Health playerHealth = player.AddComponent<Health>();
        playerHealth.InitComponent(100);
        
        Assert.AreEqual( false, playerHealth.CheckIfDead());
        playerHealth.TakeDamage(100);
        Assert.AreEqual(true, playerHealth.CheckIfDead());
    }
}
