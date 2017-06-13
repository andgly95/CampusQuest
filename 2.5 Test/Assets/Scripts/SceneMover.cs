using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using IsoTools.Physics;

public class SceneMover : MonoBehaviour {

	//SerializedField makes the private variable visible in the editor
	[SerializeField]public string loadLevel;

	//To use this script, attach it to the collider that you want to be the passage 
	//between levels. 
	//Make sure the collider "Is Trigger" is checked.
	//Add

	void OnIsoTriggerEnter(IsoCollider other)
	{
		Debug.Log("Went Through the door.");
		if (other.gameObject.tag=="Player"){
			SceneManager.LoadScene (loadLevel); 
		}
	}
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Went Through the door.");
		if (other.gameObject.tag=="Player"){
			SceneManager.LoadScene (loadLevel); 
		}
	}
}

