using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

	Canvas canvas;

	public static CanvasManager canvasManager;

	public static bool cutScene = false; // publically accessable boolian to flip canvas UI on and off. ***Whole canvas
	public bool canvasCreated;

	void Awake() {
		if (canvasManager == null) {
			DontDestroyOnLoad (gameObject);
			canvasManager = this;
		} else if (canvasManager != this) {
			Destroy(gameObject);
		}
		canvas = gameObject.GetComponent<Canvas>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cutScene)
			canvas.enabled = false; // turns off the healthbars if in cutscene
	}
}
