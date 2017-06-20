using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;

public class PlayerSave : MonoBehaviour {

	//public static GameController gameController;
	public GameObject player;
	IsoObject _isoObject = null;

	void Update(){
		
		if (Input.GetKeyDown (KeyCode.X)) {
			GameController.gameController.Delete ();
		}

		if(Input.GetButtonDown("Save")){
			_isoObject = GetComponent<IsoObject> (); // <- try player.GetComponent<IsoObject>();
			GameController.gameController.playerPositionX = _isoObject.position.x;
			GameController.gameController.playerPositionY = _isoObject.position.y;
			GameController.gameController.playerPositionZ = _isoObject.position.z;
			GameController.gameController.Save ();
			Debug.Log ("Saving: X: " + GameController.gameController.playerPositionX + " Y: " + GameController.gameController.playerPositionY + " Z: " + GameController.gameController.playerPositionZ);
		}

		if (Input.GetButtonDown ("Load")) {
			
			GameController.gameController.Load ();

			//_isoObject = GetComponent<IsoObject>;

			_isoObject.position = new Vector3 ( // <- maybe use getComponent Iso Object
				GameController.gameController.playerPositionX,
				GameController.gameController.playerPositionY,
				GameController.gameController.playerPositionZ
			);
			Debug.Log ("loading transform.position: " + _isoObject.position);
		}

	}
}	
