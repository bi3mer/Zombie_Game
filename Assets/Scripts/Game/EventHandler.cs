using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventHandler : MonoBehaviour{
	public static int multiplyNum;
	public static float spawnDelayTime;

	public GameObject enemyMelee;
	public GameObject enemyRanged;
	public GameObject bullet;

	private bool found;
	private static GameObject staticMelee;
	private static GameObject staticRanged;
	private static int wave;
	private static int timeBetweenWaves;
	private static int droneCount;
	private static GameObject[] spawnPoints;

	public static EventHandler instance;

	public static EventHandler Instance{
		get{
			if(instance == null){
				instance = GameObject.FindObjectOfType<EventHandler>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

	public void init(){
		found = false;
		multiplyNum = 3;
		staticMelee = enemyMelee;
		staticRanged = enemyRanged;
		wave = 20;
		timeBetweenWaves = 30;
		droneCount = 0;
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawner");
	}
	
	public IEnumerator waitSpawn(){
		yield return new WaitForSeconds (timeBetweenWaves);
		spawnWaves ();
		
	}
	
	public IEnumerator  spawnWaves(){
		for (int i = 0; i < wave * multiplyNum; i++) {
			int index = Random.Range(0,spawnPoints.Length );
			if(Random.Range(-10,10) > 0)
			{ // Melee
				GameObject melee = staticMelee;
				Instantiate(melee, spawnPoints[index].transform.position, Quaternion.identity);
			} 
			else 
			{ // Ranged
				GameObject ranged = staticRanged;
				Instantiate(ranged,spawnPoints[index].transform.position, Quaternion.identity);
				Debug.Log("change Eventhandler.cs spawnwaves() for inclusion of ranged");
			}
			droneCount++;
			yield return new WaitForSeconds (spawnDelayTime);
		}
		wave++;
	}

	public int getDroneCount(){
		return droneCount;
	}

	public void droneKilled(){
		droneCount--;
		if (droneCount >= 0) {
			StartCoroutine(waitSpawn());
		}
	}

	public bool getFound(){
		return found;
	}

	public void setFound(bool var){
		found = var;
	}

	public int getWave()
	{
		return wave;
	}
}