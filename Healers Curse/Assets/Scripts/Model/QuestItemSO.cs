using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class QuestItemSO : ItemSO, IDestroyableItem
    {
        public interface IDestroyableItem
        {

        }
    }
}