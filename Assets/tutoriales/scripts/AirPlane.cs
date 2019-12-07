using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class AirPlane : MonoBehaviour
    {
        public CharController charPrefab;
        public Transform Plane;
        //public Transform PlaneCam;
        private Transform cam;

        public float Distance;
        public float Speed;

        private void OnEnable()
        {
            Vector3 pos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            pos.Normalize();
            Plane.position = this.transform.position + pos * Distance;
            Plane.LookAt(this.transform.position);

            Rigidbody rg = Plane.GetComponent<Rigidbody>();
            rg.velocity = Plane.forward * Speed;
        }
        

        // Update is called once per frame
        void Update()
        {

        }
    }

}
