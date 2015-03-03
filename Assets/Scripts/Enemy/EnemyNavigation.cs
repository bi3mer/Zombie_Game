using UnityEngine;
using System.Collections;

public class EnemyNavigation : MonoBehaviour {
	private Transform target;
	/*
	 * This will all need to be changed to accomodate the static variable idea
	 */
	// Use this for initialization
	void Start () {
		this.target = Player.transform;
		this.GetComponent<NavMeshAgent> ().enabled = false; // Don't find path till necessary
	}
	
	// Update is called once per frame
	void Update () {
//		if(transform.position.y < 2){ // if component is still in the air, don't find a path
		this.GetComponent<NavMeshAgent>().enabled = true; // enable navmesh agent
		if (this.GetComponent<NavMeshAgent> ().pathStatus == NavMeshPathStatus.PathComplete) { // if a path has been found, move. if not do nothing.
			this.GetComponent<NavMeshAgent> ().SetDestination (target.position);
		}
//		}
	}
}
