using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace ForbiddenReef {
    public class EnemyAi : MonoBehaviour {
        public NavMeshAgent agent;

        public Transform player;
        public LayerMask whatIsGround, whatIsPlayer;
        //Patroling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;
        Animator animator;
        //attacking 
        public float timeBetweenAttacks;
        public bool alreadyAttacked;

        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        private void Awake () {
            animator = GetComponent<Animator> ();
            player = GameObject.Find ("Survivalist").transform;
            agent = GetComponent<NavMeshAgent> ();

        }

        private void Update () {
            // check for sight and attack range

            playerInSightRange = Physics.CheckSphere (transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere (transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling ();
            if (playerInSightRange && !playerInAttackRange) {

                Vector3 distanceToTarget = transform.position - player.position;
                if (distanceToTarget.magnitude >= 1f) {
                    animator.SetBool ("Walk Forward", true);
                    ChasePlayer ();
                }

            };
            if (playerInAttackRange && playerInSightRange) AttackPlayer ();

        }
        private void ChasePlayer () {

            agent.SetDestination (player.position);

        }

        private void Patroling () {

            if (!walkPointSet) SearchWalkPoint ();

            if (walkPointSet) {

                animator.SetBool ("Walk Forward", true);
                agent.SetDestination (walkPoint);
            };

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }

        private void AttackPlayer () {
            //Make sure enemy doesnt move

            agent.SetDestination (transform.position);
            transform.LookAt (player);
            animator.SetBool ("Walk Forward", false);

            if (!alreadyAttacked) {

                //attack code

                animator.SetTrigger ("Stab Attack");

                alreadyAttacked = true;
                Invoke (nameof (ResetAttack), timeBetweenAttacks);
            }

        }
        private void ResetAttack () {
            alreadyAttacked = false;
        }

        private void SearchWalkPoint () {

            float randomZ = Random.Range (-walkPointRange, walkPointRange);
            float randomX = Random.Range (-walkPointRange, walkPointRange);

            walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast (walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;

        }

        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere (transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere (transform.position, sightRange);
        }
    }
}