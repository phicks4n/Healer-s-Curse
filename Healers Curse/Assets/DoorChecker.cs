using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using Inventory.Model;


public class DoorChecker : MonoBehaviour
{
    public GameObject Door;
    public ItemSO key;


    void Update()
    {
        if(InventoryController.instance.SearchInventory(key))
        {
            Door.SetActive(true);
        }
    }
}
