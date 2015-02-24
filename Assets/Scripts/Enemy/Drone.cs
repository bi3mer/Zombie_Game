using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {
	public int health;
	public int moveSpeed;
	//public int k;
	public int searchRange;
	public float attackRange;

	private float nextFire; // rate of fire
	private int walkStop;  // distance till find new move random direction
	private Vector3 target;
	private Transform player;
	private Vector3 bulletSpawn;
	/*
	 * This will need to be altered a bit, to not find the gameobjects with tag.
	 */
	// Use this for initialization
	void Start () {
		//print ("change to static player");
		this.player = GameObject.FindGameObjectWithTag ("player").transform;
		generateNewTarget ();
		this.GetComponent<NavMeshAgent> ().enabled = false; // Don't find path till necessary

		// Create drone charecteristics
		attackRange += Random.Range (2,40);
		searchRange += Random.Range (5, 10);
		health 		+= Random.Range (0, 50); // increase scale of model based on health?
		moveSpeed 	+= Random.Range (0, 5);

		// multiply values based on waves for balancing
		health 		+= (health      * EventHandler.Instance.getWave() / 5); //5 is magic number for now...
		moveSpeed 	+= (moveSpeed   + (EventHandler.Instance.getWave()/10));

		this.GetComponent<NavMeshAgent> ().stoppingDistance = attackRange - 1;
		this.GetComponent<NavMeshAgent> ().speed = moveSpeed;

		walkStop = 5 + (int)attackRange;

		nextFire = 10;
	
		// bullet spawn is so the bullets don't spawninside drone

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
		if(   target.x > transform.position.x - walkStop && target.x < transform.position.x + walkStop
		   && target.z > transform.position.z - walkStop && target.z < transform.position.z + walkStop)
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
		if(Time.time > nextFire)
		{
			bulletSpawn = new Vector3 (this.transform.position.x, this.transform.position.y + 1.7f, this.transform.position.z); // this y+1 will need to be changed to be dynamic
			Instantiate (EventHandler.Instance.bullet, bulletSpawn, Quaternion.Inverse(this.player.transform.rotation));
		}
	}
}