using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class GunController : WeaponController
    {
        public Transform shootPoint;
        public Transform leftHandPosition;
        public Transform leftElbowPosition;


        public BulletController bulletPrefab;
        //public Transform bulletPrefab;
        public float MaxRecoil = 1f;
        public float ShootingModifier = 2f;

        public AudioClip shotNoise;
        private AudioSource shotSource;
        private ParticleSystem muzzle;

        protected override void Initialize()
        {
            muzzle = shootPoint.GetComponentInChildren<ParticleSystem>();
            muzzle.gameObject.SetActive(false);
            muzzle.Stop();
            shotSource = shootPoint.GetComponent<AudioSource>();
        }

        public GunStats getGunStats()
        {
            if (Stats is GunStats)
                return Stats as GunStats;
            GunStats defect = new GunStats();
            Stats = defect;
            return defect;
        }
        public override void attack()
        {
            BulletController bullet =  Instantiate<BulletController>(bulletPrefab, shootPoint.position, shootPoint.rotation); //create bullet
            GunStats gstats = getGunStats();
            bullet.Initialize(gstats.power, gstats.Damage, gstats.lifeTime);

            shotSource.PlayOneShot(shotNoise, 1f);
            muzzle.gameObject.SetActive(true);
            muzzle.Play(true);
        }

        
    }

}
