using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class BulletController : MonoBehaviour
    {
        public float power = 10f;
        public float lifeTime = 10f;
        float deltatime = 0f;

        Rigidbody rg;
        // Start is called before the first frame update
        private void Start()
        {
            rg = GetComponent<Rigidbody>();
            rg.velocity = this.transform.forward * power;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            deltatime += Time.deltaTime;
            if (deltatime >= lifeTime)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
