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

        [SerializeField]
        private Image baseImage;
        [SerializeField]
        public Sprite baseSprite;
/*
        public void EquipGear(Sprite image)
        {
            this.itemSprite = image;
            slotImage.sprite = this.itemSprite;
            slotName.enabled = false;
        }*/

        public int EquipGear(ItemSO item)
        {
            if (item != null)
            {
                equipItem = item;
                this.itemSprite = item.ItemImage;
                slotImage.sprite = this.itemSprite;
                slotName.enabled = false;
            }
            else
            {
                ResetSlot();
            }
            return 0;
        }

        public void setSlot(EquippedSlot otherSlot)
        {
            equipItem = otherSlot.equipItem;
            itemSprite = otherSlot.itemSprite;
            slotImage.sprite = this.itemSprite;
            slotName.enabled = false;
        }

        public void UnequipGear()
        {
            equipItem = null;
            ResetSlot();
        }

        private void ResetSlot()
        {
            this.itemSprite = baseSprite; // Assuming baseSprite is a default sprite
            slotImage.sprite = this.itemSprite;
            slotName.enabled = true;
        }

        public ItemSO GetEquippedGear()
        {
            return equipItem;
        }
    }
}