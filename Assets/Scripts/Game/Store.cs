using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Store : MonoBehaviour {

	public Material startcolor;
	public Material hovercolor;
 
	//public Audioclip sound; 
	public int cost = 10;

	public GameObject player;
	Player PlayerVars;
 
	bool business = false;
 
	string type;
	
	public State state;
	
	public AudioClip buyNoise;
	
	public Text costGUI;
	
	public enum State{
		Stamina, Anger, Strength, Speed, Heal, Damage, INFO
	}
		
	void Start(){
		PlayerVars = player.GetComponent<Player>();		
		costGUI.text = cost.ToString();
	}
	
	void OnMouseEnter() {
		if (Vector3.Distance(Camera.main.gameObject.transform.position, gameObject.transform.position) < 10f) {
			startcolor = renderer.material;
			renderer.material = hovercolor;
			business = true;
		}
	}
	
	void buy(){
		PlayerVars.setRage(PlayerVars.getRage() - cost);
		audio.PlayOneShot(buyNoise, 0.1F);
		updateCost();
		if (PlayerVars.getRage() < 0)
			PlayerVars.setRage(0);
	}
	
	void updateCost(){
		cost = (int)(Mathf.Floor(cost * 1.5f));
		costGUI.text = cost.ToString();
	}

	void OnMouseOver(){
		if (Input.GetAxis("XFire1") == -1 || Input.GetAxis("XFire1") == 1)
			this.purchace();
	}
	
	void OnMouseExit() {
		renderer.material = startcolor;
		business = false;
	}
	
	void OnMouseDown() {
		this.purchace();
	}
	
	void purchace(){
		print("COST: " + cost);
		print("CURR: " + PlayerVars.getRage());
		if(business && PlayerVars.getRage() >= cost){
			
			switch(state){
			case State.Strength:
				print("Strength");
				PlayerVars.incStrength();
				buy();
				break;
				
			case State.Anger:
				print("Anger");
				buy();
				PlayerVars.incRageMult();
				break;
				
			case State.Stamina:
				PlayerVars.incStamina();
				print("Stamina");
				buy();
				break;
		
			case State.Speed:
				print("Speed");
				PlayerVars.incSpeed();
				buy();
				break;
			
			case State.Heal:
				PlayerVars.repHealth();
				print("Heal");
				buy();
				break;

			case State.Damage:
				PlayerVars.getDamage(10);
				print("Damage");
				break;

			case State.INFO:	
				print("-=INFO=-");
				break;
				
			default:
				print("Default");
				break;
			}
		}
	}
}
