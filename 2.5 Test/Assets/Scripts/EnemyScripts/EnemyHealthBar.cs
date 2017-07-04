using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

	private GameObject enemyDisplayed;
	private GameObject enemyLockedOn;
	EnemyStats enemyDisplayedStats;

	private List<SpriteRenderer> lookAtSprites;

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

		lookAtSprites = new List<SpriteRenderer> ();

		if (AttackTrigger.battle) {
			enemyDisplayed = PlayerView.facingThisEnemy;
			enemyDisplayedStats = enemyDisplayed.GetComponent<EnemyStats> ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (AttackTrigger.battle) {

			lockOn (); // give the player an opportunity to lock on
			if (lockedOn == true) {
				
				backBar.enabled = true;
				frontBar.enabled = true;

				enemyHealth = enemyDisplayedStats.enemyHealth;
				maxEnemyHealth = enemyDisplayedStats.maxEnemyHealth;
				enemyHealthRatio = (enemyHealth / maxEnemyHealth);

				_EnemyRectTransform.localScale = new Vector3 (enemyHealthRatio, 1, 1);

				if (enemyDisplayed == null) // if the enemy we're locked onto has been destroyed
					lockedOn = false; // turn off the lock on
				if (enemyDisplayed != null)
					enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false; // turn off lookAt sprite



				return;
			} else { // if we are not locked on
				if (PlayerView.facingThisEnemy != null) {// if we are facing an enemy

					enemyDisplayed = PlayerView.facingThisEnemy;// update the enemy displayed
					enemyDisplayedStats = enemyDisplayed.GetComponent<EnemyStats> ();// and its script

					lookAtAdjuster ();

					backBar.enabled = true;
					frontBar.enabled = true;

					enemyHealth = enemyDisplayedStats.enemyHealth;
					maxEnemyHealth = enemyDisplayedStats.maxEnemyHealth;
					enemyHealthRatio = (enemyHealth / maxEnemyHealth);
		
					_EnemyRectTransform.localScale = new Vector3 (enemyHealthRatio, 1, 1);
					return;
				} else  { // if we are not locked on and not looking at an enemy
					backBar.enabled = false;
					frontBar.enabled = false;

					if (enemyDisplayed != null)
						enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;

					return;// while we are locked on, this return keyword will prevent the rest of the Update method from executing.
				}
		
			}
			 
		} else {// if AttackTrigger.battle == false
			backBar.enabled = false;
			frontBar.enabled = false;
			lockedOn = false; //prevents empty enemy health bar from appearing after battle
		}
	}

	void lookAtAdjuster () {

		if (lookAtSprites.Count == 0) { 
			lookAtSprites.Add (enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ());
		}

		if (lookAtSprites [0].gameObject != enemyDisplayed.gameObject) { // if the lookAtSprite's gameobject is not the gameObject that we are facing
			lookAtSprites [0].enabled = false; // turn off the sprite renderer
			lookAtSprites.Remove (lookAtSprites [0]); // remove it from the list
			lookAtSprites.Add (enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ()); // add the correct sprite renderer
			lookAtSprites [0].enabled = true; // turn it on
		} else { // if they are the same gameObject
			lookAtSprites [0].enabled = true;// otherwise make sure the lookAtSprite is on
		} 

		if(lookAtSprites[0].gameObject == null) // this here makes sure that if we kill the enemy that we're looking at
			lookAtSprites.Remove (lookAtSprites [0]); // it wont cause null reference errors
	}

	void lockOn () {
		if (PlayerView.facingThisEnemy != null) {
			if (Input.GetKeyDown (KeyCode.E)) {
				if (!lockedOn) {
					lockedOn = true;

					enemyDisplayed = PlayerView.facingThisEnemy;
					enemyDisplayedStats = enemyDisplayed.GetComponent<EnemyStats> ();

					enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
					enemyDisplayed.transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = true;

				} else {
					lockedOn = false;
					if (enemyDisplayed != null)
						enemyDisplayed.transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		} else { // if we are not facing an enemy
			if (Input.GetKeyDown (KeyCode.E)) {
				if (!lockedOn) {
					//find the nearest enemy and lock onto that
					enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
				} else {
					lockedOn = false;
					enemyDisplayed.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
					enemyDisplayed.transform.GetChild (2).GetComponent<SpriteRenderer> ().enabled = false;
				}
					
			}
		}
	}
		

}

	
		
	 



		

