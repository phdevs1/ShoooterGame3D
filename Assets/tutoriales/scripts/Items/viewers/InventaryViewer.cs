﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class InventaryViewer : MonoBehaviour
    {
        public List<InventaryGroupViewer> viewers = new List<InventaryGroupViewer>();
        Dictionary<string, InventaryGroupViewer> mappedViewers = new Dictionary<string, InventaryGroupViewer>();
       

        internal void Initialize(InvetaryController invetaryController)
        {
            foreach (InventaryGroupViewer v in viewers)
            {
                if (!mappedViewers.ContainsKey(v.SlotType))
                {
                    mappedViewers.Add(v.SlotType, v);
                    InventaryGroup group = invetaryController.GetGroup(v.SlotType);
                    if (group != null)
                    {
                        v.Initialize(group);
                    }
                }
            }
        }
    }

}
