using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForbiddenReef {
    public class AnimationStateController : MonoBehaviour {

        Animator animator;
        PlayerMovementScript playerMovementScript;
        float velocityZ = 0.0f;
        float velocityX = 0.0f;
        public float acceleration = 2.0f;

        public float deceleration = 2.0f;

        public float maxWalkVelocity = .5f;

        public float maxRunVelocity = 2.0f;

        int VelocityZHash;

        int VelocityXHash;

        void Start () {
            playerMovementScript = GetComponent<PlayerMovementScript> ();
            animator = GetComponent<Animator> ();
            VelocityZHash = Animator.StringToHash ("Velocity Z");
            VelocityXHash = Animator.StringToHash ("Velocity X");

        }

        // Update is called once per frame
        void Update () {

            //Get input from players.

            bool forwardPressed = Input.GetKey (KeyCode.W);
            bool backwardPressed = Input.GetKey (KeyCode.S);
            bool leftPressed = Input.GetKey (KeyCode.A);
            bool rightPressed = Input.GetKey (KeyCode.D);
            bool runPressed = Input.GetKey (KeyCode.LeftShift);

            bool inHand = GameObject.Find ("dagger");

            //set current max velocity
            float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

            //handle velocity changes
            changeVelocity (forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
            lockOrResetVelocity (forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

            //set parameter to our local variables
            animator.SetFloat (VelocityZHash, velocityZ);
            animator.SetFloat (VelocityXHash, velocityX);

        }

        //Handles acceleration and deceleration
        void changeVelocity (bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {
            // increase velocityZ when pressing W
            if (forwardPressed && velocityZ < currentMaxVelocity) {
                velocityZ += Time.deltaTime * acceleration;
            }
            // increase velocityZ when pressing S
            if (backwardPressed && velocityZ > -currentMaxVelocity) {
                velocityZ -= Time.deltaTime * acceleration;
            }

            // increase velocityX when pressing D
            if (rightPressed && velocityX < currentMaxVelocity) {
                velocityX += Time.deltaTime * acceleration;
            }
            // increase velocityX when pressing A
            if (leftPressed && velocityX > -currentMaxVelocity) {
                velocityX -= Time.deltaTime * acceleration;
            }

            // decrease velocityZ when not pressing W
            if (!forwardPressed && velocityZ > 0.0f) {
                // Decrease the animation more when previously sprinting so that it mataches character movement
                if (!(velocityZ > 0.5f)) {
                    velocityZ -= Time.deltaTime * deceleration;
                } else {

                    velocityZ -= Time.deltaTime * (deceleration + 3f);
                }

            }
            // decrease velocityS when not pressing S
            if (!backwardPressed && velocityZ < 0.0f) {
                // decrease velocity when not pressing S

                if (!(velocityZ < -0.5f)) {
                    velocityZ += Time.deltaTime * deceleration;
                } else {
                    velocityZ += Time.deltaTime * (deceleration + 3f);
                }
            }

            // decrease velocityX when not pressing A
            if (!leftPressed && velocityX < 0.0f) {
                if (!(velocityX < -0.5f)) {
                    velocityX += Time.deltaTime * deceleration;
                } else {
                    velocityX += Time.deltaTime * (deceleration + 3f);
                }
            }

            // decrease velocityX when not pressing D
            if (!rightPressed && velocityX > 0.0f) {
                if (!(velocityX > 0.5f)) {
                    velocityX -= Time.deltaTime * deceleration;
                } else {

                    velocityX -= Time.deltaTime * (deceleration + 3f);
                }
            }
        }

        void lockOrResetVelocity (bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {
            //reset forward movment 
            if (!forwardPressed && !backwardPressed && velocityZ != 0.0f && (velocityZ > -.05f && velocityZ < .05f)) {
                velocityZ = 0.0f;
            }

            //reset lateral movment
            if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -.05f && velocityX < .05f)) {
                velocityX = 0.0f;
            }

            //lock to run or sprint depending on current velocity in z forwards direction
            if (forwardPressed && runPressed && velocityZ > currentMaxVelocity) {
                velocityZ = currentMaxVelocity;
            } else if (forwardPressed && velocityZ > currentMaxVelocity) {
                velocityZ -= Time.deltaTime * deceleration;
                if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f)) {
                    velocityZ = currentMaxVelocity;
                }

            } else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f)) {
                velocityZ = currentMaxVelocity;
            }

            //lock to run or sprint depending on current velocity in z backwards direction
            if (backwardPressed && runPressed && velocityZ < -currentMaxVelocity) {
                velocityZ = -currentMaxVelocity;
            } else if (backwardPressed && velocityZ < -currentMaxVelocity) {
                velocityZ += Time.deltaTime * deceleration;
                if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f)) {
                    velocityZ = -currentMaxVelocity;
                }

            } else if (backwardPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f)) {
                velocityZ = -currentMaxVelocity;
            }

            //lock to run or sprint depending on current velocity in x left direction
            if (leftPressed && runPressed && velocityX < -currentMaxVelocity) {
                velocityX = -currentMaxVelocity;
            } else if (leftPressed && velocityX < -currentMaxVelocity) {
                velocityX += Time.deltaTime * deceleration;
                if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f)) {
                    velocityX = -currentMaxVelocity;
                }

            } else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f)) {
                velocityX = -currentMaxVelocity;
            }

            //lock to run or sprint depending on current velocity in x right direction

            if (rightPressed && runPressed && velocityX > currentMaxVelocity) {
                velocityX = currentMaxVelocity;
            } else if (rightPressed && velocityX > currentMaxVelocity) {
                velocityX -= Time.deltaTime * deceleration;
                if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f)) {
                    velocityX = currentMaxVelocity;
                }

            } else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f)) {
                velocityX = currentMaxVelocity;
            }

        }

        public void PlayTargetAnimation (string targetAnim, bool isInteracting) {

            animator.applyRootMotion = isInteracting;
            animator.SetBool ("isInteracting", isInteracting);
            animator.CrossFade (targetAnim, .02f);

        }

    }
}