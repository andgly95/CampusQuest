using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using IsoTools.Physics;

namespace IsoTools.Examples.Kenney{
public class SceneMover : MonoBehaviour {

	//SerializedField makes the private variable visible in the editor
	[SerializeField]private string loadLevel;
		public GameObject player;
		new Vector3 prevRoomPlayerPos;

	//To use this script, attach it to the collider that you want to be the passage 
	//between levels. 
	//Make sure the collider "Is Trigger" is checked.
	//Add Scenes to the file -> buildsettings
	// type in what scene the collider should take you to



	void Start () {
		
	}
	void Update() {
		
	}

		void OnIsoTriggerEnter(IsoCollider other){
			Debug.Log("IsoWent Through the door.");
			if (other.CompareTag ("Player")) {
				Debug.Log ("IsoAnd now we transition");
				SceneManager.LoadScene (loadLevel);
			} else if (other.CompareTag ("Player")) {
				Debug.Log ("IsoGoing Back");
				SceneManager.LoadScene (loadLevel);
			}
	}
}
}

