using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForbiddenReef {
    public class PlayerMovementScript : MonoBehaviour {
        public CharacterController controller;

        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        public float speed = 5f;

        float maxSpeed = 10f;
        float minSpeed = 5f;
        float acceleration = 2.0f;

        float deceleration = 2.0f;

        public float gravity = -9.81f;
        public float jumpHeight = 3f;
        public Transform groundCheck;
        //radius of sphere
        public float groundDistance = 0.4f;

        // mask that controller what object we are checking for
        public LayerMask groundMask;

        public bool isGrounded;
        Animator animator;
        Vector3 velocity;
        bool lightAttack;
        bool equip = false;
        bool heavyAttack;

        public bool stationary = false;

        private void Awake () {
            animator = GetComponent<Animator> ();
            playerAttacker = GetComponent<PlayerAttacker> ();
            playerInventory = GetComponent<PlayerInventory> ();
        }

        // Update is called once per frame
        void Update () {
            lightAttack = Input.GetMouseButtonDown (0);
            heavyAttack = Input.GetMouseButtonDown (1);
            if (Input.GetMouseButtonDown (2)) {
                equip = !equip;
            }

            isGrounded = Physics.CheckSphere (groundCheck.position, groundDistance, groundMask);

            if (isGrounded & velocity.y < 0) {
                velocity.y = -2f;
            }
            float x = Input.GetAxis ("Horizontal");
            float z = Input.GetAxis ("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            if (isGrounded && Input.GetKey ("left shift") && speed < maxSpeed) {
                speed += Time.deltaTime * acceleration;
                Debug.Log ("sprinting");
            } else if (!Input.GetKey ("left shift") && speed > minSpeed) {
                speed -= Time.deltaTime * deceleration;
                Debug.Log ("walking");
            }

            AttackInput ();
            if (stationary) {

                x = 0;
                z = 0;
                move = transform.right * 0 + transform.forward * z;
                controller.Move (move * speed * Time.deltaTime);
            } else {

                controller.Move (move * speed * Time.deltaTime);
            }

            if (Input.GetButtonDown ("Jump") && isGrounded) {

                animator.SetBool ("isJumping", true);
                velocity.y = Mathf.Sqrt (jumpHeight * -2f * gravity);

            } else if (!isGrounded) {
                animator.SetBool ("isJumping", false);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move (velocity * Time.deltaTime);

        }

        private void AttackInput () {

            if (lightAttack) {

                playerAttacker.HandleLightAttack (playerInventory.rightWeapon);

            }
            if (heavyAttack) {

                stationary = true;

                playerAttacker.HandleHeavyAttack (playerInventory.rightWeapon);
                Invoke ("Stationary", 2.433f);
            }

        }

        private void Stationary () {
            stationary = false;

        }
    }
}