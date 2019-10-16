using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace tutoriales
{
    public class CharController : MonoBehaviour
    {
        Transform tr;
        Rigidbody rg;
        Animator anim;
        IkHandler ik;
        /*public Transform CameraShoulder;
        public Transform CameraHolder;
        private Transform cam;/**/
        //public Transform LookAt;

        private float rotY = 0f;
        private float rotX = 0f;

        public Transform HandsPivot;
        public Transform RightHand;
        public Transform RightElbow;

        public float speed = 0.5f;
        public float rotationSpeed = 25;
        public float jumForce = 25;
        public float minAngle = -70;
        public float maxAngle = 90;
        public float cameraSpeed = 24;

        public bool OnGround = false;
        public bool Jumping = false;
        public bool Aiming = false;
        public bool Shooting = false;

        public GunController GUN;

        private Vector2 newSpeed;
        private Vector2 moveAnim;

        public InputController _input;
        // Start is called before the first frame update
        void Start()
        {
            tr = this.transform;
            rg = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            ik = GetComponentInChildren<IkHandler>();
            //ik.LookAtPosition = LookAt;
            ik.RightHandPosition = RightHand;
            ik.RightElbowPosition = RightElbow;
            //cam = Camera.main.transform;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            PlayerControl();
            //MoveControl();
            ItemsControl();
            AnimControl();
        }

      

        private void PlayerControl()
        {
            //_input.Update();
            //float d = _input.CheckF("Horizontal");
            float x = CrossPlatformInputManager.GetAxis("Horizontal");
            float y = CrossPlatformInputManager.GetAxis("Vertical");
            Aiming = CrossPlatformInputManager.GetButton("Aim");
            Shooting = CrossPlatformInputManager.GetButtonDown("Shoot");
            newSpeed = new Vector2(x, y);
            /*RaycastHit hit;
            OnGround = Physics.Raycast(this.tr.position, -tr.up, out hit, .2f);
            if (OnGround)
            {
                if (Jumping)
                {
                    rg.AddForce(tr.up * jumForce);
                }
            }*/
            Vector3 movement = new Vector3(x, 0, y);
            rg.velocity = movement * speed;
            
            if (x!=0 && y!=0)
             {
                 tr.eulerAngles = new Vector3(tr.eulerAngles.x,Mathf.Atan2(x,y)*Mathf.Rad2Deg, tr.eulerAngles.z);
             }
           
            
            /*
            float deltaX = _input.CheckF("Horizontal");
            float deltaZ = _input.CheckF("Vertical");
            float mouseX = _input.CheckF("Mouse X");
            float mouseY = _input.CheckF("Mouse Y");
            
            Vector3 sp = rg.velocity;

            float deltaX = Input.GetAxis("Horizontal");
            float deltaZ = Input.GetAxis("Vertical");
            float deltaT = Time.deltaTime;

            Vector3 side = speed * deltaX * deltaT * tr.right;
            Vector3 forward = speed * deltaZ * deltaT * tr.forward;

            Vector3 endSpeed = side + forward;
            endSpeed.y = sp.y;
            rg.velocity = endSpeed;/**/
        }
        private void MoveControl()
        {
           
        }
        private void ItemsControl()
        {
            if (GUN !=null)// can shooter
            {
                ik.LeftHandPosition = GUN.leftHandPosition;
                ik.LeftElbowPosition = GUN.leftElbowPosition;
                if (Shooting)
                {
                    GUN.Shoot();
                }
                ik.UpdateRecoil(GUN.MaxRecoil, -moveAnim.x, GUN.ShootingModifier);
            }
            //HandsPivot.position = anim.GetBoneTransform(HumanBodyBones.RightShoulder).position;
            //Quaternion localRotation = Quaternion.Euler(-rotY, HandsPivot.localRotation.y, HandsPivot.localRotation.z);
            //HandsPivot.localRotation = localRotation;
        }
        private void CameraControl()
        {
           /* float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float deltaT = Time.deltaTime;
            rotY += mouseY * deltaT * rotationSpeed;
            float xrot = mouseX * deltaT * rotationSpeed;
            tr.Rotate(0, xrot, 0);
            rotY = Mathf.Clamp(rotY, minAngle, maxAngle);
            Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0);
            CameraShoulder.localRotation = localRotation;
            cam.position = Vector3.Lerp(cam.position, CameraHolder.position, cameraSpeed * deltaT);
            cam.rotation = Quaternion.Lerp(cam.rotation, CameraHolder.rotation, cameraSpeed * deltaT);/**/
        }
        private void AnimControl()
        {

            anim.SetFloat("X", newSpeed.x);
            anim.SetFloat("Y", newSpeed.y);

            ik.Aiming = this.Aiming;
            ik.Shooting = this.Shooting;
        }

        

       
    }

}
