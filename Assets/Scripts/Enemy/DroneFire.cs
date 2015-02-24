using UnityEngine;
using System.Collections;

public class DroneFire : MonoBehaviour {
	public int lifeTime;
	public int speed;
	// Use this for initialization
	void Start () 
	{
		//print ("change to static player"); 
		rigidbody.velocity = GameObject.FindGameObjectWithTag ("player").transform.forward * speed;
		Destroy (gameObject,lifeTime);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "player") 
		{
			print ("do dmg to player!!!!!!");
		} 
		else if (other.gameObject.tag != "enemy")
		{
			print (other.gameObject.tag);
			Destroy (this.gameObject);
		}
	}
}
