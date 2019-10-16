using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class GunController : MonoBehaviour
    {
        public Transform shootPoint;
        public Transform leftHandPosition;
        public Transform leftElbowPosition;

        public Transform bulletPrefab;

        public float MaxRecoil = 1f;
        public float ShootingModifier = 2f;

        public void Shoot()
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation); //create bullet

        }
    }

}
