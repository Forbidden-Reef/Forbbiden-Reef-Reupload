using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForbiddenReef {
    public class DamageCollider : MonoBehaviour {
        public Collider damageCollider;
        public bool enemy;

        public int damage = 25;

        private void Awake () {
            if (!enemy) {
                damageCollider = GetComponent<Collider> ();
                damageCollider.gameObject.SetActive (true);
                damageCollider.isTrigger = true;
                //allows the collider to be enabled/disabled ,but the item its self is not affected
                damageCollider.enabled = true;
            }

        }

        public void EnableDamageCollider () {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider () {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter (Collider collision) {

            if (collision.tag == "Enemy" && damageCollider.tag != "Enemy Weapon") {
                Debug.Log ("trigger");
                EnemyStats enemyStats = collision.GetComponent<EnemyStats> ();

                if (enemyStats != null) {

                    enemyStats.TakeDamage (damage);
                }
            }

            if (collision.tag == "Player" && damageCollider.tag != "PlayerWeapon") {
                // Debug.Log (collision);
                PlayerStats playerStats = collision.GetComponent<PlayerStats> ();

                if (playerStats != null) {
                    playerStats.TakeDamage (damage);
                }
            }

        }
    }
}