using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion


    Equipment[] currentEquipment;

    public Transform weaponMeshHolder;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);

    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; // ilosc slotów, tutaj 1 czyli weapon
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int) newItem.equipSlot; // bierzemy index w naszej array EquipmentSlot u mnie weapon = 0 

        Equipment oldItem = null;


        if (currentEquipment[slotIndex] != null)
        {   
            Destroy(weaponMeshHolder.GetChild(0).gameObject);
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        
        currentEquipment[slotIndex] = newItem;
        
        GameObject tempWeapon = currentEquipment[slotIndex].weapon;
        Instantiate(tempWeapon, weaponMeshHolder);
    }
}