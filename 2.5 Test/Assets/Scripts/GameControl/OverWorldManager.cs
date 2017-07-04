using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldManager : MonoBehaviour {

	// for this script to work each enemy placed on the overworld needs to have a unique index that distinguishes each enemy from one another
	// with each room transition the indexes are flushed, so the same index can be used for two different enemies in separate rooms.
	// indexes are specified in the editor as public variables.

	public static List<int> defeatedEnemyIndexes;
	public static List<enemyAndScript> enemies;
	public static enemyAndScript enemyFighting;

	private GameObject enemyToAdd;

	private int fightingIndex;

	// Use this for initialization
	void Start () {
		defeatedEnemyIndexes = new List<int> (); // the list of all enemies in a scene you defeated
		enemies = new List<enemyAndScript> (); 
		enemies = getEnemies ();
	}
	
	// Update is called once per frame
	// *** reference enemies loaded in a scene by index so we can destroy their gameobjects after the player defeats them in battlescene
	void Update () {
		if (enemyFighting == null) { // <- will always be null in the overworld
			foreach (enemyAndScript e in enemies) {
				if (e.script.battling) {
					enemyFighting = e;
					fightingIndex = enemyFighting.script.index;
				}
			}
		}

		if (BattleManager.victory == true) { //&& enemyAdded == false) { // put this in a method
			Debug.Log ("Victory");			// if true record the enemy by their index and add them to the list
			foreach (enemyAndScript e in enemies) {
				if (fightingIndex == e.script.index) {
					defeatedEnemyIndexes.Add (e.script.index);
				}
			}
		}

		if (AttackTrigger.battle == false) { // after the battle and when walking between rooms
			getEnemies();
			indexComparer ();
		}

	}

	void indexComparer () {
		foreach (enemyAndScript e in enemies) {
			foreach (int index in defeatedEnemyIndexes) {
				if (index == e.script.index) {
					Destroy (e.enemy.gameObject);
				}
			}
		}
	}

	List<enemyAndScript> getEnemies () { // method that returns our custom list of enemy gameObjects and their corresponding index
		enemies = new List<enemyAndScript>(); 
		foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy")) {
			enemies.Add (new enemyAndScript() {enemy = e, script = e.GetComponent<OverworldEnemy>()});
		}
		return enemies;
	}

}

public class enemyAndScript {
	public GameObject enemy {get; set;} // must be public so start method can find it
	public OverworldEnemy script { get; set;} //
}
