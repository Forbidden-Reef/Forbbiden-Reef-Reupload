using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ForbiddenReef {
    public class PlayerStats : MonoBehaviour {

        public Scene scene;
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        public HealthBar healthBar;
        AnimationStateController animatorHandler;
        private void Awake () {
            animatorHandler = GetComponent<AnimationStateController> ();
            scene = SceneManager.GetActiveScene ();

        }
        void Start () {
            maxHealth = SetMaxHealthFromHealthLevel ();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth (maxHealth);

        }

        //calculate the amount of health based on health level
        private int SetMaxHealthFromHealthLevel () {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage (int damage) {
            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth (currentHealth);

            int rando = Random.Range (0, 2);

            if (rando == 1) {
                animatorHandler.PlayTargetAnimation ("Damage", true);
            } else {
                animatorHandler.PlayTargetAnimation ("Damage2", true);
            }
            if (currentHealth <= 0) {
                currentHealth = 0;

                if (rando == 1) {
                    animatorHandler.PlayTargetAnimation ("Death1", true);
                } else {
                    animatorHandler.PlayTargetAnimation ("Death2", true);
                }
                Destroy (this.gameObject.GetComponent<CharacterController> ());
                Camera.main.transform.localPosition = new Vector3 (0f, 2f, -0.90f);
                Camera.main.transform.localRotation = new Quaternion (90f, 0f, 0f, 0);
                Invoke ("restart", 5);
            }

        }

        private void restart () {

            SceneManager.LoadScene ("StewartAnimationsTest");
        }

        private void destroy () {

            Destroy (this.gameObject);
            Invoke ("restart", 1);
        }
    }
}