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
	public int dexterity = 1;
	public int speed = 1;
	public int strength = 1;

	public int damage = 10;
	public GameObject Larm;
	public GameObject Rarm;
	
	private CharacterMotor controller;	
	
	public Text healthNumber;

	void Awake() {
		controller = this.GetComponent<CharacterMotor>();
		playerTransform = this.GetComponent<Transform>();
	}
	
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	
	}

// Getters and Setters
	void setHealth(int health){
		this.curHealth = health;
		healthNumber.text = health.ToString();
	}
	
	// Get damage from enemies. Corresponds to enemy script
	void getDamage(int damage){
		setHealth(curHealth -= damage);
	}

	int getHealth(){
		return curHealth;
	}

	void setRage(int rage){
		this.rage = rage;
	}

	void updateStamina(){
		this.stamina++;
		this.curHealth += 10;
		this.maxHealth += 10;
	}

	void updateSpeed(){
		this.speed++;
		this.speed += 1;
		controller.movement.maxForwardSpeed += this.speed;
		controller.movement.maxBackwardsSpeed += this.speed;
		controller.movement.maxSidewaysSpeed += this.speed;
	}
}
