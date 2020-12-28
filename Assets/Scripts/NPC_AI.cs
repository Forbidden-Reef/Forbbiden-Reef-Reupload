using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ForbiddenReef {
    public class NPC_AI : MonoBehaviour {
        public LayerMask whatIsGround, whatIsPlayer;
        public NavMeshAgent agent;

        public Transform player;
        Animator animator;
        //attacking 

        public float sightRange;
        public bool playerInSightRange, PlayerTalks;
        bool sitting, standing = true;
        private void Awake () {
            animator = GetComponent<Animator> ();
            player = GameObject.Find ("Survivalist").transform;
            agent = GetComponent<NavMeshAgent> ();

        }

        private void Update () {
            // check for sight and attack range

            playerInSightRange = Physics.CheckSphere (transform.position, sightRange, whatIsPlayer);

            if (Input.GetKeyDown (KeyCode.E) && sitting && playerInSightRange) {
                animator.SetTrigger ("Startled");
            }

            if (Input.GetKeyDown (KeyCode.E) && standing && playerInSightRange) {
                animator.SetTrigger ("ConvoDone");
            }
        }
        public void transitionStand () {
            animator.SetBool ("Listening", true);
        }
        public void transitionSit () {
            animator.SetBool ("ConvoDone", true);
            animator.SetBool ("Listening", false);
        }

        private void OnDrawGizmosSelected () {

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere (transform.position, sightRange);
        }
    }
}