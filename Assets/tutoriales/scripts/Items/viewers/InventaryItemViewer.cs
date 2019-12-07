using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tutoriales
{
    public class InventaryItemViewer : MonoBehaviour
    {
        InventaryGroupViewer inventaryGroupViewer;
        Image itemIcon;
        internal ItemController invItem;
        internal Transform mtransform;

        public Sprite defaultSprinte;

        public void Initialize(InventaryGroupViewer inventaryGroup)
        {
            this.mtransform = this.transform;
            this.inventaryGroupViewer = inventaryGroup;
            itemIcon = GetComponent<Image>();

        }
        public void SetItem(ItemController item)
        {
            this.invItem = item;
            itemIcon.sprite = (item != null ? item.Stats.Icon : defaultSprinte);
            
        }

        public void OnClickSelect()
        {
            inventaryGroupViewer.thisGroup.Select(invItem);
        }
    }

}
