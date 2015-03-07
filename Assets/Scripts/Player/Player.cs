using UnityEngine;
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
	public int rage = 100;
	public float rageMult = 1.0f;
	public int rageIncOnHit = 5;
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

	public AudioClip deathNoise;
	public AudioClip getHitNoise;

	
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
			audio.PlayOneShot(deathNoise, 0.7F);
			gameObject.transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
			StartCoroutine(MyCoroutine());
		} else {
			incRage((int)(rageMult*rageIncOnHit));
			audio.PlayOneShot(getHitNoise, 0.7F);
		}
	}
	
	IEnumerator MyCoroutine()
    {
		Camera.main.gameObject.transform.position = Vector3.Lerp (Camera.main.gameObject.transform.position, EventHandler.Instance.DeathPosition.transform.position,5.0f);
		GetComponent<MouseLook> ().enabled = false;
		GetComponent<RigidbodyFPS> ().enabled = false;
		Camera.main.transform.LookAt(EventHandler.Instance.Center.transform);
		this.rigidbody.useGravity = false;
		HiveMind.Instance.destroyAll ();
		yield return new WaitForSeconds (3);
		EventHandler.Instance.gameOverExplosion ();
		print("Waiting");
        yield return new WaitForSeconds(7);
		Application.OpenURL ("http://goo.gl/forms/UM2v06UOhE"); // open url at end of game
		Application.LoadLevel("menuScene");
		print("Waited");

    }

	// === RAGE ===
	public int getRage(){
		return rage;
	}
	
	public void setRage(int rage){
		this.rage = (int)(rage);
		rageNumber.text = this.rage.ToString();
	}
	
	public void incRage(int rage){
		this.rage += (int)(rage);
		rageNumber.text = this.rage.ToString();
	}
	
	public void incRageMult(){
		rageMult += .2f;
	}
	
	public float getRageMult(){
		return rageMult;
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
