/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class InventaryController : MonoBehaviour
    {
        public ItemViewer ItemViewer;

        public List<InventaryGroup> InventaryGroups;
        private Dictionary<string, InventaryGroup> mappedInventary = new Dictionary<string, InventaryGroup>();

        private void Start()
        {
            MapInventaryGroups();
        }

        private void MapInventaryGroups()
        {
            mappedInventary.Clear();
            foreach(InventaryGroup g in InventaryGroups)
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

        
    }

    [System.Serializable]
    public class InventaryGroup
    {
        public string SlotType;
        public Transform RealPosition;
        public int MaxCapacity = 3;

        private int selIndex;
        public int SelectedIndex
        {
            set { selIndex = value; }
            get { return selIndex; }
        }

        public List<ItemController> items;

        public ItemController GetSelected()
        {
            return items[selIndex];
        }

        public bool AddItem(ItemController item)
        {
            if (items.Count >= MaxCapacity)
                return false;
            items.Add(item);
            item.Take(RealPosition);
            return true;
        }
        
    }
}*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;	

namespace tutoriales
{
    public class InventaryController : MonoBehaviour
    {
        public ItemViewer ItemViewer;
        //public InventaryViewer inventaryViewer;

        public List<InventaryGroup> inventaryGroups;

        private Dictionary<string, InventaryGroup> mappedInventary = new Dictionary<string, InventaryGroup>();

        private void Start()
        {
            MapInventaryGroups();
            //inventaryViewer.Initialize(this);
        }

        private void MapInventaryGroups()
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
                bool result = mappedInventary[item.Stats.SlotType].AddItem(item);
                return result;
            }
            return false;
        }

        public ItemController GetSelectedAt(string group)
        {
            InventaryGroup invgroup = GetGroup(group);
            if (invgroup == null)
                return null;
            return invgroup.GetSelected();
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

    [System.Serializable]
    public class InventaryGroup
    {
        public string SlotType;
        public Transform RealPosition;
        public int MaxCapacity = 3;
        public bool ReplaceSelectedOnMax = true;

        public ItemEvent AddedItem;
        public ItemEvent RemovedItem;

        private int selIndex;
        public int SelectedIndex { set { selIndex = value; } get { return selIndex; } }

        public List<ItemController> items;
        public ItemController GetSelected()
        {
            if (items.Count == 0) return null;
            if (selIndex < 0) return null;
            if (selIndex < items.Count)
                return items[selIndex];
            return null;
        }

        public bool AddItem(ItemController item)
        {
            if (items.Count >= MaxCapacity)
            {
                if (ReplaceSelectedOnMax)
                {
                    ItemController ic = GetSelected(); ic.Drop(); items[selIndex] = item; item.Take(RealPosition);
                    if (RemovedItem != null) RemovedItem(ic); if (AddedItem != null) AddedItem(item); return true;
                }
                return false;
            }
            items.Add(item); item.Take(RealPosition);
            if (AddedItem != null) AddedItem(item);
            return true;
        }

        public int GetIndex(ItemController itm) { return items.IndexOf(itm); }
        public void Select(ItemController itm)
        {
            int indx = GetIndex(itm);
            SelectedIndex = indx;
        }

        public bool RemoveItem(ItemController item)
        {
            if (items.Remove(item))
            {
                if (RemovedItem != null) RemovedItem(item);
                return true;
            }
            return false;
        }


    }

    public delegate void ItemEvent(ItemController item);
}
