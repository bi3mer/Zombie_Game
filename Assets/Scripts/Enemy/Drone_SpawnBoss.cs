using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Drone_SpawnBoss : DroneAbstract {
	public int attackRange;
	public int attack;

	private int hits = 900;
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
		attackRange += Random.Range (10,20);
		searchRange += Random.Range (15, 30);
		moveSpeed 	+= Random.Range (10, 15);
		attack      += Random.Range (5,15);
		
		// multiply values based on waves for balancing
		moveSpeed   += (moveSpeed + (EventHandler.Instance.getWave () / 10));

	
		this.GetComponent<NavMeshAgent> ().stoppingDistance = attackRange - 1;
		this.GetComponent<NavMeshAgent> ().speed = moveSpeed;
		
		walkStop = 5 + (int)attackRange; 

		if(searchRange < attackRange)
		{
			searchRange = attackRange + 1;
		} 
		this.addToHive ();
		this.checkSelf ();

		StartCoroutine (attackPlayer ());
	}
	
	public IEnumerator attackPlayer()
	{
		while(true)
		{
			//collider or overlap sphere
			if(   player.position.x+attackRange > transform.position.x && player.position.x - attackRange < transform.position.x
			   && player.position.y+attackRange > transform.position.y && player.position.y - attackRange < transform.position.y
			   && player.position.z+attackRange > transform.position.z && player.position.z - attackRange < transform.position.z)
			{
				bulletSpawn = new Vector3 (this.transform.position.x, this.transform.position.y+1, this.transform.position.z); // this y+1 will need to be changed to be dynamic
				GameObject bullet = Instantiate (EventHandler.Instance.BossBullet, bulletSpawn, Quaternion.Inverse(this.player.transform.rotation)) as GameObject;
				bullet.GetComponent<DroneFire>().setDmg(this.attack);
			}
			yield return new WaitForSeconds (1.5f);
		}
	}

	public override void makeExplosion()
	{
		Vector3 explosionPosition = new Vector3 (this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
		Instantiate(EventHandler.Instance.bossExplosion,explosionPosition,Quaternion.identity);
	}

	public override void getDamage(int damage)
	{
		//print ("here");
		hits--;
		if(hits%300 == 0)
		{
			// spawn wave as a boss mechanic!
			StartCoroutine(EventHandler.Instance.spawnWaves ());
		}

		if(hits == 0)
		{
			this.player.gameObject.GetComponent<Player> ().incRage (100);
			this.destroySelf();
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "player")
		{
			col.gameObject.rigidbody.AddExplosionForce(100.0f,transform.position,5.0f,3.0f);
		}
	}
}
