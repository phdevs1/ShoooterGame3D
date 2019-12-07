using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class InvetaryController : MonoBehaviour
    {
        public ItemViewer ItemViewer;
        public InventaryViewer inventaryViewer;
        
        public List<InventaryGroup> inventaryGroups;
        private Dictionary<string, InventaryGroup> mappedInventary = new Dictionary<string, InventaryGroup>();

        public void Initialize(bool player)
        {
            MapInventaryGroup();
            if (player)
            {
                inventaryViewer.Initialize(this);
            }
            else
            {
                ItemViewer = null;
                inventaryViewer = null;
            }
            
        }
        private void MapInventaryGroup()
        {
            mappedInventary.Clear();
            foreach (InventaryGroup g in inventaryGroups)
            {
                if (!mappedInventary.ContainsKey(g.SlotType))
                {
                    mappedInventary.Add(g.SlotType, g);
                }
            }
        }
        public bool AddItem(ItemController item)
        {
            if (mappedInventary.ContainsKey(item.Stats.SlotType))
            {
                return mappedInventary[item.Stats.SlotType].AddItem(item);
            }
            return false;
        }
        public ItemController GetSelectedAt(string group)
        {
            if (!mappedInventary.ContainsKey(group))
                return null;
            return mappedInventary[group].GetSelected();
        }
        public InventaryGroup GetGroup(string group)
        {
            if (!mappedInventary.ContainsKey(group))
                return null;
            return mappedInventary[group];
        }
        public bool RemoveItem(ItemController item)
        {
            if (mappedInventary.ContainsKey(item.Stats.SlotType))
            {
                return mappedInventary[item.Stats.SlotType].RemoveItem(item);
            }
            return false;
        }
       
    }
    [System.Serializable]// this is for recognize in inventaryController
    public class InventaryGroup
    {
        public string SlotType;
        public Transform RealPosition;
        public int MaxCapacity = 3;
        public bool ReplaceSelectedOnMax = true;
        
        public ItemEvent AddedItem;
        public ItemEvent RemovedItem;
        private int selIndex;
        public int SelectedIndex
        {
            set { selIndex = value; }
            get { return selIndex; }
        }
        public List<ItemController> items;

        public ItemController GetSelected()
        {
            return GetItemAt(selIndex);
        }
        public bool AddItem(ItemController item)
        {
            if (items.Count >= MaxCapacity)
            {
                /*if (ReplaceSelectedOnMax)
                {
                    DropItem(selIndex, item.transform);
                   
                    items[selIndex] = item;
                    item.Take(RealPosition, true);
                   
                    if (AddedItem != null)
                        AddedItem(item);
                    return true; 
                    
                }/**/
                return false; 
            }
                
            items.Add(item);
            item.Take(RealPosition, items.Count == 1);
            if (AddedItem != null)
                AddedItem(item);
            return true;
        }
        public bool DropItem(int index, Transform dropTransform)
        {
            ItemController ic = GetItemAt(index);
            if (ic != null)
            {
                ic.Drop(dropTransform, index == selIndex);
                if (RemovedItem != null)
                    RemovedItem(ic);
                return true;
            }
            return false;
        }
        public ItemController GetItemAt(int index)
        {
            if (items.Count == 0)
                return null;
            if (index < 0)
                return null;
            if (index < items.Count)
                return items[index];
            return null;
        }
        public int GetIndex(ItemController itm)
        {
            return items.IndexOf(itm);
        }
        public void Select(ItemController itm)
        {
            int indx = GetIndex(itm);
            if (indx != SelectedIndex)
            {
                ItemController item = GetSelected();
                if (item != null)
                {
                    item.Hide();
                }
                SelectedIndex = indx;
                item = GetSelected();
                if (item != null)
                {
                    Debug.Log("----------------------------------");
                    item.Show();
                }
            }
            
        }
        public bool RemoveItem(ItemController item)
        {
            if (items.Remove(item))
            {
                if (RemovedItem != null)
                    RemovedItem(item);
                return true;
            }
            return false;
        }

    }

    public delegate void ItemEvent(ItemController item);
}
