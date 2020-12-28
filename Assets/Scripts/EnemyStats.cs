using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    Animator animator;

    private void Awake () {
        animator = GetComponent<Animator> ();
    }
    void Start () {
        maxHealth = SetMaxHealthFromHealthLevel ();
        currentHealth = maxHealth;

    }

    //calculate the amount of health based on health level
    private int SetMaxHealthFromHealthLevel () {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage (int damage) {
        Debug.Log ("hurt");
        currentHealth = currentHealth - damage;

        animator.SetTrigger ("Take Damage");

        if (currentHealth <= 0) {
            currentHealth = 0;
            animator.SetTrigger ("Die");

            Invoke ("destroy", 1.30f);
        }
    }

    private void destroy () {
        Destroy (this.gameObject);
    }
}