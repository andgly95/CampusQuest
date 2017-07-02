using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;
using IsoTools.Physics;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

	private float b4BattlePosX;
	private float b4BattlePosY;
	private float b4BattlePosZ;

	private float battlePosX = -2.057f;
	private float battlePosY = 2.3753f;
	private float battlePosZ = 0.3693f;

	private int battleSceneIndex;
	private int returningSceneIndex;

	GameObject player;
	public static GameObject[] enemyList;

	//public static bool victory;

	IsoObject _isoPlayer;

	Transform _prevIsoWorld;
	Transform _currIsoWorld;

	// Use this for initialization
	void Start () {
		AttackTrigger.battle = true;
		//victory = false;

		returningSceneIndex = BattleTrans.prevSceneIndex;

		prevIsoWorldFinder ();
		GetPlayer ();

		_isoPlayer = player.gameObject.GetComponent<IsoObject> (); // puts player at starting position
		_isoPlayer.position = new Vector3 (battlePosX, battlePosY, battlePosZ);
	}
	
	// Update is called once per frame
	void Update () {
	// if the battle started
		if (AttackTrigger.battle) {// count the number of enemies, if 0
			enemyCounter ();// send us back to original scene at original position
		}
	}

	void enemyCounter() {
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag ("Enemy");
		if (enemyList.Length == 0) {
			//victory = true;
			AttackTrigger.battle = false;
			StartCoroutine (ChangeSceneBack ());
		}
	}

	IEnumerator ChangeSceneBack() { // very much like previous method but with a slight difference
		battleSceneIndex = SceneManager.GetActiveScene ().buildIndex;

		_prevIsoWorld.transform.DetachChildren(); 
		prevIsoWorldFinder ();	
						
		SceneManager.LoadScene (returningSceneIndex, LoadSceneMode.Additive);

		Scene nextScene = SceneManager.GetSceneAt (1); 

		SceneManager.MoveGameObjectToScene(player, nextScene);

		_isoPlayer.position = new Vector3 (b4BattlePosX, b4BattlePosY, b4BattlePosZ); // this line places the player back in the same position that they
		// were before the battle started

		yield return null;
		SceneManager.UnloadSceneAsync (battleSceneIndex);
		OverWorldManager.enemyFighting.script.battleStarting = false;


	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {

		Debug.Log ("Active scene is " + scene.name + ".");

		_prevIsoWorld.gameObject.SetActive(false); 
		Destroy((_prevIsoWorld as Transform).gameObject); 

		currIsoWorldFinder();

		player.transform.parent = _currIsoWorld.transform;

		_isoPlayer.position = new Vector3 (BattleTrans.b4BattlePosX, BattleTrans.b4BattlePosY, BattleTrans.b4BattlePosZ);

		_prevIsoWorld.gameObject.SetActive (true);
		_prevIsoWorld = _currIsoWorld.transform;

	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void GetPlayer() {
		Transform[] ts = _prevIsoWorld.gameObject.GetComponentsInChildren<Transform> ();
		foreach (Transform t in ts) {
			if (t.gameObject.CompareTag ("Player")) {
				player = t.gameObject;
			}
		}
	}

	void prevIsoWorldFinder(){
		GameObject[] gObjs = UnityEngine.Object.FindObjectsOfType <GameObject>();
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

}
