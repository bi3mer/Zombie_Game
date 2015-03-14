using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class PlayerAttack : MonoBehaviour {

	public int baseDamage = 10;
	public int strength = 1;
	
	Animator anim;
	public Collider col;
	
	public int button = 0;
	public int controller = 0;
	
	public AudioClip zombieNoise;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(button) || Input.GetAxis("XFire1") == controller) {
			anim.SetTrigger("armHit");
			audio.PlayOneShot(zombieNoise, 1.0F);
		}
	}

	public int getStrength(){
		return this.strength;
	}

	public void setStrength(int strength){
		this.strength = strength;
	}

	// If the arms collide with something than deal damage and play the sounds effect.
	void OnCollisionStay(Collision collider){
		if(collider.gameObject.CompareTag("enemy")){
			GameObject enemy = collider.gameObject;
			DealDamage(enemy);
		}
	}
	
	// Will talk to the enemy script to deal damage to a specific enemy.	
	void DealDamage(GameObject enemy){
		DroneAbstract drone = enemy.GetComponent ("DroneAbstract") as DroneAbstract;
		drone.getDamage(baseDamage * strength);
	}
}
