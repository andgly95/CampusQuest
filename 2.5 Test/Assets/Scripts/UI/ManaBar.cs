using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour {

	private float currMana;
	private float currMaxMana;
	private float currManaRatio;

	private float otherPlayerMana;
	private float otherPlayerMaxMana;
	private float otherPlayerManaRatio;

	private float otherPlayer1Mana;
	private float otherPlayer1MaxMana;
	private float otherPlayer1ManaRatio;

	RectTransform _manaRectTransform = null;
	RectTransform _manaRectTransform1 = null;
	RectTransform _manaRectTransform2 = null;

	// Use this for initialization
	void Start () {
		_manaRectTransform = gameObject.transform.root.GetChild(1).GetChild(0).GetComponent<RectTransform>(); // Main health bar
		_manaRectTransform1 = gameObject.transform.root.GetChild(3).GetChild(0).GetComponent<RectTransform>();// First Small Health bar
		_manaRectTransform2 = gameObject.transform.root.GetChild(5).GetChild(0).GetComponent<RectTransform>();// Second Small Health bar
	}

	// Update is called once per frame
	void Update () {

		currMana = PlayerStats.currPlayerMana;
		otherPlayerMana = PlayerStats.otherPlayerMana;
		otherPlayer1Mana = PlayerStats.otherPlayer1Mana;

		currMaxMana = PlayerStats.currPlayerMaxMana;
		otherPlayerMaxMana = PlayerStats.otherPlayerMaxMana;
		otherPlayer1MaxMana = PlayerStats.otherPlayer1MaxMana;

		currManaRatio = (currMana / currMaxMana);
		otherPlayerManaRatio = (otherPlayerMana / otherPlayerMaxMana);
		otherPlayer1ManaRatio = (otherPlayer1Mana / otherPlayer1MaxMana);
				
		_manaRectTransform.localScale = new Vector3 (currManaRatio, 1, 1);
		_manaRectTransform1.localScale = new Vector3 (otherPlayerManaRatio, 1, 1); 
		_manaRectTransform2.localScale = new Vector3 (otherPlayer1ManaRatio, 1, 1); 
	}
}

