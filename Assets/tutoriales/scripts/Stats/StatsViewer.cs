using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tutoriales
{
    public class StatsViewer : MonoBehaviour
    {
        Slider[] sliders;

        private void Start()
        {
            sliders = GetComponentsInChildren<Slider>();
        }

       
    }

}
