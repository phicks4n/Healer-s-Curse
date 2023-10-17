using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using System.Text;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private InventoryPage inventoryUI;

        [SerializeField]
        private InventoryPage equipmentUI;

        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField]
        private InventorySO equipmentData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                {
                    continue;
                }
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            equipmentUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                equipmentUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            equipmentUI.InitializeInventoryUI(inventoryData.Size);
            this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            this.equipmentUI.OnDescriptionRequested += HandleDescriptionRequest;
            this.inventoryUI.OnSwapItems += HandleSwapItems;
            this.equipmentUI.OnSwapItems += HandleSwapItems;
            this.inventoryUI.OnStartDragging += HandleDragging;
            this.equipmentUI.OnStartDragging += HandleDragging;
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            this.equipmentUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            if (inventoryUI.isActiveAndEnabled == true)
            {
                InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);

                if (inventoryItem.IsEmpty)
                {
                    return;
                }

                IItemAction itemAction = inventoryItem.item as IItemAction;

                if (itemAction != null)
                {
                    inventoryUI.ShowItemAction(itemIndex);
                    inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
                }

                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;

                if (destroyableItem != null)
                {
                    inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
                }
            }
            else if (equipmentUI.isActiveAndEnabled == true)
            {
                InventoryItem equipmentItem = equipmentData.GetItemAt(itemIndex);

                if (equipmentItem.IsEmpty)
                {
                    return;
                }

                IItemAction equipAction = equipmentItem.item as IItemAction;

                if (equipAction != null)
                {
                    equipmentUI.ShowItemAction(itemIndex);
                    equipmentUI.AddAction(equipAction.ActionName, () => PerformAction(itemIndex));
                }

                IDestroyableItem destroyableEquip = equipmentItem.item as IDestroyableItem;

                if (destroyableEquip != null)
                {
                    equipmentUI.AddAction("Drop", () => DropItem(itemIndex, equipmentItem.quantity));
                }
            }
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                {
                    inventoryUI.ResetSelection();
                }
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            if (inventoryUI.isActiveAndEnabled == true)
            {
                inventoryData.RemoveItem(itemIndex, quantity);
                inventoryUI.ResetSelection();
                audioSource.PlayOneShot(dropClip);
            }
            else if (equipmentUI.isActiveAndEnabled == true)
            {
                inventoryData.RemoveItem(itemIndex, quantity);
                equipmentUI.ResetSelection();
                audioSource.PlayOneShot(dropClip);
            }
        }

        private void HandleDragging(int itemIndex)
        {
            if (inventoryUI.isActiveAndEnabled == true)
            {
                InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
                if (inventoryItem.IsEmpty)
                {
                    return;
                }
                inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
            }
            else if (equipmentUI.isActiveAndEnabled == true)
            {
                InventoryItem equipmentItem = inventoryData.GetItemAt(itemIndex);
                if (equipmentItem.IsEmpty)
                {
                    return;
                }
                equipmentUI.CreateDraggedItem(equipmentItem.item.ItemImage, equipmentItem.quantity);
            }
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            if (inventoryUI.isActiveAndEnabled == true)
            {
                inventoryData.SwapItems(itemIndex_1, itemIndex_2);
            }
            else if (equipmentUI.isActiveAndEnabled == true)
            {
                inventoryData.SwapItems(itemIndex_1, itemIndex_2);
            }
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName}" + $": {inventoryItem.itemState[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (equipmentUI.isActiveAndEnabled == true)
                {
                    equipmentUI.Hide();
                }

                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventoryUI.isActiveAndEnabled == true)
                {
                    inventoryUI.Hide();
                }

                if (equipmentUI.isActiveAndEnabled == false)
                {
                    equipmentUI.Show();
                    foreach (var item in equipmentData.GetCurrentInventoryState())
                    {
                        equipmentUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                    equipmentUI.Hide();
                }
            }
        }
    }
}