using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	private int player1Health = 25; // these values need to be pulled from playerSave data at some point
	private int player2Health = 75;
	private int player3Health = 30;

	private int player1MaxHealth = 50; // these values need to be pulled from playerSave data at some point
	private int player2MaxHealth = 100;
	private int player3MaxHealth = 30;

	private int player1Mana = 25;
	private int player2Mana = 20;
	private int player3Mana = 30;

	private int player1MaxMana = 50;
	private int player2MaxMana = 100;
	private int player3MaxMana = 30;

	public static PlayerStats playerStats;

		public static int currPlayerHealth;
		public static int currPlayerMaxHealth;
		public static int currPlayerMana;
		public static int currPlayerMaxMana;

		public static int otherPlayerHealth;
		public static int otherPlayerMaxHealth;
		public static int otherPlayerMana;
		public static int otherPlayerMaxMana;

		public static int otherPlayer1Health;
		public static int otherPlayer1MaxHealth;
		public static int otherPlayer1Mana;
		public static int otherPlayer1MaxMana;

	private bool blue;
	private bool green;
	private bool pink;

	// Use this for initialization
	// In this script we check to see which sprite we are using and that corresponds to their respective health and mana
	// but this should be rebound later because when we animate movement we're not going to be using just 1 sprite.

	void Start () {
		blue = SpriteController.blue; // These bools should be renamed to the names of the characters and not depend on sprites;
		green = SpriteController.green;
		pink = SpriteController.pink;
		currPlayerStats ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Q)) {
			blue = SpriteController.blue; // These bools should be renamed to the names of the characters
			green = SpriteController.green; 
			pink = SpriteController.pink;
			currPlayerStats ();
		}
	}

	void currPlayerStats () {
		if (blue) {
			currPlayerHealth = player1Health;
			currPlayerMaxHealth = player1MaxHealth;
			currPlayerMana = player1Mana;
			currPlayerMaxMana = player1MaxMana;

			otherPlayerHealth = player3Health;
			otherPlayerMaxHealth = player3MaxHealth;
			otherPlayerMana = player3Mana;
			otherPlayerMaxMana = player3MaxMana;

			otherPlayer1Health = player2Health;
			otherPlayer1MaxHealth = player2MaxHealth;
			otherPlayer1Mana = player2Mana;
			otherPlayer1MaxMana = player2MaxMana;

		} else if (green) {
			currPlayerHealth = player2Health;
			currPlayerMaxHealth = player2MaxHealth;
			currPlayerMana = player2Mana;
			currPlayerMaxMana = player2MaxMana;

			otherPlayerHealth = player1Health;
			otherPlayerMaxHealth = player1MaxHealth;
			otherPlayerMana = player1Mana;
			otherPlayerMaxMana = player1MaxMana;

			otherPlayer1Health = player3Health;
			otherPlayer1MaxHealth = player3MaxHealth;
			otherPlayer1Mana = player3Mana;
			otherPlayer1MaxMana = player3MaxMana;
		} else if (pink) {
			currPlayerHealth = player3Health;
			currPlayerMaxHealth = player3MaxHealth;
			currPlayerMana = player3Mana;
			currPlayerMaxMana = player3MaxMana;

			otherPlayerHealth = player2Health;
			otherPlayerMaxHealth = player2MaxHealth;
			otherPlayerMana = player2Mana;
			otherPlayerMaxMana = player2MaxMana;

			otherPlayer1Health = player1Health;
			otherPlayer1MaxHealth = player1MaxHealth;
			otherPlayer1Mana = player1Mana;
			otherPlayer1MaxMana = player1MaxMana;
		}
	}

	void TakeDamage(int damage) {
		currPlayerHealth -= damage;
	}


}
