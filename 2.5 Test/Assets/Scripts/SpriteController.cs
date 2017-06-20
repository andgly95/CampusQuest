using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {
	public Sprite greenAlien;
	public Sprite blueAlien;
	public Sprite pinkAlien;
	SpriteRenderer _spriteRenderer = null;
	// Use this for initialization
	void Start () {
		_spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		_spriteRenderer.sprite = blueAlien;
	}

	//We need to improve this SpriteController script by adding functions that play specific animations if certain keys are pressed

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)){
			Debug.Log ("Switching Characters!");
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == blueAlien) {
				_spriteRenderer.sprite = greenAlien;
				Debug.Log ("Switched to green!");
				return;
			}
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == greenAlien) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = pinkAlien;
				Debug.Log("Switched to pink!");
				return;
			}
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == pinkAlien) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = blueAlien;
				Debug.Log("Switched to blue!");
				return;
			}
		}

	}
}
