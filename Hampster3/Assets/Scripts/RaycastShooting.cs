using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooting : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(Camera.main.transform.position,
                Camera.main.transform.forward);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    //DO DAMAGE
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
                }
            }
        }

    }
}
