using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tutoriales
{
    public class Stat : MonoBehaviour
    {
        private float max;
        private float value;

        public float Value
        {
            get { return value; }
            set
            {
                this.value = value;
            }
        }
    }

}
