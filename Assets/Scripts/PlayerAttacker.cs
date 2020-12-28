using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForbiddenReef {
    public class PlayerAttacker : MonoBehaviour {
        AnimationStateController animatorHandler;

        private void Awake () {
            animatorHandler = GetComponent<AnimationStateController> ();
        }

        public void HandleLightAttack (Weapon weapon) {
            int rando = Random.Range (0, 2);
            // if (rando == 1) {
            //     animatorHandler.PlayTargetAnimation (weapon.Light_attack_1, true);
            // } else {
            animatorHandler.PlayTargetAnimation (weapon.Light_attack_2, true);
            // }

        }

        public void HandleHeavyAttack (Weapon weapon) {
            animatorHandler.PlayTargetAnimation (weapon.Heavy_attack_1, true);
        }

    }
}