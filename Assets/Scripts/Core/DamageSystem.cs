using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ForbiddenReef {
    public class DamageSystem : MonoBehaviour {
        public GameObject player;
        public int maxHealth = 100;
        public int currentHealth;
        float distance;
        public HealthBar healthBar;

        // Start is called before the first frame update
        void Start () {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth (maxHealth);
        }
        //s
        void OnMouseOver () {

            distance = Vector3.Distance (player.transform.position, this.transform.position);
            if (distance <= 5.5f && Input.GetMouseButtonDown (0)) {
                Debug.Log ("attacking");
                // TakeDamage (20);

            }
        }
        // Update is called once per frame
        void Update () {

        }

        // void TakeDamage (int damage) {
        //     currentHealth -= damage;

        //     healthBar.SetHealth (currentHealth);
        // }
    }
}