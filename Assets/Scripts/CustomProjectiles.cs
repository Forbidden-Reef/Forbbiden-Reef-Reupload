using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Thanks for downloading my custom projectiles script! :D
/// Feel free to use it in any project you like!
/// 
/// The code is fully commented but if you still have any questions
/// don't hesitate to write a yt comment
/// or use the #coding-problems channel of my discord server
/// 
/// Dave
namespace ForbiddenReef {
    public class CustomProjectiles : MonoBehaviour { //Assignables
        public Rigidbody rb;
        public GameObject explosion;
        public LayerMask whatIsEnemies;

        public int waterBallDmg = 10;
        //Stats
        [Range (0f, 1f)]
        public float bounciness;
        public bool useGravity;

        //Damage
        public int explosionDamage;
        public float explosionRange;

        //Lifetime
        public int maxCollisions;
        public float maxLifetime;
        public bool explodeOnTouch = true;

        private int collisions;
        private PhysicMaterial physics_mat;

        private void Start () {
            Setup ();
        }

        private void Update () {
            if (collisions >= maxCollisions) Explode ();

            maxLifetime -= Time.deltaTime;
            if (maxLifetime <= 0) Explode ();
        }

        private void Explode () {
            //Instantiate explosion
            if (explosion != null) Instantiate (explosion, transform.position, Quaternion.identity);

            // Debug.Log ("1");

            //Check for enemies
            // Collider[] enemies = Physics.OverlapSphere (transform.position, explosionRange, whatIsEnemies);
            // print (enemies.Length);
            // for (int i = 0; i < enemies.Length; i++) {
            //     //Get component of enemy and call TakeDamage()

            //     //Just an example:
            //     ///enemies[i].GetComponent<ShootingAi>().TakeDamage(explosionDamage);
            // }

            Destroy (gameObject);

        }

        private void OnCollisionEnter (Collision collision) {
            //Don't count collisions with oter bullets
            if (collision.collider.CompareTag ("Bullet")) return;
            print (collision.gameObject.tag);
            //Count up collisions
            collisions++;

            if (collision.collider.tag == "Player") {
                // Debug.Log (collision);
                PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats> ();

                if (playerStats != null) {
                    playerStats.TakeDamage (waterBallDmg);
                }
            }

            //Explode if bullet hits an enemy directly and explodeOnTouch is activated
            if (collision.collider.CompareTag ("Player") && explodeOnTouch) Explode ();
        }

        private void Setup () {
            //Make a new Physics material
            physics_mat = new PhysicMaterial ();
            physics_mat.bounciness = bounciness;
            physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
            physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
            //Assign material to collider
            GetComponent<SphereCollider> ().material = physics_mat;

            //Set gravity
            rb.useGravity = useGravity;
        }

        /// Just to visualize the explosion range
        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere (transform.position, explosionRange);
        }
    }
}