using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace tutoriales
{
   
    public class CharController : MonoBehaviour
    {
        public bool Active = true;
        public bool Player = true;
        Transform tr;
        Rigidbody rg;
        //Animator anim;
        internal Animator anim;
        RagdollController ragdoll;
        internal InvetaryController inventary;
        internal  IkHandler ik;
        /*public Transform CameraShoulder;
        public Transform CameraHolder;
        private Transform cam;/**/
        public Transform LookAt;

        public Transform throwOrigin;
        /*private float rotY = 0f;
        private float rotX = 0f;*/

        public Transform HandsPivot;
        public Transform RightHand;
        public Transform RightElbow;

        public ChartStats Stats;
        //Stat life = new Stat();
        Stat life;
        Stat shield;
        Stat armor;
        StatsViewer aa = new StatsViewer();
        /*public float speed = 0.1f;
        public float rotationSpeed = 25;
        public float jumForce = 25;
        public float minAngle = -70;
        public float maxAngle = 90;
        public float cameraSpeed = 24;*/



        public States states = new States();
        //public float CurrentLife;
        //public GunController GUN;

        private Vector2 newSpeed;
        private Vector2 moveAnim;
        private bool initialized = false;
        public InputController _input;
        // Start is called before the first frame update
        void Initialize()
        {
            tr = this.transform;
            rg = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            ik = GetComponentInChildren<IkHandler>();
            inventary = GetComponent<InvetaryController>();
            ragdoll = GetComponentInChildren<RagdollController>();
            //ik.LookAtPosition = LookAt;
            ik.RightHandPosition = RightHand;
            ik.RightElbowPosition = RightElbow;
            //cam = Camera.main.transform;

            inventary.Initialize(Player);

            
            //life.Init("life", this.Stats.MaxLife);
            life = new Stat("life", this.Stats.MaxLife, this.Stats.MaxLife);
            shield = new Stat("shield", this.Stats.MaxLife, 0);
            armor = new Stat("armor", this.Stats.MaxLife, 0);
            
            if (Player )
            {
                
                StatsViewer.Viewer.Add(life);
                StatsViewer.Viewer.Add(shield);
                StatsViewer.Viewer.Add(armor);
            }
            
            
            //CurrentLife = Stats.MaxLife;
            Active = true;
            initialized = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!initialized)
            {
                Initialize();
            }
            if (Player)
            {
                PlayerControl();
            }
            if (!Active)
            {
                return;
            }
            
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
            states.Aiming = CrossPlatformInputManager.GetButton("Aim");
            states.Shooting = CrossPlatformInputManager.GetButtonDown("Shoot");
            states.Interacting = CrossPlatformInputManager.GetButtonDown("Hand");
            states.Consuming = CrossPlatformInputManager.GetButtonDown("Consume");
            states.Kill = CrossPlatformInputManager.GetButtonDown("Kill");
            states.Throwing = (CrossPlatformInputManager.GetButtonDown("Throwing")? !states.Throwing : states.Throwing);

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

            //this move the soldier
            /*Vector3 movement = new Vector3(x, 0, y);
            rg.velocity = movement * Stats.speed;/**/
            
            if (x!=0 && y!=0)
             {
                 tr.eulerAngles = new Vector3(tr.eulerAngles.x,Mathf.Atan2(x,y)*Mathf.Rad2Deg, tr.eulerAngles.z);
             }

            if (CrossPlatformInputManager.GetButtonDown("Kill"))
            {
                TakeDamage(20);
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
            /*Collider nearest = null;
            ItemController item = GetComponent<ItemController>();
            if (item == null)
            {
                //inventary.ItemViewer.DrawItemViewer(item.mTransform,camera);
                if (states.Interacting)
                {
                    inventary.AddItem(item);
                }
            }/**/
            //Vector3 pos = new Vector3(0.01f, 0.01f, 0.01);
            Collider[] checking = Physics.OverlapSphere(LookAt.position, 0.009f, LayerMask.GetMask("Item"));
            if (checking.Length > 0)
            {
                
                float near = 0.009f;
                Collider nearest = null;
                foreach (Collider c in checking)
                {
                    Vector3 collisionpos = c.ClosestPoint(LookAt.position);
                    float distance = Vector3.Distance(collisionpos, LookAt.position);
                    if (distance < near)
                    {
                        
                        nearest = c;
                        near = distance;
                    }
                }
                if (nearest != null)
                {
                    ItemController item = nearest.GetComponent<ItemController>();
                    if (item != null)
                    {
                        if (Player)
                        {
                            //inventary.ItemViewer.DrawItemViewer(item.Stats, item.mTransform, LookAt);
                        }


                        if (states.Interacting)
                        {
                            
                            inventary.AddItem(item);
                        }
                    }
                }
            }
            else
            {
                //inventary.ItemViewer.HideViewer();
            }/**/

            if (!states.Throwing)
            {
                this.GunControl();
            }
            else
            {
                this.ThrowControl();
            }

        }
        public void GunControl()
        {
            ItemController selectedWeapon = inventary.GetSelectedAt("PrimaryWeapon");
            if (selectedWeapon != null)
            {
                selectedWeapon.Show();
                if (selectedWeapon is GunController)
                {
                    GunController GUN = selectedWeapon as GunController;
                    ik.LeftHandPosition = GUN.leftHandPosition;
                    ik.LeftElbowPosition = GUN.leftElbowPosition;
                    if (states.Shooting)
                    {
                        GUN.attack();
                    }
                    ik.UpdateRecoil(GUN.MaxRecoil, -moveAnim.x, GUN.ShootingModifier);
                }
            }
            if (states.Consuming)
            {
                ItemController selectedMed = inventary.GetSelectedAt("Meds");
                if (selectedMed != null)
                {
                    if (selectedMed is ConsumableItem)
                    {

                        ConsumableItem consumable = selectedMed as ConsumableItem;
                        consumable.Use(this);
                    }
                }
            }
            /*if (GUN !=null)// can shooter
            {
                ik.LeftHandPosition = GUN.leftHandPosition;
                ik.LeftElbowPosition = GUN.leftElbowPosition;
                if (states.Shooting)
                {
                    GUN.Shoot();
                }
                ik.UpdateRecoil(GUN.MaxRecoil, -moveAnim.x, GUN.ShootingModifier);
            }*/
        }
        public void ThrowControl()
        {
            ItemController selectedweapon = inventary.GetSelectedAt("PrimaryWeapon");
            if (selectedweapon != null)
            {
                selectedweapon.Hide();
            }
            ItemController selectedThrowable = inventary.GetSelectedAt("Throwable");
            this.states.Armed = true;
            
            if (selectedThrowable != null)
            {
                if (selectedThrowable is ThrowableItem)
                {
                    if (states.Shooting)
                    {
                        selectedThrowable.Use(this);
                    }
                }
            }
        }
        public void TakeDamage(float damage)
        {
            life.Value -= damage;
            if (life.Value <= 0)
            {
                ragdoll.Active(true);
                this.Active = false;
            }
        }
        internal void Consume(float effectStrength, string effectStat)
        {
            if (effectStat == "life")
            {
                if ((life.Value + effectStrength) <= life.Max)
                {
                    life.Value += effectStrength;
                }
                else
                {
                    life.Value = life.Max;
                }
            }
            else if (effectStat == "shield")
            {
                if ((shield.Value + effectStrength) <= shield.Max)
                {
                    shield.Value += effectStrength;
                }
            }
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
            //HandsPivot.position = anim.GetBoneTransform(HumanBodyBones.RightShoulder).position;
            //Quaternion localRotation = Quaternion.Euler(-rotY, HandsPivot.localRotation.y, HandsPivot.localRotation.z);
            //HandsPivot.localRotation = localRotation;
            anim.SetFloat("X", newSpeed.x);
            anim.SetFloat("Y", newSpeed.y);

            ik.Aiming = this.states.Aiming;
            ik.Shooting = this.states.Shooting;

            ik.DisableLeftHand = this.states.Throwing;
            ik.DisableRightHand = this.states.Throwing;
        }

        

       
    }

    public class States
    {
        public bool OnGround = false;
        public bool Jumping = false;
        public bool Aiming = false;
        public bool Shooting = false;
        public bool Interacting = false;
        public bool Consuming = false;
        public bool Kill = false;
        public bool Throwing = false;
        public bool Armed = false;


    }
}
