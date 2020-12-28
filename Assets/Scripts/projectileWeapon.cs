using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class projectileWeapon : MonoBehaviour {
    public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;

    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;
    public bool isCharged;
    public Camera fpsCam;

    public Transform attackPoint;

    public bool allowInvoke = true;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    private void Update () {
        MyInput ();
        //setup ammo display if it exists
        if (ammunitionDisplay != null) ammunitionDisplay.SetText (bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);

    }

    private void Awake () {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void MyInput () {
        //check if allowed to hold down fire button and the ntake the input
        if (allowButtonHold) shooting = Input.GetKey (KeyCode.Mouse0);
        else shooting = Input.GetKeyDown (KeyCode.Mouse0);

        //Reload functionality
        if (Input.GetKeyDown (KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload ();

        //Auto reload on empty
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload ();

        //shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = 0;
            if (isCharged) {
                Invoke ("Shoot", 1.033f);
            } else { Shoot (); }
        }

    }

    private void Shoot () {
        readyToShoot = false;

        //find middle of screen
        Ray ray = fpsCam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray cast hits something and save that targetPoint
        Vector3 targetPoint;
        if (Physics.Raycast (ray, out hit))
            targetPoint = hit.point;
        else
            //if you dont hit anything there isn't and object there, so you need to just set a arbitrary distance away from the player
            targetPoint = ray.GetPoint (75);

        //calculate direction from attack point to target point
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //optional spread randomized between two values
        float x = Random.Range (-spread, spread);
        float y = Random.Range (-spread, spread);

        // calculate spread direction  by adding the new x/y values to the old direction
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3 (x, y, 0);

        //Spawn the bullet at the attackPoint .
        GameObject currentBullet = Instantiate (bullet, attackPoint.position, Quaternion.identity);

        //make sure the projectile faces the correct way
        // currentBullet.transform.forward = directionWithoutSpread.normalized;

        //give the bullet force
        currentBullet.GetComponent<Rigidbody> ().AddForce (directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);

        //this is for upward force in the case that it is a bouncing projectile
        currentBullet.GetComponent<Rigidbody> ().AddForce (fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        if (muzzleFlash != null)
            Instantiate (muzzleFlash, attackPoint.position, Quaternion.identity);

        //decremeant number of bullets
        bulletsLeft--;
        //incremeant number of bullets
        bulletsShot++;

        //after every shot have a time out
        if (allowInvoke) {
            //this line make sure that you can only invoke the method once as opposed to it be invoked on a set timer
            Invoke ("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        // // allow multiple bullets to be fired for example with a shot gun
        // if (bulletsShot < bulletsPerTap && bulletsLeft > 0) {
        //     Invoke ("Shoot", timeBetweenShots);
        // }

    }

    public void ResetShot () {
        readyToShoot = true;
        allowInvoke = true;

    }
    private void Reload () {
        reloading = true;
        Invoke ("ReloadFinished", reloadTime);
    }

    private void ReloadFinished () {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}