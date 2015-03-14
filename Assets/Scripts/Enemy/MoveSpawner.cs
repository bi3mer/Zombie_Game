using UnityEngine;
using System.Collections;

public class MoveSpawner : MonoBehaviour {
	public GameObject[] spawnPoints;
	public float fracJourney;
	public int wave;

	private int index;

	void Start(){
		this.spawnPoints = GameObject.FindGameObjectsWithTag ("spawn_point");
		//print (this.spawnPoints.Length );
		this.index = 0;
		this.changeIndex ();
	}

	void changeIndex(){
		if(this.spawnPoints.Length == 0){
			return;
		}
		int temp = this.index;
		while(this.index == temp)
			this.index = Random.Range (0, this.spawnPoints.Length );
	} 


	void Update () {
		if (transform.position == this.spawnPoints [index].transform.position) {
			this.changeIndex();		
		}
		transform.position = Vector3.MoveTowards(transform.position, this.spawnPoints[index].transform.position, Time.deltaTime*fracJourney);
	}
}
