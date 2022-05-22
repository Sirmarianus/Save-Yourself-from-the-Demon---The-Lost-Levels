using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    
    public HealthBarPlayer healthBar;
    public int maxHealth = 100;
    public int currentHealth { get;  set; }
    public Stat damage;
    public Stat health;
    public bool dead = false;


    void Awake()
    {
        currentHealth = maxHealth;
        Debug.Log(healthBar);
        healthBar.SetMaxHealth(maxHealth);
    }
    
    // private void Start()
    // {
    //     healthBar.SetMaxHealth(maxHealth);
    // }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + "takes" + damage + "damage.");
        if(healthBar != null)
            healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }
    
    public virtual void Die()
    {
        // Die in some way
        // meant to be overwritten
        Debug.Log(transform.name + "died");
    }
   
}
