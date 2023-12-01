using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using Inventory.Model;

public class DoorDisappear : MonoBehaviour
{
    public ItemSO key;

    void Start()
    {
        this.gameObject.SetActive(false);

        if(InventoryController.instance.SearchInventory(key))
        {
            this.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        InventoryController.instance.RemoveItem(key);
    }
}
