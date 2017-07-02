using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

	private GameObject enemyDisplayed;
	private GameObject enemyLockedOn;
	EnemyStats enemyDisplayedStats;

	private float enemyHealth;
	private float maxEnemyHealth;
	private float enemyHealthRatio;

	RectTransform _EnemyRectTransform = null;
	private Image backBar = null;
	private Image frontBar = null;

	private bool lockedOn;

	// Use this for initialization
	void Start () {
		_EnemyRectTransform = gameObject.transform.root.GetChild (6).GetChild (0).GetComponent<RectTransform>();
		backBar = gameObject.transform.root.GetChild(6).GetComponent<Image>();
		frontBar = gameObject.transform.root.GetChild(6).GetChild(0).GetComponent<Image>();

		backBar.enabled = false;
		frontBar.enabled = false;
		lockedOn = false;

		if (AttackTrigger.battle) {
			enemyDisplayed = PlayerView.facingThisEnemy;
			enemyDisplayedStats = enemyDisplayed.GetComponent<EnemyStats> ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (AttackTrigger.battle) {

			lockOn (); // give the player an opportunity to lock on

			if (lockedOn == true) {//if we are locked on, we do not allow for mutability of which enemy gets the health bar
				backBar.enabled = true;
				frontBar.enabled = true;

				enemyHealth = enemyDisplayedStats.enemyHealth; // enemyDisplayedStats gets assigned in a different condition if we are not locked on
																 // by default, locked on is set to false
				maxEnemyHealth = enemyDisplayedStats.maxEnemyHealth;
				enemyHealthRatio = (enemyHealth / maxEnemyHealth);
		
				_EnemyRectTransform.localScale = new Vector3 (enemyHealthRatio, 1, 1);
				return;// while we are locked on, this return keyword will prevent the rest of the Update method from executing.

			} else if (lockedOn == false && PlayerView.facingAnEnemy == true) { // if we are not locked on
				backBar.enabled = true;
				frontBar.enabled = true;

				enemyDisplayed = PlayerView.facingThisEnemy; // allow for mutability of which enemy gets the health bar presented.
				enemyDisplayedStats = enemyDisplayed.GetComponent<EnemyStats> ();
				enemyHealth = enemyDisplayedStats.enemyHealth;

				maxEnemyHealth = enemyDisplayedStats.maxEnemyHealth;
				enemyHealthRatio = (enemyHealth / maxEnemyHealth);

				_EnemyRectTransform.localScale = new Vector3 (enemyHealthRatio, 1, 1);
			} else if (PlayerView.facingAnEnemy == false) {
				backBar.enabled = false;
				frontBar.enabled = false;
			}
			 
		} else { // if AttackTrigger.battle == false
			backBar.enabled = false;
			frontBar.enabled = false;
			lockedOn = false; //prevents empty enemy health bar from appearing after battle
		}
	}

	void lockOn () {
		if (PlayerView.facingAnEnemy == true) {
			if (Input.GetKeyDown (KeyCode.E)) {
				if (!lockedOn) {
					lockedOn = true;
					enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = true;
				} else {
					lockedOn = false;
					enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		} else { // if we are not facing an enemy
			if (Input.GetKeyDown (KeyCode.E)) {
				if (!lockedOn) {
					//find the nearest enemy and lock onto that
				} else {
					lockedOn = false;
					enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
				}
					
			}
		}


	}
}
	
		
	 



		

