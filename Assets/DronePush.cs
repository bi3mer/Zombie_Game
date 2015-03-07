using UnityEngine;
using System.Collections;
using AssemblyCSharp; 
public class DronePush : DroneAbstract {

	// Use this for initialization
	void Start () {
		searchRange += Random.Range (5, 10);
		health 		+= Random.Range (0, 50); // increase scale of model based on health?
		moveSpeed 	+= Random.Range (1, 5);

		// multiply values based on waves for balancing
		health 		+= (health      * EventHandler.Instance.getWave() / 5);  //magic numbers in here for now. will balance later
		moveSpeed   += (moveSpeed + (EventHandler.Instance.getWave () / 10));
		//attack      += (attack 		+ (EventHandler.Instance.getWave()/10));
		
		this.GetComponent<NavMeshAgent> ().stoppingDistance = 0;
		this.GetComponent<NavMeshAgent> ().speed = moveSpeed;
		this.GetComponent<NavMeshAgent> ().enabled = false;
		searchRange = 20;

		player = Player.transform;

		HiveMind.Instance.addDrone(this);
		this.checkSelf ();
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "player")
		{
			col.gameObject.rigidbody.AddExplosionForce(50.0f,transform.position,5.0f,3.0f);
		}
	}

	

}
