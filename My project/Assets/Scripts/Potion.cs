using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Potion")]
public class Potion : Item
{
    Transform player;
    private PlayerStats stats;
    private int healValue;
    public GameObject UseParticle;
    
    public override void Use()
    {
        healValue = 50;
        player = PlayerManager.instance.player.transform;
        stats = player.GetComponent<PlayerStats>();
        if (stats.currentHealth < stats.maxHealth)
        {
            int hpDiff = stats.maxHealth - stats.currentHealth;
            if (hpDiff < healValue)
            {
                stats.currentHealth = stats.maxHealth;
                Instantiate(UseParticle, new Vector3(player.position.x, player.position.y, player.position.z),player.rotation, player);
            }
            else
            {
                stats.currentHealth += 50;
                Instantiate(UseParticle, new Vector3(player.position.x, player.position.y, player.position.z),player.rotation, player);
            }
            stats.healthBar.SetHealth(stats.currentHealth);
        }
        base.Use();
        RemoveFromInventory();
    }
}
