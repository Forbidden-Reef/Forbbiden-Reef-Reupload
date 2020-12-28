using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ForbiddenReef {
    public class Equip : MonoBehaviour {
        public bool equipped;
        public Camera mainCam;
        public bool slotFull;
        public Transform player;
        public Weapon weapon;
        WeaponSlotManager weaponSlotManager;

        public WeaponHolderSlot holderSlot;
        public GameObject item;
        public GameObject tempParent;
        // Start is called before the first frame update
        void Start () {
            weaponSlotManager = tempParent.GetComponent<WeaponSlotManager> ();

        }

        // Update is called once per frame
        void Update () {

            Vector3 distanceToPlayer = player.position - transform.position;
            if (Input.GetKeyDown (KeyCode.E) && distanceToPlayer.magnitude <= 2f) {
                PickUp ();

            }
            if (equipped && Input.GetKeyDown (KeyCode.Q)) Drop ();

        }

        private void PickUp () {
            equipped = true;
            slotFull = true;

            //Make weapon a child of the camera and move it to default position
            weaponSlotManager.LoadWeaponOnSlot (weapon);
            Destroy (this.gameObject);

        }

        private void Drop () {
            equipped = false;
            slotFull = false;

        }

    }
}