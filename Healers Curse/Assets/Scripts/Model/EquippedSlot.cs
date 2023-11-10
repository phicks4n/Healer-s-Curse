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

        private Sprite itemSprite;

        public void EquipGear(Sprite image)
        {
            this.itemSprite = image;
            slotImage.sprite = this.itemSprite;
            slotName.enabled = false;
        }
    }
}