using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For his neutral special, Cybearg wields a gun!
public class Gun : MonoBehaviour
{
    private float damage = 1f;
    public float range = 100f;
    public float fireRate = 4f;
    public float impactForce = 30f;

    public Camera cam;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;

    public float cooldown = 0f;

    public GameObject GunShot;
    

    //weapon animation 
    [SerializeField] GunAnimatorManager animationController;
    // Update is called once per frame
    void Update()
    {

    }
    //For AI to inteeract with
    public bool TryShoot(float[] actions)
    {
        //Can't mix discrete and continuous actions unless you're a lot smarter than I am, so you have to do this
        if (actions[3]>=0.5 && Time.time >= cooldown)
        {
          
            cooldown = Time.time + 1f / fireRate;
            Shoot();
            return true;
        }
        return false;
    }
    //Shoot gun
    void Shoot()
    {

        GameObject gunShot = Instantiate(GunShot, this.transform.position, this.transform.rotation) as GameObject;

        //weapon animation
        animationController.fireWeapon();

        RaycastHit hit;
        muzzleflash.Play();
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log("hit character");
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            zombie z = hit.collider.gameObject.GetComponent<zombie>();
            if(z != null)
			{
                z.takeDamage(10);
			}

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.3f);
        }
    }
}
