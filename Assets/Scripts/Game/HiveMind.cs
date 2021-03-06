﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class HiveMind : MonoBehaviour {
	/*
	 * This class is in charge of keeping track of all the drone game objects
	 * and having them perform actions. It is THE HIVEMIND!!!!
	 */

	private ArrayList drones = new ArrayList();

	public static HiveMind instance;
	
	public static HiveMind Instance{
		get{
			if(instance == null){
				instance = GameObject.FindObjectOfType<HiveMind>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}
	
	public int getSize()
	{
		return this.drones.Count;
	}

	public void addDrone(DroneAbstract drone){
		this.drones.Add(drone);
	}

	public void removeDrone(DroneAbstract drone){
		this.drones.Remove(drone);
		if(getSize()<= 0)
		{
			StartCoroutine(EventHandler.Instance.waitSpawn());
		}
	}

	public void moveDrones()
	{
		if(this.drones.Count < 10)
		{
			foreach(DroneAbstract drone in this.drones)
			{
				drone.moveTowardsPlayer();
			}
			return;
		}
		bool found = false;
		foreach(DroneAbstract drone in this.drones)
		{
			if(drone.search())
			{
				found = true;
			}
		}
		if(found)
		{
			foreach(DroneAbstract drone in this.drones)
			{
				drone.moveTowardsPlayer();
			}
		}
		else
		{

			foreach(DroneAbstract drone in this.drones)
			{
				//print("must implement move in random direction");
				drone.moveRandomDirection();
			}
		}
	}

	public void destroyAll()
	{
		for (int i =0; i < this.drones.Count; i++) 
		{
			DroneAbstract drn = drones[i] as DroneAbstract;
			drn.destroySelf();
		}
	}

}
