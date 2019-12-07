using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class ItemController : MonoBehaviour
    {
        public ItemStats Stats;

        private Transform mTransform;
        private Rigidbody rg;
        private SphereCollider collider;

        private void Start()
        {
            mTransform = this.transform;
            rg = GetComponent<Rigidbody>();
            collider = GetComponent<SphereCollider>();
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        public void Take(Transform slotPosition)
        {
            mTransform.parent = slotPosition;
            mTransform.localPosition = Vector3.zero;
            rg.detectCollisions = false;
            rg.isKinematic = true;
        }

        public void Drop()
        {
            mTransform.parent = null;
            rg.detectCollisions = true;
            rg.isKinematic = false;

            RaycastHit hit;
            if (Physics.Raycast(mTransform.position, Vector3.down, out hit))
            {
                mTransform.position = hit.point;
            }
        }
    }
}
