using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;
    public bool shotGunFiring;

    public BulletController bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    public float timeBetweenSGShots;
    private float shotCounter;

    public float damage = 10f;

    Quaternion rot = Quaternion.Euler(0, 15, 0);
    Quaternion rot2 = Quaternion.Euler(0, 8, 0);
    Quaternion rot3 = Quaternion.Euler(0, -8, 0);
    Quaternion rot4 = Quaternion.Euler(0, -15, 0);

    public Light MLight;
    public Light SLight;

    public Transform firePoint;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                MLight.enabled = true;
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                EventManager em = ServiceLocator.GetService<EventManager>();
                em.TriggerEvent(ConstManager.EVENT_PLAYER_FIRE);
                newBullet.speed = bulletSpeed;


            }
            else
            {
                MLight.enabled = false;
            }

        }
        else
        {
            MLight.enabled = false;
        }

        if (shotGunFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenSGShots;
                SLight.enabled = true;
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation * rot) as BulletController;
                BulletController newBullet2 = Instantiate(bullet, firePoint.position, firePoint.rotation * rot2) as BulletController;
                BulletController newBullet3 = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                BulletController newBullet4 = Instantiate(bullet, firePoint.position, firePoint.rotation * rot3) as BulletController;
                BulletController newBullet5 = Instantiate(bullet, firePoint.position, firePoint.rotation * rot4) as BulletController;

                newBullet.speed = bulletSpeed;
                newBullet2.speed = bulletSpeed;
                newBullet3.speed = bulletSpeed;
                newBullet4.speed = bulletSpeed;
                newBullet5.speed = bulletSpeed;

                EventManager em = ServiceLocator.GetService<EventManager>();
                em.TriggerEvent(ConstManager.EVENT_PLAYER_SGFIRE);
            }
            else
            {
                SLight.enabled = false;
            }

        }
        else
        {
            SLight.enabled = false;
        }
    }
}
