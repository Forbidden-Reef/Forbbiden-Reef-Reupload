using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ForbiddenReef {

    public class DamagePlayer : MonoBehaviour {
        bool alreadyAttacked;
        private int damage = 1;

        private void Update () {

        }
        private void OnTriggerEnter (Collider other) {

            EnemyAi enemyAi = GetComponent<EnemyAi> ();
            alreadyAttacked = enemyAi.alreadyAttacked;
            Debug.Log (other);
            PlayerStats playerStats = other.GetComponent<PlayerStats> ();
            if (playerStats != null && alreadyAttacked) {
                playerStats.TakeDamage (damage);
            }
        }

    }
}