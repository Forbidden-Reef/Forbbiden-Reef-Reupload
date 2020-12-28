using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForbiddenReef {
    public class WeaponHolderSlot : MonoBehaviour {
        public Transform parentOverride;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;

        public GameObject CurrentWeaponModel;

        public void UnloadWeapon () {
            if (CurrentWeaponModel != null) {
                //deactivate the model
                CurrentWeaponModel.SetActive (false);
            }
        }

        public void UnloadWeaponAndDestroy () {
            if (CurrentWeaponModel != null) {
                //deactivate the model
                Destroy (CurrentWeaponModel);

            }
        }

        public void unArmed () {
            if (CurrentWeaponModel != null) {
                //deactivate the model
                Destroy (CurrentWeaponModel);

            }
        }

        public void LoadWeaponModel (Weapon weapon) {
            UnloadWeaponAndDestroy ();
            //if model for the weapon doesnt exist unload it
            if (weapon == null) {
                UnloadWeapon ();
                return;
            }

            //create the weapon model
            GameObject model = Instantiate (weapon.modelPrefab) as GameObject;

            //position it with parent or inital transform
            if (model != null) {
                if (parentOverride != null) {
                    model.transform.parent = parentOverride;
                } else {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            CurrentWeaponModel = model;
        }

    }

}