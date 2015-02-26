using UnityEngine;
using System.Collections;

public class DroneFire : MonoBehaviour {
	public int lifeTime;
	public int speed;

	private int damage;
	// Use this for initialization
	void Start () 
	{
		transform.LookAt (GameObject.FindGameObjectWithTag ("player").transform);
		//print ("change to static player"); 
		rigidbody.velocity = transform.forward * speed;
		Destroy (gameObject,lifeTime);

		// damage = 0;
	}

	public void setDmg(int dmg)
	{
		damage = dmg;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "player") 
		{
			// do dmg to player
			print ("do dmg to player!!!!!! " + damage);
			Destroy (this.gameObject);
		}
		else if(other.gameObject.tag == "envr")
		{
			Destroy(this.gameObject);
		}
	}
}
