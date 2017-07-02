using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	//public static EnemyStats enemyStats;

	public int enemyHealth= 30;
	public int maxEnemyHealth= 30;

	// Use this for initialization
	void Start () {
		enemyHealth = 30;
		maxEnemyHealth = 30;
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
	}
}
