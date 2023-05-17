using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModifier : MonoBehaviour
{
    public float shootDelay = 0.2f; // the time delay between shots
    public float reloadTime = 1f; // the time it takes to reload the gun
    public Animator gunAnimator; // reference to the Animator component on the gun
    private float shootTimer = 0f; // the time remaining until the next shot can be fired
    private float reloadTimer = 0f; // the time remaining until the gun is fully reloaded
    private RaycastShooting gunScript; // reference to the RaycastShooting script on the gun

    // Start is called before the first frame update
    void Start()
    {
        gunScript = GetComponent<RaycastShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
        }

        if (reloadTimer > 0f)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0f)
            {
                gunScript.currentAmmo = gunScript.maxAmmo;
                gunAnimator.SetBool("Reload", false);
            }
        }
    }

    public void Shoot()
    {
        if (gunScript.currentAmmo >= 1 && shootTimer <= 0f && reloadTimer <= 0f)
        {
            gunScript.currentAmmo--;
            gunScript._input.shoot = false;

            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);

                }
            }

            shootTimer = shootDelay;
        }
    }
    public void Reload()
    {
        if (reloadTimer <= 0f)
        {
            gunAnimator.SetBool("Reload", true);
            reloadTimer = reloadTime;
        }
    }
}
