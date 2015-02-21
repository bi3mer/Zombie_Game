using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {
	/*
	 * This will hold all variables for our use, such as player, spawn time, number of zombies on the map, etc.
	 * I imagine more will be added as we go.
     */
	public int zombieCount = 0;
	public int timeBetweenWaves = 30;
	public int wave = 1;
	public GameObject playerGameObj;
	
}
