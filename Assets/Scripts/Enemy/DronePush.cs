using UnityEngine;
using System.Collections;
using AssemblyCSharp; 
public class DronePush : DroneAbstract {

	// Use this for initialization
	void Start () {
		searchRange += Random.Range (5, 10);
		health 		+= Random.Range (100,200); // push drones will always have more health than the average drone out there
		moveSpeed 	+= Random.Range (10, 12);

		// multiply values based on waves for balancing
		health 		+= (health      * EventHandler.Instance.getWave() / 2);  
		moveSpeed   += (moveSpeed + (EventHandler.Instance.getWave () / 10));
		//attack      += (attack 		+ (EventHandler.Instance.getWave()/10));
		
		this.GetComponent<NavMeshAgent> ().stoppingDistance = 0;
		this.GetComponent<NavMeshAgent> ().speed = moveSpeed;
		this.GetComponent<NavMeshAgent> ().enabled = false;
		searchRange = 20;

		player = Player.transform;

		this.addToHive ();
		this.checkSelf ();
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "player")
		{
			col.gameObject.rigidbody.AddExplosionForce(100.0f,transform.position,5.0f,3.0f);
		}
	}

	

}