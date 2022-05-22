using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats : CharacterStats
{

  public Transform player;
  public List<GameObject> DropItems = new List<GameObject>();
  [SerializeField]public int experienceGranted;
  private Animator anim;
  
  public override void Die()
  {
    anim = GetComponentInChildren<Animator>();
    player = PlayerManager.instance.player.transform;
    base.Die();
    player.GetComponent<PlayerStats>().SetExperience(experienceGranted);
    anim.SetTrigger("Die");
    StartCoroutine(WaitForDeath(gameObject));
  }

  public void DropItem()
  {
    Vector3 dropPosition = transform.position;
    if (DropItems.Count != 0)
    {
      float randomItem = Random.Range(1, 101);
      if (randomItem > 90)
      {
        Instantiate(DropItems[0], dropPosition, Quaternion.identity);
      }
      else
      {
        Instantiate(DropItems[1], dropPosition, Quaternion.identity);
      }
      
    }
  }

  IEnumerator WaitForDeath(GameObject gameObject)
  {
    yield return new WaitForSeconds(1.5f);
    DropItem();
    Destroy(gameObject);
  }

 

}
