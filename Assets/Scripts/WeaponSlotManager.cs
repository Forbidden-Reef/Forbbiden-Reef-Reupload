using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForbiddenReef {
    public class WeaponSlotManager : MonoBehaviour {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;
        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;
        bool singleWeild = false;
        bool unarmed = true;
        bool dualWeild = false;

        private void Awake () {

            //find and assign the character weapon slots
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot> ();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots) {
                if (weaponSlot.isLeftHandSlot) {
                    leftHandSlot = weaponSlot;
                } else if (weaponSlot.isRightHandSlot) {
                    rightHandSlot = weaponSlot;
                }
            }
        }
        public void LoadWeaponOnSlot (Weapon weapon) {
            if (unarmed) {
                rightHandSlot.LoadWeaponModel (weapon);
                LoadRightWeaponDamageCollider ();

                singleWeild = true;

            }
            if (singleWeild && !unarmed) {

                leftHandSlot.LoadWeaponModel (weapon);
                LoadLeftWeaponDamageCollider ();
                singleWeild = false;

            }
            dualWeild = true;
            unarmed = false;
        }

        #region  Handle Weapon's Damage Collider

        private void LoadLeftWeaponDamageCollider () {
            leftHandDamageCollider = leftHandSlot.CurrentWeaponModel.GetComponentInChildren<DamageCollider> ();
        }

        private void LoadRightWeaponDamageCollider () {
            rightHandDamageCollider = rightHandSlot.CurrentWeaponModel.GetComponentInChildren<DamageCollider> ();
        }

        public void OpenLeftDmageCollider () {
            if (dualWeild) {
                leftHandDamageCollider.EnableDamageCollider ();
            }
        }
        public void OpenRightDmageCollider () {
            if (singleWeild || dualWeild) {
                rightHandDamageCollider.EnableDamageCollider ();
            }

        }

        public void CloseleftHandDamageCollider () {
            if (dualWeild) {
                leftHandDamageCollider.DisableDamageCollider ();
            }
        }

        public void CloseRightHandDamageCollider () {
            if (singleWeild || dualWeild) {
                rightHandDamageCollider.DisableDamageCollider ();
            }
        }
        #endregion
    }
}