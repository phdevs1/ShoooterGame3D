using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    [CreateAssetMenu(fileName = "gunStats", menuName = "tutoriales/gun stats")]
    public class GunStats : WeaponStats
    {
        public float power = 100f;
        public float lifetime = 10f;

        public float MaxRecoil = 1f;
        public float ShootingModifier = 2f;

        public BulletController bulletPrefab;
    }
}