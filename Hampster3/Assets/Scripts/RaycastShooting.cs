using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class RaycastShooting : MonoBehaviour
{
    public int maxAmmo = 30;
    public int currentAmmo;
    public StarterAssetsInputs _input;
    [SerializeField]
    private GunModifier gunModifier;
    public TextMeshProUGUI ammoText;
    // Start is called before the first frame update
    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        if (_input.shoot && currentAmmo > 0)
        {
            gunModifier.Shoot();
            currentAmmo--;
            UpdateAmmoText();
        }
        if (_input.reload)
        {
            Reload();
            _input.reload = false;
        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            string tag = hit.collider.gameObject.tag;
            if (tag == "Tagged")
            {
                Debug.DrawRay(transform.position, transform.forward, Color.green);
                Debug.Log("Hit object with tag: " + tag);
            }
        }
        void Reload()
        {
            gunModifier.Reload();
            currentAmmo = maxAmmo;
            UpdateAmmoText();
        }

        void UpdateAmmoText()
        {
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
        }
    }
}
