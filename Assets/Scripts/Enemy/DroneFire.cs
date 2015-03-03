using UnityEngine;
using System.Collections;

public class DroneFire : MonoBehaviour {
	public int lifeTime;
	public int speed;
	private int damage;
	// Use this for initialization
	void Start () 
	{
		transform.LookAt (Player.transform);
		rigidbody.velocity = transform.forward * speed;
		Destroy (gameObject,lifeTime);
	}

	public void setDmg(int dmg)
	{
		damage = dmg;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "player") 
		{
			other.gameObject.GetComponent<Player>().getDamage(damage);   // deal damage to player
			Destroy (this.gameObject); // destroy self
		}
		else if(other.gameObject.tag == "envr")
		{
			Destroy(this.gameObject); //destroy self
		}
		// nothing is done if a drone hits another drone.
	}
}
