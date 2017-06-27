using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;
using IsoTools.Physics;
using UnityEngine.SceneManagement;

public class BattleTrans : MonoBehaviour {

	public static BattleTrans battleTrans;

	public static int prevSceneIndex;
	private int nextSceneIndex = 4;
	private int returningSceneIndex;


	public static float b4BattlePosX;
	public static float b4BattlePosY;
	public static float b4BattlePosZ;


	bool load;
	bool battling = false;

	GameObject player;
	IsoObject _isoPlayer;

	Transform _prevIsoWorld;
	Transform _currIsoWorld;


	void Start() {

		GameObject[] gObjs = UnityEngine.Object.FindObjectsOfType <GameObject>();

		foreach (GameObject g in gObjs) {
			if (g.tag == "IsoWorld") {
				_prevIsoWorld = g.GetComponent<Transform> ();
			}
		}
		GetPlayer ();
		_isoPlayer = player.gameObject.GetComponent<IsoObject> ();
	}

	void GetPlayer() {
		Transform[] ts = _prevIsoWorld.gameObject.GetComponentsInChildren<Transform> ();
		foreach (Transform t in ts) {
			if (t.gameObject.CompareTag ("Player")) {
				//Debug.Log ("We have the player " + t);
				player = t.gameObject;
			}
		}
	}

	void prevIsoWorldFinder(){
		GameObject[] gObjs = SceneManager.GetActiveScene ().GetRootGameObjects ();
		foreach (GameObject g in gObjs) {
			if (g.tag == "IsoWorld") {
				_prevIsoWorld = g.GetComponent<Transform> ();
			}
		}
	}

	void currIsoWorldFinder(){
		GameObject[] gObjs = UnityEngine.Object.FindObjectsOfType <GameObject>();
		foreach (GameObject g in gObjs) {
			if (g.tag == "IsoWorld") {
				_currIsoWorld = g.GetComponent<Transform> ();
			}
		}
	}

	void gameObjectLister(){ // a debugging function ignore
		GameObject[] gObjs = UnityEngine.Object.FindObjectsOfType <GameObject>();
		foreach (GameObject g in gObjs) {
			if (g.activeInHierarchy)
				Debug.Log ("Game Objects Are: " + g);
		}
	}



	void OnIsoTriggerEnter(IsoCollider other) { //This function takes us into the battle scene
		if (other.CompareTag( "Weapon")) { 
			_prevIsoWorld = player.transform.parent;
			if (!load) {
				load = true;

				returningSceneIndex = SceneManager.GetActiveScene().buildIndex;

				b4BattlePosX = _isoPlayer.position.x;
				b4BattlePosY = _isoPlayer.position.y;
				b4BattlePosZ = _isoPlayer.position.z;
				StartCoroutine (ChangeScene ());
			}
		}
	}


	IEnumerator ChangeScene() {
		prevSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		_prevIsoWorld.transform.DetachChildren(); // This line detaches everything that was in the first IsoWorld 
		prevIsoWorldFinder ();					// So we can move the player into the next scene.
		// ^ This line reassigns _prevIsoWorld to the now empty gameObject that it is.

		SceneManager.LoadScene (nextSceneIndex, LoadSceneMode.Additive);

		Scene nextScene = SceneManager.GetSceneAt (1); // gets the second scene in the list of SceneManager's added scene. This is an array

		SceneManager.MoveGameObjectToScene(player, nextScene);// gameObject refers to whatever gameobject this script is attached to

		yield return null;
		SceneManager.UnloadSceneAsync (prevSceneIndex); //This line destroys everything in the old scene if it hasnt been moved over.
		// Only works in one direction though.
	}

	void OnEnable() {

		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {

		Debug.Log ("Active scene is " + scene.name + ".");

		// logs the new room :)

		_prevIsoWorld.gameObject.SetActive(false); // Even after destroying this object it is still listed as active, so i set it to inactive
		Destroy((_prevIsoWorld as Transform).gameObject); //So that deletes the old IsoWorld

		//All i did was initialize _currIsoWorld and now we are setting _currIsoWorld to  be equal to the isoWorld of the new room
		//GameObject[] gObjs = SceneManager.GetActiveScene ().GetRootGameObjects ();
		currIsoWorldFinder();
		//gameObjectLister (); a debug function call
		player.transform.parent = _currIsoWorld.transform;//This sets the player's parent equal to our new _currIsoWorld
		//Transform _prevIsoWorld;
		_prevIsoWorld = _currIsoWorld.transform;
		_prevIsoWorld.gameObject.SetActive (true);
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
		load = false;
	}


}



