using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    // public CameraManager camera;
    Inventory inventory;
    // Update is called once per frame
    private InventorySlot[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI; // call UpdateUI method when item change
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
        inventoryUI.SetActive(!inventoryUI.activeSelf); // hide inventory at start of the game
        // camera = CameraManager.instance;
    }
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            // camera.enabled = false;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
