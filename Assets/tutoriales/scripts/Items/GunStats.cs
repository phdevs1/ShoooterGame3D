using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace tutoriales
{
    [CreateAssetMenu(fileName ="gunStats", menuName ="tutoriales/gun stats")] ///¿? comentar
    public class GunStats : WeaponStats
    {
        //public Transform bulletPrefab;

        public BulletController bulletPrefab;
        public float MaxRecoil = 1f;
        public float ShootingModifier = 2f;
        public float power = 100f;
        public float lifeTime = 10f;
    }
}

