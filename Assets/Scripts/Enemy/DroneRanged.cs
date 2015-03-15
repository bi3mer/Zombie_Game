using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class DroneRanged : DroneAbstract {
	public int attackRange;
	public int attack;

	// Use this for initialization
	void Start () 
	{
		player = Player.transform; 
		generateNewTarget (); // generate point on map to move to, if player is not visible.
		this.GetComponent<NavMeshAgent> ().enabled = false; // Don't find path till necessary

		// Create drone charecteristics
		attackRange += Random.Range (20, 30);
		searchRange += Random.Range (5, 10);
		health 		+= Random.Range (100, 150); // increase scale of model based on health?
		moveSpeed 	+= Random.Range (10, 15);
		attack      += Random.Range (3, 8);

		// multiply values based on waves for balancing
		health 		+= (health      * EventHandler.Instance.getWave() / 2);  //magic numbers in here for now. will balance later
		moveSpeed   += (moveSpeed + (EventHandler.Instance.getWave () / 10));
		//attack      += (attack 		+ (EventHandler.Instance.getWave()/10));

		this.GetComponent<NavMeshAgent> ().stoppingDistance = attackRange - 1;
		this.GetComponent<NavMeshAgent> ().speed = moveSpeed;

		walkStop = 5 + (int)attackRange; 

		if(searchRange < attackRange)
		{
			searchRange = attackRange + 1;
		} 
		this.addToHive ();
		this.checkSelf ();

		StartCoroutine (attackPlayer ());// check and attack every second
	}

	// Update is called once per frame
	public IEnumerator attackPlayer()
	{
		while(true)
		{
//			print ("1");
			if(   player.position.x+attackRange > transform.position.x && player.position.x - attackRange < transform.position.x
			   && player.position.y+attackRange > transform.position.y && player.position.y - attackRange < transform.position.y
			   && player.position.z+attackRange > transform.position.z && player.position.z - attackRange < transform.position.z) // limits how fast the player can attack
			{
//				print ("2");
				bulletSpawn = new Vector3 (this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z); // this y+1 will need to be changed to be dynamic
				GameObject bullet = Instantiate (EventHandler.Instance.bullet, bulletSpawn, Quaternion.Inverse(this.player.transform.rotation)) as GameObject;
				bullet.GetComponent<DroneFire>().setDmg(this.attack);
			}
			yield return new WaitForSeconds (1);
		}
	}
}

