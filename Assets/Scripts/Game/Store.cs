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
		Stamina, Strength, Speed
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
			//audio.PlayOneShot(sound);
			PlayerVars.setRage(curRage - cost);
			cost = (int)(Mathf.Floor(cost * 1.5f));
			
			switch(state){
			case State.Strength:
				print("Strength");
				break;
				
			case State.Stamina:
				print("Stamina");
				break;
		
			case State.Speed:
				print("Speed");
				break;
			
			default:
				print("Default");
				break;
			}
		}
	}
}
