using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using Inventory.Model;


public class DoorChecker : MonoBehaviour
{
    public GameObject Door;
    public ItemSO key;

    private void Start()
    {
        // Ensure InventoryController.instance is not null before using it
        if (InventoryController.instance == null)
        {
            Debug.LogError("InventoryController is not initialized!");
        }
    }

    void Update()
    {
        if(InventoryController.instance.SearchInventory(key))
        {
            Door.SetActive(true);
        }
    }
}
