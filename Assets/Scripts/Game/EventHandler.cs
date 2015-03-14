using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventHandler : MonoBehaviour{
	public static int multiplyNum;
	public float spawnDelayTime;

	/*
	public GameObject enemyMelee;
	public GameObject enemyRanged;
	public GameObject enemyPush;
	*/

	public GameObject[] enemies; // used to spawn enmies
	public GameObject[] bosses;  // used to spawn enemies

	public GameObject bullet;
	public GameObject tinyExplosion;
	public GameObject mushroomExplosion;
	public GameObject bossExplosion;
	public GameObject Center;
	public GameObject DeathPosition;

	private bool found;
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
		multiplyNum = 3;;
		wave = 5; // bosswave!!
		timeBetweenWaves = 15;
		droneCount = 0;

		/***********************************/
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawner");
		/***********************************/
	}

	public GameObject[] getSpawnPoints()
	{
		return spawnPoints;
	}

	public IEnumerator waitSpawn(){
		yield return new WaitForSeconds (timeBetweenWaves);
		StartCoroutine(spawnWaves ());
		
	}
	
	public IEnumerator  spawnWaves()
	{
		wave++;
		if(wave % 5 == 0) // spawn bosses every 10 waves
		{
			this.spawnBoss();
			yield return new WaitForSeconds(0);
		}
		else
		{
			StartCoroutine(this.spawnEnemies());
		}
	}

	public void spawnBoss()
	{
		int spawnLocation = Random.Range(0,spawnPoints.Length);
		int bossType = Random.Range(0,this.bosses.Length);
		int index = Random.Range (0, this.getSpawnPoints ().Length);

		Instantiate(this.bosses[bossType],spawnPoints[index].transform.position,Quaternion.identity);
	}

	public IEnumerator spawnEnemies()
	{
		for (int i = 0; i < wave * multiplyNum; i++) 
		{
			int spawnLocation = Random.Range(0,spawnPoints.Length);
			int spawnType 	  = Random.Range(0,enemies.Length);
			int index 		  = Random.Range (0,getSpawnPoints().Length);

			print ("i: " + i  + " " + wave*multiplyNum);

			Instantiate(this.enemies[spawnType], spawnPoints[index].transform.position, Quaternion.identity);

			droneCount++; // increase drone count
			yield return new WaitForSeconds (spawnDelayTime); // pause between each spawn, to decrease lag
		}
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

			// Destroy's ufo from blast
			Destroy(this.getSpawnPoints()[i]);
		}
	}
}