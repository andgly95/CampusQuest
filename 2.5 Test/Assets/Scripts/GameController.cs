using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

	public static GameController gameController;

	public float playerPositionX;
	public float playerPositionY;
	public float playerPositionZ;

	void Awake (){
		// this is called a singleton manager
		// if there is no gameController then
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			//set the gameController to this instance of the GameController class
			gameController = this;
		} else if (gameController != this) {
			Destroy (gameObject);
		}
	}

	public void Save(){
		//Creates a binary formatter and a File
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		
		//Creates an Object to save data to
		PlayerData data = new PlayerData(); //<- calling the constructor below
		data.playerPosX = playerPositionX;
		data.playerPosY = playerPositionY;
		data.playerPosZ = playerPositionZ;
	
		//Writes the object to the file & closes it
		bf.Serialize(file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
		}
	}

	public void Delete(){ //This method deletes the persistent 
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			File.Delete(Application.persistentDataPath + "/playerInfo.dat");
		}
	}
}
// This is another class within this same script
// 
[Serializable] class PlayerData{
	public float playerPosX;
	public float playerPosY;
	public float playerPosZ;
}
