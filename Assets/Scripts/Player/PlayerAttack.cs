using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public int baseDamage = 10;
	public int strength = 0;
	
	Animator anim;
	public Collider col;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			anim.SetTrigger("armHit");
		}
	}
	
	// If the arms collide with something than deal damage and play the sounds effect.
	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Enemy")){
			GameObject enemy = col.gameObject;
			DealDamage(enemy);
			audio.Play();
		}
	}
	
	// Will talk to the enemy script to deal damage to a specific enemy.	
	void DealDamage(GameObject enemy){
		
	}
}
