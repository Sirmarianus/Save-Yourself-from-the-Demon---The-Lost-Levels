
using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
   public float radius = 3f;

   public PlayerManager player;

   bool hasInteracted = false;

   public virtual void Start()
   {
       player = PlayerManager.instance;
   }

   public virtual void Interact()
   {
       //mean to the be overwritthen
       Debug.Log("Interacting with" + transform.name);
   }

   void Update()
   {
      
           float distance = Vector3.Distance(player.player.transform.position,transform.position);
           if (distance <= radius)
           {
               Interact();
               hasInteracted = true;
           }
       
   }
   
   void OnDrawGizmosSelected()
   {
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position, radius);
   }
}
