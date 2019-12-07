using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace tutoriales
{
    public class GroundController : MonoBehaviour
    {
        Rigidbody rg;

        public float speed = 10f;

        private Vector2 newSpeed;
        // Start is called before the first frame update
        void Start()
        {
            rg = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            GroundControl();
        }

        private void GroundControl()
        {
            float x = CrossPlatformInputManager.GetAxis("Horizontal");
            float y = CrossPlatformInputManager.GetAxis("Vertical");

            newSpeed = new Vector2(x, y);

            Vector3 movement = new Vector3(-x, 0, -y);
            rg.velocity = movement * speed;
        }
    }

}
