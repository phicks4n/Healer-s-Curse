using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using Inventory.Model;

public class Bar : MonoBehaviour
{
    public ItemSO key;

    void Start()
    {
        if(InventoryController.instance.SearchInventory(key))
        {
            this.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(InventoryController.instance.SearchInventory(key))
        {
            this.gameObject.SetActive(false);
        }
    }
}
