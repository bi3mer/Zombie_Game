using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventHandler : MonoBehaviour{
	public static int multiplyNum;
	public float spawnDelayTime;

	public GameObject enemyMelee;
	public GameObject enemyRanged;
	public GameObject enemyPush;
	public GameObject bullet;
	public GameObject tinyExplosion;
	public GameObject mushroomExplosion;
	public GameObject bossExplosion;
	public GameObject Center;
	public GameObject DeathPosition;

	private bool found;
	private static GameObject staticMelee;
	private static GameObject staticRanged;
	private static GameObject staticPush;
	private static int wave;
	private static int timeBetweenWaves;
	private static int droneCount;
	private static GameObject[] spawnPoints;


	public static EventHandler instance;

	public static EventHandler Instance{
		get{
			if(instance == null)
			{
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
		staticPush = enemyPush;
		wave = 1;
		timeBetweenWaves = 15;
		droneCount = 0;
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawner");
	}

	public GameObject[] getSpawnPoints()
	{
		return spawnPoints;
	}

	public IEnumerator waitSpawn(){
		yield return new WaitForSeconds (timeBetweenWaves);
		StartCoroutine(spawnWaves ());
		
	}
	
	public IEnumerator  spawnWaves(){
		print ("spawn: " + this.getWave ());
		for (int i = 0; i < wave * multiplyNum; i++) {
			int index = Random.Range(0,spawnPoints.Length );
			int rand = Random.Range(-10,10);
			if(rand > 4)
			{ // Melee
				GameObject melee = staticMelee;
				Instantiate(melee, spawnPoints[index].transform.position, Quaternion.identity);
			} 
			else if(rand > -3)
			{
				GameObject push = staticRanged;
				Instantiate (push,spawnPoints[index].transform.position,Quaternion.identity);
			}
			else
			{ // Ranged
				GameObject ranged = staticPush;
				Instantiate(ranged,spawnPoints[index].transform.position, Quaternion.identity);
				Debug.Log("change Eventhandler.cs spawnwaves() for inclusion of ranged");
			}
			droneCount++;
			yield return new WaitForSeconds (spawnDelayTime);
		}
		wave++;
		
		// update GUI!!!!
		//Player.Instance.finishWave(this.wave);
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
	
	public void gameOverExplosion()
	{
		for(int i = 0 ; i < this.getSpawnPoints().Length; i++)
		{
			// reduce spawnpoint height by 8 for explosion
			Vector3 explosionSpawnPoint = new Vector3(this.getSpawnPoints()[i].transform.position.x,
			                                          this.getSpawnPoints()[i].transform.position.y - 100, 
			                                          this.getSpawnPoints()[i].transform.position.z);
			// explode!
			Instantiate(this.mushroomExplosion,explosionSpawnPoint,Quaternion.identity);

		}
	}
}