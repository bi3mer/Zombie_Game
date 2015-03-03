using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public int baseDamage = 10;
	public int strength = 1;
	
	Animator anim;
	public Collider col;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1) && gameObject.tag == "rarm") {
			anim.SetTrigger("armHit");
		}
		if (Input.GetMouseButtonDown(0) && gameObject.tag == "larm") {
			anim.SetTrigger("armHit");
		}
	}

	public int getStrength(){
		return this.strength;
	}

	public void setStrength(int strength){
		this.strength = strength;
	}

	// If the arms collide with something than deal damage and play the sounds effect.
	void OnCollisionEnter(Collision collider){
		if(collider.gameObject.CompareTag("enemy")){
			GameObject enemy = collider.gameObject;
			DealDamage(enemy);
			//audio.Play();
		}
	}
	
	// Will talk to the enemy script to deal damage to a specific enemy.	
	void DealDamage(GameObject enemy){
		Drone drone = enemy.GetComponent ("Drone") as Drone;
		drone.getDamage (baseDamage * strength);
	}
}
