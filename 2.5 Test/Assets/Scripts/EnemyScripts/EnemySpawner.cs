using System.Collections;
using System.Collections.Generic;
using IsoTools.Physics;
using IsoTools;
using IsoTools.Physics.Internal;
using UnityEngine;
using IsoTools;
using IsoTools.Physics;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemy;                // The enemy prefab to be spawned.
	public Transform isoworld;
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	private Vector3 _isoSpawnPoint;
	private IsoObject _isoSpawn;
	private int index = 0;
	private GameObject instance = null;
	private int spawnPointIndex;


	void Start ()
	{
		_isoSpawnPoint = new Vector3 (1, -0.5f, 1.35f);
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		/*foreach (Transform spawns in spawnPoints) {
			_isoSpawnPoints [index] = _isoSpawn.position;
			index++;
			Debug.Log ("I am alive");
		}*/
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{

		// Find a random index between zero and one less than the number of spawn points.
		spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Debug.Log ("Spawn Point index" + spawnPointIndex);
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		instance = Instantiate (enemy, isoworld) as GameObject;
		instance.transform.parent = isoworld.transform;
		Debug.Log ("Instance is: " + enemy);
		_isoSpawn = instance.GetComponent<IsoObject> ();
		_isoSpawn.position = _isoSpawnPoint;
	}
}
