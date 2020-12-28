using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForbiddenReef {
    public class PlayerInventory : MonoBehaviour {

        WeaponSlotManager weaponSlotManager;
        public Weapon rightWeapon;
        public Weapon leftWeapon;

        private void Update () {

        }
        private void Awake () {
            weaponSlotManager = GetComponent<WeaponSlotManager> ();

        }

        private void Start () {
            // weaponSlotManager.LoadWeaponOnSlot (rightWeapon, false);
            // weaponSlotManager.LoadWeaponOnSlot (leftWeapon, true);
        }
    }
}