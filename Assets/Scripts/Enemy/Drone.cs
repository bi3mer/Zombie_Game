using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {
	public int health;
	public int moveSpeed;
	public int k;
	public int searchRange;
	public float attackRange;
	
	private Vector3 target;
	private Transform player;
	/*
	 * This will need to be altered a bit, to not find the gameobjects with tag.
	 */
	// Use this for initialization
	void Start () {
		searchRange = 20;
		this.player = GameObject.FindGameObjectWithTag ("player").transform;
		generateNewTarget ();
		this.GetComponent<NavMeshAgent> ().enabled = false; // Don't find path till necessary
		HiveMind.Instance.addDrone(this);
	}

	// Update is called once per frame
	void Update () {
		// search to attack
		if(   player.position.x+attackRange > transform.position.x && player.position.x - attackRange < transform.position.x
		   && player.position.y+attackRange > transform.position.y && player.position.y - attackRange < transform.position.y
		   && player.position.z+attackRange > transform.position.z && player.position.z - attackRange < transform.position.z)
		{
			this.attackPlayer();
		}
//		moveTowardsPlayer();
//		moveRandomDirection();	
	}

	public void moveTowardsPlayer(){
		moveTowards (player);
	}

	public void moveRandomDirection(){
		if(   target.x > transform.position.x - 2 && target.x < transform.position.x + 2
		   && target.z > transform.position.z - 2 && target.z < transform.position.z + 2)
		{
			generateNewTarget ();
		}


		if(this.GetComponent<NavMeshAgent>().enabled == false && transform.position.y < 10)
		{
			this.GetComponent<NavMeshAgent>().enabled = true;
		}

		if(this.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
		{
			this.GetComponent<NavMeshAgent>().SetDestination(this.target);
		}
	
	}

	public void moveTowards(Transform movePlace){
		if(transform.position.y < 10){ // if component is still in the air, don't find a path
			this.GetComponent<NavMeshAgent>().enabled = true; // enable navmesh agent
			if (this.GetComponent<NavMeshAgent> ().pathStatus == NavMeshPathStatus.PathComplete) 
			{ // if a path has been found, move. if not do nothing.
				this.GetComponent<NavMeshAgent> ().SetDestination (movePlace.position);
			}
		}
	}

	public bool search(){
		if(   player.position.x+searchRange > transform.position.x && player.position.x - searchRange < transform.position.x
		   && player.position.y+searchRange > transform.position.y && player.position.y - searchRange < transform.position.y
		   && player.position.z+searchRange > transform.position.z && player.position.z - searchRange < transform.position.z)
		{
			return true;
		}
		return false;
	}

	public void generateNewTarget(){
		int walkRadius = 1000;
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;// will need to be changed
		NavMeshHit hit;
		NavMesh.SamplePosition (randomDirection, out hit, walkRadius, 1);
		Vector3 finalPosition = hit.position;
		target = finalPosition;
	}

	public void getDamage(int dmg)
	{
		this.health -= dmg;
		if(health <= 0)
		{
			print ("reverse these.... >>>> ?????");
			Destroy(this.gameObject);
			HiveMind.Instance.removeDrone(this);

		}
	}

	public void attackPlayer()
	{
		print ("attacking the player at some point in the near future");
	}
}


/*
		if (EventHandler.Instance.getDroneCount () <= 10)
		{
			moveTowardsPlayer ();
			print("less than 10");
			return;
		}

		if(player.position.x+searchRange > transform.position.x && player.position.x - searchRange < transform.position.x
		   && player.position.y+searchRange > transform.position.y && player.position.y - searchRange < transform.position.y
		   && player.position.z+searchRange > transform.position.z && player.position.z - searchRange < transform.position.z){
			//broadcast
			EventHandler.Instance.setFound(true);
			moveTowardsPlayer();
			print("Move towards " + EventHandler.Instance.getFound());
			return;
		} else {
			if(EventHandler.Instance.getFound() == true){
				moveTowardsPlayer();
				EventHandler.Instance.setFound(false);
				return;
			}
			EventHandler.Instance.setFound(false);
		}

		print ("not found");
		//moveRandomDirection();

		//print("Move away: " + EventHandler.Instance.getFound());
		*/
