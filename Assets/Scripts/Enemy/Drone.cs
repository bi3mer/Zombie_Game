using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Drone : DroneAbstract {
	public int attackRange;
	public int attack;

	private int fireTime;
	private float nextFire; // rate of fire

	/*
	 * This will need to be altered a bit, to not find the gameobjects with tag.
	 */
	// Use this for initialization
	void Start () 
	{
		player = Player.transform; 
		generateNewTarget (); // generate point on map to move to, if player is not visible.
		this.GetComponent<NavMeshAgent> ().enabled = false; // Don't find path till necessary

		// Create drone charecteristics
		attackRange += Random.Range (2,20);
		searchRange += Random.Range (5, 10);
		health 		+= Random.Range (0, 50); // increase scale of model based on health?
		moveSpeed 	+= Random.Range (2, 10);
		attack      += Random.Range (1, 5);

		// multiply values based on waves for balancing
		health 		+= (health      * EventHandler.Instance.getWave() / 5);  //magic numbers in here for now. will balance later
		moveSpeed   += (moveSpeed + (EventHandler.Instance.getWave () / 10));
		//attack      += (attack 		+ (EventHandler.Instance.getWave()/10));

		this.GetComponent<NavMeshAgent> ().stoppingDistance = attackRange - 1;
		this.GetComponent<NavMeshAgent> ().speed = moveSpeed;

		walkStop = 5 + (int)attackRange; 

		fireTime = 0;
		nextFire = 25;
		if(searchRange < attackRange)
		{
			searchRange = attackRange + 1;
		} 
		this.addToHive ();
		this.checkSelf ();
	}

	// Update is called once per frame
	void Update () 
	{
		fireTime++;
		// search to attack
		if(   player.position.x+attackRange > transform.position.x && player.position.x - attackRange < transform.position.x
		   && player.position.y+attackRange > transform.position.y && player.position.y - attackRange < transform.position.y
		   && player.position.z+attackRange > transform.position.z && player.position.z - attackRange < transform.position.z)
		{
			attackPlayer();
		}
	}

	public void attackPlayer()
	{
		if(fireTime > nextFire) // limits how fast the player can attack
		{
			fireTime = 0;
			bulletSpawn = new Vector3 (this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z); // this y+1 will need to be changed to be dynamic
			GameObject bullet = Instantiate (EventHandler.Instance.bullet, bulletSpawn, Quaternion.Inverse(this.player.transform.rotation)) as GameObject;
			bullet.GetComponent<DroneFire>().setDmg(this.attack);
		}
	}
}


/*
 * RaycastHit hit;
		Ray shotRay = new Ray (transform.position, transform.forward);
		
		if (Physics.Raycast (shotRay, out hit, 100)) 	
		{
			
			if(hit.collider.tag == "player")
			{
				transform.LookAt (GameObject.FindGameObjectWithTag ("player").transform);
				print ("hello raycast");
				rigidbody.velocity = transform.forward * speed;
				Destroy (gameObject,lifeTime);
			}
			
			
		}
	}

*/