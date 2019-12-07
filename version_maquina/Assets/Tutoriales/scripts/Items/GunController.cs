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
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tutoriales
{
    public class GunController : WeaponController
    {
        Transform crosshair;

        public Transform shootPoint;
        public Transform leftHandPosition;
        public Transform leftElbowPosition;

        public BulletController bulletPrefab;

        private bool showCrosshair = false;
        protected override void Initialize()
        {
            crosshair = GetComponentInChildren<Canvas>().transform; crosshair.gameObject.SetActive(false);
        }

        public GunStats getGunStats()
        {
            if (Stats is GunStats)
                return Stats as GunStats;
            GunStats defect = new GunStats();
            Stats = defect; return defect;
        }

        public override void Attack()
        {
            BulletController bullet = Instantiate<BulletController>(bulletPrefab, shootPoint.position, shootPoint.rotation);
            GunStats gstats = getGunStats();
            bullet.Initilize(gstats.power, gstats.Damage, gstats.lifeTime);
        }
        
        public void ShowCrosshair()
        {
            crosshair.gameObject.SetActive(showCrosshair = !showCrosshair);
        }

        public void DrawCrossHair(Transform camera)
        {
            if (!showCrosshair)
            {
                ShowCrosshair();
            }
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, getGunStats().Range))
            {
                shootPoint.LookAt(hit.point);
            } else
            {
                Vector3 end = camera.position + camera.forward * getGunStats().Range;
                shootPoint.LookAt(end);
            }
        }
    }
}*/
