using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {

	public Material startcolor;
	public Material hovercolor;
 
	//public Audioclip sound; 
	public int cost = 10;
 
	Player PlayerVars;
 
	bool business = false;
 
	string type;
	
	public State state;
	
	public enum State{
		Stamina, Strength, Speed, Health, Damage
	}
		
	void Start(){
		GameObject player = GameObject.FindWithTag("player");
		PlayerVars = player.GetComponent<Player> ();
	}
	
	void OnMouseEnter() {
		if (Vector3.Distance(Camera.main.gameObject.transform.position, gameObject.transform.position) < 10f) {
			startcolor = renderer.material;
			renderer.material = hovercolor;
			business = true;
		}
	}

	void OnMouseExit() {
		renderer.material = startcolor;
		business = false;
	}
	
	void OnMouseDown() {
		int curRage = PlayerVars.getRage();
		if(business && curRage >= cost){
			//audio.PlayOneShot(soußßßnd);
			PlayerVars.setRage(curRage - cost);
			cost = (int)(Mathf.Floor(cost * 1.5f));
			
			switch(state){
			case State.Strength:
				print("Strength");
				PlayerVars.incStrength ();
				cost = (int)(Mathf.Floor(cost * 1.5f));
				break;
				
			case State.Stamina:
				PlayerVars.incStamina();
				print("Stamina");
				cost = (int)(Mathf.Floor(cost * 1.5f));
				break;
		
			case State.Speed:
				print("Speed");
				PlayerVars.incSpeed();
				cost = (int)(Mathf.Floor(cost * 1.5f));
				break;
			
			case State.Health:
				PlayerVars.repHealth();
				print("Heath");
				break;

			case State.Damage:
				PlayerVars.getDamage(10);
				print("Damage");
				break;

			default:
				print("Default");

				break;
			}
		}
	}
}
