using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;
using IsoTools;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	//private Transform player;
	private Transform _IsoWorld;
	private Transform player;

	// Use this for initialization
	void Start () {

		GameObject[] gObjs = UnityEngine.Object.FindObjectsOfType <GameObject>();
		foreach (GameObject g in gObjs) {
			if (g.CompareTag( "Player")) {
				player = g.transform;
			}
		}
	}


	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			GetPlayer ();
			}
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
	}


	void GetPlayer(){
		GameObject[] gObjs = UnityEngine.Object.FindObjectsOfType <GameObject> ();
		foreach (GameObject g in gObjs) {
			if (g.CompareTag ("Player")) {
				player = g.transform;
			}
		}
	}

}
