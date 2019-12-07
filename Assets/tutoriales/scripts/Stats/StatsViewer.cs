using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tutoriales
{
    public class StatsViewer : MonoBehaviour
    {
        static public StatsViewer Viewer;
        public Slider[] sliders;

        private void OnEnable()
        {
            Viewer = this;
            sliders = GetComponentsInChildren<Slider>();
        }
        public void Add(Stat stat)
        {
            stat.StatChanged += new StatEvent(OnStatChange);
            OnStatChange(stat);
        }
        public void Test()
        {
            Debug.Log("entroooooooooooooooooooooooooooooooooooooooooooooooooo");
        }
        public void OnStatChange(Stat stat)
        {
            foreach (Slider sl in sliders)
            {
                if (sl.name.Contains(stat.Name))
                {
                    sl.value = stat.Percentage();
                }
            }
        }

    }

}
