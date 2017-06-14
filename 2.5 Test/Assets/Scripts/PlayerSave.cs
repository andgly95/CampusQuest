using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour {

	public static GameController gameController;

	void Update(){
		
		if (Input.GetKeyDown (KeyCode.X)) {
			GameController.gameController.Delete ();
		}

		if(Input.GetButtonDown("Save")){
			GameController.gameController.Save ();
			GameController.gameController.playerPositionX = transform.position.x;
			GameController.gameController.playerPositionY = transform.position.y;
			GameController.gameController.playerPositionZ = transform.position.z;

		}

		if (Input.GetButtonDown ("Load")) {
			GameController.gameController.Load ();
			transform.position = new Vector3 (
				GameController.gameController.playerPositionX,
				GameController.gameController.playerPositionY,
				GameController.gameController.playerPositionZ
			);
		}

	}
}	
