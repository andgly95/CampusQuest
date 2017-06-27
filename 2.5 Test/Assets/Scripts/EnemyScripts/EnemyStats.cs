using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	int enemyHealth;
	int enemyLevel;

	// Use this for initialization
	void Start () {
		enemyHealth = 30;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth <= 0) {
			Destroy (gameObject); //  will need a death animation to play
		}
	}

	// Method to apply damage to the enemy. See AttackTriggerBattle Script to see when it gets called.
	void TakeDamage(int damage) {
		enemyHealth -= 10;
		Debug.Log ("Enemy Health: " + enemyHealth); 
	}
}
