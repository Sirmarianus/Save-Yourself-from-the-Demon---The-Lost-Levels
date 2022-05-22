using UnityEngine;

public class ItemPickup : Interactable
{
   public Item item;
   public override void Interact()
   {
      base.Interact();
      if (Input.GetKeyDown(KeyCode.X))
      {
         PickUp();
      }
     
   }

   void PickUp()
   {
      Debug.Log("Picking up" + item.name);
      // Add to inventory
      bool wasPickedUp = Inventory.instance.Add(item);
      if (wasPickedUp)
      {
         Destroy(gameObject);
      };
     
   }
}
