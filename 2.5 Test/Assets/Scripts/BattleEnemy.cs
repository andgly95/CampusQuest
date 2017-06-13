using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;
using Pathfinding;
//using IsoTools.Examples.Kenny;

//namespace IsoTools.Examples.Kenney {
	[RequireComponent (typeof(IsoRigidbody))]
	[RequireComponent (typeof(Seeker))]
	//[RequireComponent(typeof(IsoTriggerListener), typeof(IsoCollisionListener))]
	public class BattleEnemy : MonoBehaviour {

		public Transform target;
		// How many times a second we will update our path
		public float updateRate = 2f;
		// Caching
		private Seeker seeker;
		private IsoRigidbody rb;

		// The calculated path
		public Path path;

		// The AI's speed per second
		public float speed = 300f;
		public ForceMode fMode;

		[HideInInspector]
		public bool pathIsEnded = false;

		// the max distance from the AI to a waypoint for it to continue to the next waypoint
		public float nextWayPointDist = 3f;
		
		//The waypoint we are currently moving towards, referenced by an index
		//thats why its an int
		private int currentWayPoint = 0;

	void start() {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<IsoRigidbody> ();
		if(target == null) {
			Debug.Log ("No Player found.");
		}
		// start a new path to the target position
		// and return the result to the onpath complete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);
		//then we call a function that updates the path if the player moves
		// this happens twice a second: see UpdateRate
		StartCoroutine (UpdatePath ());	
	}

	IEnumerator UpdatePath () {
		if (target == null) {// iF there is no player
			//TODO: INSERT A PLAYER SEARCH HERE
			yield return false;
		}
		//if there is a target.
		seeker.StartPath (transform.position, target.position, OnPathComplete);
		//and then we wait to call the path finder again
		yield return new WaitForSeconds (1 / updateRate);
		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete(Path p) {
		Debug.Log ("We got a path. Did it have an error?" + p.error);
		if (!p.error) {
			path = p;
			currentWayPoint = 0;
		}
	}

	void FixedUpdate() {// like update, but only updates when a change occurs
		if (target == null) {// iF there is no player
			//TODO: INSERT A PLAYER SEARCH HERE
			return;
		}
			if (path == null)
				return;
			if (currentWayPoint >= path.vectorPath.Count) {
				if (pathIsEnded)
					return;
				Debug.Log ("End of path reached. ");
				pathIsEnded = true;
				return;
			}
			pathIsEnded = false;

			//Direction to the next waypoint
			Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
			dir *= speed * Time.fixedDeltaTime;

			rb.AddForce(dir, fMode);

			float dist = Vector3.Distance (transform.position, path.vectorPath [currentWayPoint]);
			if (dist < nextWayPointDist) {
				currentWayPoint++;
				return;
			}
	}
}

	