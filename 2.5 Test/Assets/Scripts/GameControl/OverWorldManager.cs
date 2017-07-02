using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldManager : MonoBehaviour {

	public static List<GameObject> defeatedEnemies;
	public static List<enemyAndScript> enemies;
	public static enemyAndScript enemyFighting;

	private bool scriptStarted; // this bool checks to see if we have run the script before

	// Use this for initialization
	void Start () {
		if (!scriptStarted) {
			Debug.Log ("We're here");
			defeatedEnemies = new List<GameObject> (); // the list of all enemies in a scene you defeated
			enemies = new List<enemyAndScript> (); 
			getEnemies ();
			scriptStarted = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyFighting == null) {
			foreach (enemyAndScript e in enemies) {
				if (e.script.battleStarting) {
					Debug.Log ("We're battling ");
					enemyFighting = e;
				}
			}
		}

		if (enemyFighting.script.battleStarting == false) {
			Debug.Log ("Victory");
			defeatedEnemies.Add (enemyFighting.enemy);
		}

		Debug.Log (defeatedEnemies.Count);
		foreach (GameObject e in defeatedEnemies) {
			if (defeatedEnemies.Count != 0)
				Debug.Log ("We defeated these enemies: " + e);
		}
	}

	List<enemyAndScript> getEnemies () { // method that returns our custom list of enemy gameObjects and their corresponding script
		enemies = new List<enemyAndScript>(); 
		foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy")) {
			enemies.Add (new enemyAndScript() {enemy = e, script = e.GetComponent<BattleTrans>()});
		}
		return enemies;
	}

}

public class enemyAndScript {
	public GameObject enemy {get; set;} // must be public so start method can find it
	public BattleTrans script { get; set;} //
}
