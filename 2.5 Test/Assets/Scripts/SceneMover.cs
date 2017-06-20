using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using IsoTools.Physics;
using IsoTools;

//namespace IsoTools.Examples.Kenney{
public class SceneMover : MonoBehaviour {


	//SerializedField makes the private variable visible in the editor
	[SerializeField]private string loadLevel;
	private GameObject player;
		//new Vector3 prevRoomPlayerPos;
	IsoObject _isoObject = null;
	Transform _prevIsoWorld = null;
	Transform _currIsoWorld = null;


	/*
	//To use this script, attach it to the trigger collider that you want to be the passage 
	//between levels. 
	//Make sure the collider "Is Trigger" is checked.
	//Add Scenes to the file -> buildsettings
	// type in what scene the collider should take you to in the inspector
	void GetPlayer(){
		Transform[] ts = _prevIsoWorld.gameObject.GetComponentsInChildren<Transform> ();
		foreach (Transform t in ts)
			if (t.gameObject.CompareTag ("Player"))
				player = t.gameObject;
	}

	void OnEnable() {

		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		
	//	Debug.Log(SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadLevel)));
		Debug.Log("Active scene is " + scene.name + ".");

		// logs the new room :)

		GameObject[] gObjs = SceneManager.GetActiveScene ().GetRootGameObjects (); //Gets the root game objects in the active scene
		player = GameObject.FindGameObjectsWithTag ("Player")[0];
		_prevIsoWorld = player.transform.parent;
		Debug.Log ("_prevIsoWorld is " + _prevIsoWorld);
		//All i did was initialize _currIsoWorld and now we are setting _currIsoWorld to  be equal to the isoWorld of the new room
		//GameObject[] gObjs = SceneManager.GetActiveScene ().GetRootGameObjects ();
		Debug.Log("gObjs length in OnLevelFinishedLoading is: " + gObjs.Length);
		foreach (GameObject g in gObjs)
			if (g.tag == "IsoWorld")
				_currIsoWorld = g.GetComponent<Transform>();
		//Debug.Log ("This is the current " + _currIsoWorld);
		if (_prevIsoWorld == _currIsoWorld) {
			print ("Dud.."); // So they are not the same
		}
		player.transform.parent = _currIsoWorld.transform;//This sets the player's parent equal to our new _currIsoWorld
		//if (_prevIsoWorld == null) { apparently _prevIsoWorld is null.. The debug proves it
			//Debug.Log ("Conditional True!");
		_prevIsoWorld.parent = trashable.transform;
		Debug.Log ("What is" + _prevIsoWorld.parent);
		Destroy (trashable.gameObject);
		//Destroy ((_prevIsoWorld as Transform).gameObject);


		//GameObject trashable = new GameObject(); <- moved assignment to OnTrigger
		//_prevIsoWorld.parent = trashable.transform;
		//DestroyImmediate(_prevIsoWorld); // This line should destroy the old IsoWorld so we dont stack up 
		//Destroy((_prevIsoWorld as Transform).gameObject);
		DestroyImmediate(trashable);
	}	
		
	void OnDisable() {

		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
		//Destroy((_prevIsoWorld as Transform).gameObject);

	}

	void Start () {
		GameObject[] gObjs = SceneManager.GetActiveScene ().GetRootGameObjects ();
		Debug.Log("gObjs length in start is: " + gObjs.Length);
		foreach (GameObject g in gObjs)
			if (g.tag == "IsoWorld")
				_prevIsoWorld = g.GetComponent<Transform>();
		/* placed here this conditional destroys the current IsoWorld when you go into a new room
		 * if (_prevIsoWorld != null && _currIsoWorld != null) {
			Destroy((_prevIsoWorld as Transform).gameObject);
		}

		GetPlayer ();
		_isoObject = player.GetComponent<IsoObject> ();
	}*/
		
	void Update() {
		
	}

	void OnIsoTriggerEnter(IsoCollider other){
		//Debug.Log("IsoWent Through the door.");
		//prevRoomPlayerPos = (_isoObject.position);

		if (other.CompareTag ("Player")) {
			//player.SendMessage("IsoWorldDelete");

			_prevIsoWorld = player.transform.parent;
			//DontDestroyOnLoad (_prevIsoWorld);
			if (_prevIsoWorld != null) {
				Destroy (_prevIsoWorld);
			}
			GameObject trashable = new GameObject();

			SceneManager.LoadScene (loadLevel);
			//See OnLevelFinishedLoading now
			//_prevIsoWorld = (GameObject)transform.gameObject; cant convert transform to gameobject implicitly
			//_prevIsoWorld = _prevIsoWorld.gameObject; same error


		}
	}
}


