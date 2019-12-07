using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class CharController : Photon.Pun.MonoBehaviourPun
    {
        public bool Player = true;

        Transform tr;
        Rigidbody rg;
        Animator anim;
        CapsuleCollider coll;
        IkHandler ik;

        public Transform CameraShoulder;
        public Transform CameraHolder;
        public Transform LookAt;
        private Transform cam;

        private float rotY = 0f;
        private float rotX = 0f;

        public Transform HandsPivot;
        public Transform RightHand;
        public Transform RightElbow;

        public bool Aiming = false;
        public bool Shooting = false;

        public GunController GUN;

        public float speed = 200;
        public float rotationSpeed = 25;
        public float minAngle = -70;
        public float maxAngle = 70;
        public float cameraSpeed = 24;

        private Vector2 moveDelta;
        private Vector2 mouseDelta;
        private Vector2 moveAnim;
        private float deltaT;

        public InputController _input;


        // Use this for initialization	    
        void Start()
        {
            tr = this.transform;
            rg = GetComponent<Rigidbody>();
            coll = GetComponent<CapsuleCollider>();
            anim = GetComponentInChildren<Animator>();
            cam = Camera.main.transform;

            ik = GetComponentInChildren<IkHandler>();
            ik.LookAtPosition = LookAt;
            ik.RightHandPosition = RightHand;
            ik.RightElbowPosition = RightElbow;
            cam = Camera.main.transform;
        }


        // Update is called once per frame	    
        void FixedUpdate()
        {
            if (Player)
            {
                PlayerControl();
                CameraControl();
            }
            else
            {
                Player = photonView.IsMine;
                /*if (Player = (Photon.Pun.PhotonNetwork.CurrentRoom == null || photonView.IsMine))
                {
                    InitStatsInventary();
                }*/
            }
            MoveControl();
            ItemsControl();
            AnimControl();
        }

        

        private void PlayerControl()
        {
            _input.Update();

            float deltaX = _input.Checkf("Horizontal");
            float deltaZ = _input.Checkf("Vertical");
            float mouseX = _input.Checkf("Mouse X");
            float mouseY = _input.Checkf("Mouse Y");
            
            Aiming = _input.Check("Fire2");
            Shooting = _input.Check("Fire1");

            moveDelta = new Vector2(deltaX, deltaZ);
            mouseDelta = new Vector2(mouseX, mouseY);
            deltaT = Time.deltaTime;
        }

        private void MoveControl()
        {
            Vector3 side = speed * moveDelta.x * deltaT * tr.right;
            Vector3 forward = speed * moveDelta.y * deltaT * tr.forward;

            Vector3 endSpeed = side + forward;
            Vector3 sp = rg.velocity;
            endSpeed.y = sp.y;

            rg.velocity = endSpeed;
        }

        private void ItemsControl()
        {
            if (GUN != null)
            {
                ik.LeftHandPosition = GUN.leftHandPosition; ik.LeftElbowPosition = GUN.leftElbowPosition;
                if (Shooting) { GUN.Shoot(); }
                ik.UpdateRecoil(GUN.MaxRecoil, -moveAnim.x, GUN.ShootingModifier);
            }
            HandsPivot.position = anim.GetBoneTransform(HumanBodyBones.RightShoulder).position;
            Quaternion localRotation = Quaternion.Euler(-rotY, HandsPivot.localRotation.y, HandsPivot.localRotation.z);
            HandsPivot.localRotation = localRotation;
        }

        private void CameraControl()
        {
            rotY += mouseDelta.y * deltaT * rotationSpeed;
            float xrot = mouseDelta.x * deltaT * rotationSpeed;
            tr.Rotate(0, xrot, 0);
            rotY = Mathf.Clamp(rotY, minAngle, maxAngle);
            Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0); CameraShoulder.localRotation = localRotation;
            cam.position = Vector3.Lerp(cam.position, CameraHolder.position, cameraSpeed * deltaT);
            cam.rotation = Quaternion.Lerp(cam.rotation, CameraHolder.rotation, cameraSpeed * deltaT);
        }

        private void AnimControl()
        {
            anim.SetFloat("X", moveDelta.x);
            anim.SetFloat("Y", moveDelta.y);

            ik.Aiming = this.Aiming;
            ik.Shooting = this.Shooting;
        }
    }
}

