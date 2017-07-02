using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {

	public static SpriteController spriteController;

	public Sprite blueAlien;
	public Sprite greenAlien;
	public Sprite pinkAlien;

	public static bool blue = false;
	public static bool green = false;
	public static bool pink = false;

	public SpriteRenderer _spriteRenderer = null;
	// Use this for initialization
	void Start () {
		_spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		_spriteRenderer.sprite = blueAlien;
		blue = true;
		green = false;
		pink = false; 
	}

	//We need to improve this SpriteController script by adding functions that play specific animations if certain keys are pressed

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)){
			
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == blueAlien) {
				_spriteRenderer.sprite = greenAlien;
				green = true;
				blue = false;
				return;
			}
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == greenAlien) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = pinkAlien;
				green = false;
				pink = true;
				return;
			}
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == pinkAlien) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = blueAlien;
				pink = false;
				blue = true;
				return;
			}
		}

	}
}
