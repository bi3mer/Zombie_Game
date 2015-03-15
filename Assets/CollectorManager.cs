using UnityEngine;
using System.Collections;

public class CollectorManager : MonoBehaviour {

	private int count;
	public GameObject player;
	Player playerVars;

	// Use this for initialization
	void Start () {
		playerVars = player.GetComponent<Player>();	
		count = transform.childCount;
	}
	
	public void decrementCount(){
		count--;
		if (count <= 0){
			playerVars.incRage(1000);
		}
	}
}
