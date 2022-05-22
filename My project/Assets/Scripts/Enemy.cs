using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles interaction with the Enemy */

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    // PlayerManager playerManager;
    CharacterStats myStats;

    public override void Start()
    {
        
        player = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = player.player.GetComponent<CharacterCombat>();
        // if (playerCombat != null && Input.GetMouseButtonDown(0))
        // {
        //     playerCombat.Attack(myStats);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterCombat playerCombat = player.player.GetComponent<CharacterCombat>();
        Debug.Log(other.name);
        if (other.tag == "Sword")
        {
            playerCombat.Attack(myStats);
            other.enabled = false;
        }
    }
}