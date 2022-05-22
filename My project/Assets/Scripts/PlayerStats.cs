using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : CharacterStats
{
    private int level = 1;
    public float experience { get; private set; }
    public ExperienceBar experienceBar;
    public TMP_Text levelText;
    private Transform player;
    public GameObject LevelParticle;
    public void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        experienceBar.SetExp(0);
        levelText.text = "1";
        player = PlayerManager.instance.player.transform;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("jazda");

        PlayerManager.instance.player.transform.position = new Vector3(335, 15, 12);
        healthBar.SetHealth(maxHealth);
        currentHealth = maxHealth;
    }

    public void SetExperience(float exp)
    {
        experience += exp;
        float experienceNeeded = GameLogic.ExperienceForNextLevel(level);
        float previousExperience = GameLogic.ExperienceForNextLevel(level - 1);
        if (experience >= experienceNeeded)
        {
            LevelUp();
            experienceNeeded = GameLogic.ExperienceForNextLevel(level);
            previousExperience = GameLogic.ExperienceForNextLevel(level - 1);
        }
        experienceBar.SetExpNeeded(experienceNeeded - previousExperience);
        experienceBar.SetExp(experience-previousExperience);
    }

    void LevelUp()
    {
        level++;
        levelText.text = level.ToString();
        Instantiate(LevelParticle, new Vector3(player.position.x, player.position.y, player.position.z),
            player.rotation, player);
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
            damage.AddModifier(newItem.damageModifier);
        if(oldItem != null)
            damage.RemoveModifier(oldItem.damageModifier);
    }
}