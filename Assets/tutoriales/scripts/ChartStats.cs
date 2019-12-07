using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    [CreateAssetMenu(fileName = "stats", menuName = "tutoriales/char stats")]
    public class ChartStats : ScriptableObject
    {
        public float speed = 0.1f;
        /*public float rotationSpeed = 25;
        public float jumForce = 25;
        public float minAngle = -70;
        public float maxAngle = 90;
        public float cameraSpeed = 24;*/

        public float throwForce = 50;

        public float MaxLife = 100;
    }

}
