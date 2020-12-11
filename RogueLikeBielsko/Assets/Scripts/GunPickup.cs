using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public float waitToBeCollected = .5f;
    public Gun theGun;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeCollected <= 0)
        {

            bool hasGun = false;
            foreach(Gun gunToCheck in PlayerControler.instance.availableGuns)
            {
                if(theGun.weaponName == gunToCheck.weaponName)
                {
                    hasGun = true;
                }
            }

            if (!hasGun)
            {
               Gun gunClone = Instantiate(theGun);
                gunClone.transform.parent = PlayerControler.instance.gunArm;
                gunClone.transform.position = PlayerControler.instance.gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                PlayerControler.instance.availableGuns.Add(gunClone);
                PlayerControler.instance.currentGun = PlayerControler.instance.availableGuns.Count-1;

                PlayerControler.instance.SwitchGun();




            }

            AudioManager.instance.PlaySFX(7);
            Destroy(gameObject);
        }
    }
}
