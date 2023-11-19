using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory.Model
{
    public class EquippedSlot : MonoBehaviour
    {
        [SerializeField]
        private Image slotImage;
        
        [SerializeField]
        private TMP_Text slotName;

        public Sprite itemSprite;
        [SerializeField]
        public ItemSO equipItem;
/*
        public void EquipGear(Sprite image)
        {
            this.itemSprite = image;
            slotImage.sprite = this.itemSprite;
            slotName.enabled = false;
        }*/

        public void EquipGear(ItemSO item)
        {
            equipItem = item;
            this.itemSprite = item.ItemImage;
            slotImage.sprite = this.itemSprite;
            slotName.enabled = false;
        }

        public void setSlot(EquippedSlot otherSlot)
        {
            equipItem = otherSlot.equipItem;
            itemSprite = otherSlot.itemSprite;
            slotImage.sprite = this.itemSprite;
            slotName.enabled = false;
        }
    }
}