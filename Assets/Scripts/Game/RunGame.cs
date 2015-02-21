using UnityEngine;
using System.Collections;

public class RunGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventHandler.Instance.init ();
		StartCoroutine (EventHandler.Instance.spawnWaves ());
	}

	void Update()
	{
		print ("hereasdfaf");
		HiveMind.Instance.moveDrones();
	}
}
