using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour {

	public static HealthBar healthBar; // made public static so we can access the bool for cutScene

	private float currHealth;
	private float currMaxHealth;	// these must be floats to do the division and create the ratio.
	private float currHealthRatio; 

	private float otherPlayerHealth;
	private float otherPlayerMaxHealth;
	private float otherPlayerHealthRatio;

	private float otherPlayer1Health;
	private float otherPlayer1MaxHealth;
	private float otherPlayer1HealthRatio;

	public static bool cutScene = false; // publically accessable boolian to flip canvas UI on and off. ***Whole canvas

	RectTransform _healthRectTransform = null;
	RectTransform _healthRectTransform1 = null;
	RectTransform _healthRectTransform2 = null;

	Canvas canvas;

	// Use this for initialization
	void Start () { 
		_healthRectTransform = gameObject.transform.root.GetChild(0).GetChild(0).GetComponent<RectTransform>(); // Main health bar
		_healthRectTransform1 = gameObject.transform.root.GetChild(2).GetChild(0).GetComponent<RectTransform>();// First Small Health bar
		_healthRectTransform2 = gameObject.transform.root.GetChild(4).GetChild(0).GetComponent<RectTransform>();// Second Small Health bar

		canvas = gameObject.transform.root.GetComponent<Canvas>();
		DontDestroyOnLoad (gameObject.transform.root.gameObject); // allows health bars to persist through scenes
	}
	
	// Update is called once per frame
	void Update () {
		if (cutScene)
			canvas.enabled = false; // turns off the healthbars if in cutscene
			
		currHealth = PlayerStats.currPlayerHealth;
		currMaxHealth = PlayerStats.currPlayerMaxHealth;

		otherPlayerHealth = PlayerStats.otherPlayerHealth;
		otherPlayerMaxHealth = PlayerStats.otherPlayerMaxHealth;

		otherPlayer1Health = PlayerStats.otherPlayer1Health;
		otherPlayer1MaxHealth = PlayerStats.otherPlayer1MaxHealth;

		currHealthRatio = (currHealth / currMaxHealth);
		otherPlayerHealthRatio = (otherPlayerHealth / otherPlayerMaxHealth);
		otherPlayer1HealthRatio = (otherPlayer1Health / otherPlayer1MaxHealth);

		_healthRectTransform.localScale = new Vector3 (currHealthRatio, 1, 1); 
		_healthRectTransform1.localScale = new Vector3 (otherPlayerHealthRatio, 1, 1);
		_healthRectTransform2.localScale = new Vector3 (otherPlayer1HealthRatio, 1, 1);
	}
}
