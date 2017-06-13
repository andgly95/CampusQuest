using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;

public class CameraController : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
		//Debug.Log ("Camera position: " + transform.position);
	}
}
