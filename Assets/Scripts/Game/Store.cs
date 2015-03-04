using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {

	public Material startcolor;
	public Material hovercolor;
 
	//public Audioclip sound; 
	public int cost = 10;
	private int curRage;
 
	Player PlayerVars;
 
	bool business = false;
 
	string type;
	
	public State state;
	
	public AudioClip buyNoise;
	
	public enum State{
		Stamina, Strength, Speed, Health, Damage, INFO
	}
		
	void Start(){
		GameObject player = GameObject.FindWithTag("player");
		PlayerVars = player.GetComponent<Player> ();
	}
	
	void OnMouseEnter() {
		if (Vector3.Distance(Camera.main.gameObject.transform.position, gameObject.transform.position) < 10f) {
			startcolor = renderer.material;
			renderer.material = hovercolor;
			curRage = PlayerVars.getRage();
			business = true;
		}
	}

	void OnMouseExit() {
		renderer.material = startcolor;
		business = false;
	}
	
	void OnMouseDown() {
		if(business && curRage >= cost){
			
			switch(state){
			case State.Strength:
				print("Strength");
				PlayerVars.incStrength();
				buy();
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
			
			case State.Health:
				PlayerVars.repHealth();
				print("Heath");
				break;

			case State.Damage:
				PlayerVars.getDamage(10);
				print("Damage");
				break;

			case State.INFO:	
				print("-=INFO=-");
				print("CURRENT RAGE: " + curRage);
				break;
				
			default:
				print("Default");
				print("CURRENT RAGE: " + curRage);
				break;
			}
		}
	}
	
	void buy(){
		cost = (int)(Mathf.Floor(cost * 1.5f));
		curRage = curRage - cost;
		PlayerVars.setRage(curRage);
		audio.PlayOneShot(buyNoise, 0.1F);
		print(curRage);
		if (curRage < 0)
			PlayerVars.setRage(0);
	}
}
