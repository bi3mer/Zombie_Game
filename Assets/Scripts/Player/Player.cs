using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Handles the players values:
//	Health
//	Abilities
//	Rage
//	Kills?
public class Player : MonoBehaviour {

	static Transform playerTransform;

	public int maxHealth = 100;
	public int curHealth = 100;
	public int rage = 0;
	public int stamina = 1;
	public int speed = 1;
	public int strength = 1;

	public int damage = 10;
	public GameObject Larm;
	public GameObject Rarm;
	
	private PlayerController controller;
	
	public Text healthNumber;
	public Text rageNumber;

	void Awake() {
		controller = this.GetComponent<PlayerController>();
		playerTransform = this.GetComponent<Transform>();
	}
	
	// Use this for initialization
	void Start () {
		rage = 100;
		healthNumber.text = maxHealth.ToString();
		rageNumber.text = rage.ToString();
	}

	// Update is called once per frame
	void Update () {
	
	}

// Getters and Setters
	
	int getHealth(){
		return curHealth;
	}

	void setHealth(int health){
		this.curHealth = health;
		healthNumber.text = health.ToString();
	}
	
	// Get damage from enemies. Corresponds to enemy script
	void getDamage(int damage){
		setHealth(curHealth -= damage);
		if (curHealth <= 0){
			Destroy(this.gameObject);
		}
	}

	public int getRage(){
		return rage;
	}
	
	public void setRage(int rage){
		this.rage = rage;
		rageNumber.text = rage.ToString();
	}
	
	// Player Health
	public int getStamina(){
		return stamina;
	}
	
	public void setStamina(int stamina){
		this.stamina = stamina;
		this.stamina++;
		this.curHealth += 10;
		this.maxHealth += 10;
	}
	
	// Player Speed
	public int getSpeed(){
		return speed;
	}
	
	public void setSpeed(int speed){
		this.speed = speed;
		rageNumber.text = speed.ToString();
		this.speed++;
		controller.movement.maxForwardSpeed += 1;
		controller.movement.maxBackwardsSpeed += 1;
		controller.movement.maxSidewaysSpeed += 1;
	}
}
