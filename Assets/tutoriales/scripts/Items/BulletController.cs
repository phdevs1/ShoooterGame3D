using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class BulletController : MonoBehaviour
    {
        //public float power = 10f;
        protected float lifeTime = 10f;
        protected float damage;
        protected float deltatime = 0f;

        protected Rigidbody rg;
        protected Transform mtransform;
        protected Vector3 lastPosition;
        protected LayerMask hitboxMask;

        public void Initialize(float power, float damage, float lifeTime)
        {
            rg = GetComponent<Rigidbody>();
            mtransform = GetComponent<Transform>();
            rg.velocity = this.transform.forward * power;
            this.damage = damage;
            this.lifeTime = lifeTime;
            hitboxMask = LayerMask.NameToLayer("hitbox");
            lastPosition = mtransform.position;
        }
       

        // Update is called once per frame
        private void FixedUpdate()
        {
            deltatime += Time.deltaTime;
            detectCollision();
            if (deltatime >= lifeTime)
            {
                Destroy(this.gameObject);
            }
        }
        protected virtual void detectCollision()
        {
            
            Vector3 newpos = mtransform.position;
            Vector3 dir = newpos - lastPosition;
            RaycastHit hit;
            //Debug.Log(!Physics.Raycast(newpos, dir.normalized, out hit, dir.magnitude));
            if (Physics.Raycast(lastPosition, dir.normalized, out hit, dir.magnitude))
            {
                
                GameObject go = hit.collider.gameObject;
                if (go.layer == hitboxMask)
                {
                    BodyPart bp = go.GetComponent<BodyPart>();
                    if (bp != null)
                    {
                        bp.TakeHit(damage);
                        Debug.Log("impacto en " + bp.BodyName);
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
            lastPosition = newpos;
        }
    }

}
