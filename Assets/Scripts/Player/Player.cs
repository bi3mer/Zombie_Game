﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Handles the players values:
//	Health
//	Abilities
//	Rage
//	Kills?
public class Player : MonoBehaviour {

	public static Transform transform;

	public int maxHealth = 100;
	public int curHealth = 100;
	public int rage = 0;
	public int stamina = 1;
	public int speed = 12;
	public float jump = 4;
	public int strength = 1;

	public int damage = 10;
	public GameObject Larm;
	public GameObject Rarm;
	
	private RigidbodyFPS controller;
	private PlayerAttack attack1;
	private PlayerAttack attack2;

	
	public Text healthNumber;
	public Text rageNumber;

	void Awake() {
		transform = gameObject.GetComponent<Transform>();
		attack1 = Larm.GetComponent<PlayerAttack>();
		attack2 = Rarm.GetComponent<PlayerAttack>();
		controller = this.GetComponent<RigidbodyFPS>();
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


	// === DAMAGE ===
	public void getDamage(int damage){
		setHealth(curHealth -= damage);
		if (curHealth <= 0){
			Destroy(this.gameObject);
		}
	}

	// === RAGE ===
	public int getRage(){
		return rage;
	}
	
	public void setRage(int rage){
		this.rage = rage;
		rageNumber.text = rage.ToString();
	}

	// === HEALTH ===
	int getHealth(){
		return curHealth;
	}

	void setHealth(int health){
		this.curHealth = health;
		healthNumber.text = health.ToString();
	}

	public void repHealth(){
		this.setHealth(maxHealth);
	}

	// === Player Stamina ===
	public int getStamina(){
		return stamina;
	}
	
	public void incStamina(){
		this.stamina++;
		this.setHealth (curHealth + 10);
		this.maxHealth += 10;
	}

	// === Player Strength ===
	public void incStrength(){
		attack1.setStrength(attack1.getStrength() + 1);
		attack2.setStrength(attack2.getStrength() + 1);
		print("Strength: " + attack1.getStrength());
	}
	
	// === Player Speed ===
	public int getSpeed(){
		return speed;
	}
	
	public void incSpeed(){
		this.speed ++;
		this.jump += .2f;
		controller.runSpeed = this.speed;
		controller.runBackwardSpeed = this.speed;
		controller.runSidestepSpeed = this.speed;
		controller.jumpHeight = this.jump;
		print ("Speed: " + this.speed);
		print ("Jump: " + this.jump);
	}
}