/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class CharController : Photon.Pun.MonoBehaviourPun
    {
        public bool Active = true;
        public bool Player = true;

        Transform tr;
        Rigidbody rg;
        Animator anim;
        CapsuleCollider coll;
        IkHandler ik;

        public Transform CameraShoulder;
        public Transform CameraHolder;
        public Transform LookAt;
        private Transform cam;

        private float rotY = 0f;
        private float rotX = 0f;

        public Transform HandsPivot;
        public Transform RightHand;
        public Transform RightElbow;

        //public CharStats Stats;
        private States states;

        public GunController GUN;

        public float speed = 10;
        public float rotationSpeed = 50;
        public float minAngle = -70;
        public float maxAngle = 90;
        public float cameraSpeed = 24;

        private Vector2 moveDelta;
        private Vector2 mouseDelta;
        private Vector2 moveAnim;
        private float deltaT;

        public InputController _input;


        // Start is called before the first frame update
        void Start()
        {
            tr = this.transform;
            rg = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            ik = GetComponentInChildren<IkHandler>();
            ik.LookAtPosition = LookAt;
            ik.RightHandPosition = RightHand;
            ik.RightElbowPosition = RightElbow;
            cam = Camera.main.transform;

        }

        void FixedUpdate()
        {
            if (Player)
            {
                PlayerControl();
                CameraControl();
            }
            else
            {
                Player = photonView.IsMine; 
            }

            if (!Active)
            {
                return;
            }

            MoveControl();
            ItemsControl();
            AnimControl();
        }

        private void PlayerControl()
        {
            _input.Update();

            float deltaX = _input.Checkf("Horizontal");
            float deltaZ = _input.Checkf("Vertical");
            float mouseX = _input.Checkf("Mouse X");
            float mouseY = _input.Checkf("Mouse Y");
            //states.Taking = _input.Check("Interact");
            states.Aiming = _input.Check("Fire2");
            states.Shooting = _input.Check("Fire1");

            moveDelta = new Vector2(deltaX, deltaZ);
            mouseDelta = new Vector2(mouseX, mouseY);

            deltaT = Time.deltaTime;

            
        }

        private void MoveControl()
        {
            Vector3 sp = rg.velocity;

            Vector3 side = speed * moveDelta.x * deltaT * tr.right;
            Vector3 forward = speed * moveDelta.y * deltaT * tr.forward;

            Vector3 endSpeed = side + forward;

            rg.velocity = endSpeed;

            //Vector2 moveAnim = moveDelta * (Running ? 2 : 1);
        }

        private void ItemsControl()
        {
            /*if (GUN != null)
            {
                ik.LeftHandPosition = GUN.leftHandPosition; ik.LeftElbowPosition = GUN.leftElbowPosition;
                if (states.Shooting)
                {
                    GUN.Shoot();
                }
                ik.UpdateRecoil(GUN.MaxRecoil, -moveAnim.x, GUN.ShootingModifier);
            }
            
        }

        private void CameraControl()
        {
            rotY += mouseDelta.y * deltaT * rotationSpeed;
            float xrot = mouseDelta.x * deltaT * rotationSpeed;

            tr.Rotate(0, xrot, 0);

            rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

            Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0);
            CameraShoulder.localRotation = localRotation;

            cam.position = Vector3.Lerp(cam.position, CameraHolder.position, cameraSpeed * deltaT);
            cam.rotation = Quaternion.Lerp(cam.rotation, CameraHolder.rotation, cameraSpeed * deltaT);
        }

        private void AnimControl()
        {
            HandsPivot.position = anim.GetBoneTransform(HumanBodyBones.RightShoulder).position;
            Quaternion localRotation = Quaternion.Euler(-rotY, HandsPivot.localRotation.y, HandsPivot.localRotation.z);
            HandsPivot.localRotation = localRotation;

            anim.SetFloat("X", moveDelta.x);
            anim.SetFloat("Y", moveDelta.y);

            ik.Aiming = states.Aiming;
            ik.Shooting = states.Shooting;
        }
    }

    public class States
    {
        public bool Aiming = false;
        public bool Shooting = false;
        public bool Taking = false;
    }

}
*/

