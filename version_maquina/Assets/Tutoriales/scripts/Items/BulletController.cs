using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class BulletController : MonoBehaviour
    {
        private float lifetime = 50f;
        private float damage;
        float deltatime = 0f;

        Rigidbody rg;

        public void Initilized(float power, float damage, float lifetime)
        {
            rg = GetComponent<Rigidbody>();
            rg.velocity = this.transform.forward * power;

            this.damage = damage;
            this.lifetime = lifetime;
        }

        private void FixedUpdate()
        {
            deltatime += Time.deltaTime;
            if (deltatime >= lifetime)
                Destroy(this.gameObject);
        }
    }
}
