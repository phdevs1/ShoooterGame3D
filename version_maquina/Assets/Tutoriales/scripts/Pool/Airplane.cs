using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class Airplane : MonoBehaviour
    {
        public CharController charPrefab;
        public Transform Plane;
        public Transform PlaneCam;
        public Transform PlaneCamRotator; 
        private Transform cam;

        public float Distance;
        public float MinDistanceJump;
        public float Speed;
        public float rotationSpeed;

        public bool PrepareForLunch = false;
        public bool Launched = false;

        private void OnEnable()
        {
            /*cam = Camera.main.transform;
            View = GetComponent<PhotonView>();
            rg = Plane.GetComponent<Rigidbody>();

            if (PhotonNetwork.IsMasterClient)
                ResetRandomPos();*/

            //Vector3 pos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Vector3 pos = new Vector3(0, 0, 0);
            pos.Normalize();

            //Plane.position = this.transform.position + pos * Distance;
            Plane.position = this.transform.position;
            Plane.LookAt(this.transform.position);

            Rigidbody rg = Plane.GetComponent<Rigidbody>();
            //rg.velocity = Plane.forward * Speed;

            cam = Camera.main.transform;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!Launched)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");
                Vector2 mouseDelta = new Vector2(mouseX, mouseY);
                float xrot = mouseDelta.x * Time.deltaTime * rotationSpeed;

                PlaneCamRotator.Rotate(0, xrot, 0);

                cam.position = PlaneCam.position;
                cam.LookAt(Plane);

                cam.position = PlaneCam.position;
                cam.LookAt(Plane);

                bool distance = Vector3.Distance(this.transform.position, Plane.position) < MinDistanceJump;
                if (distance)
                {
                    float jump = Input.GetAxis("Jump");
                    if(jump == 1)
                    {
                        Launched = true;

                        Photon.Pun.PhotonNetwork.Instantiate(charPrefab.name, Plane.position, Quaternion.identity);
                        /*CharController player = Instantiate<CharController>(charPrefab);
                        player.transform.position = Plane.position;*/
                    }
                }
            }          
        }

    }
}
