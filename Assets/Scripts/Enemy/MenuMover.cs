using UnityEngine;
using System.Collections;

public class MenuMover : MonoBehaviour {
	public int pointNum = 0;
    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, EventHandler.Instance.spawnPoints[pointNum].transform.position, step);
		if(gameObject.transform.position == EventHandler.Instance.spawnPoints[pointNum].transform.position){
			if(pointNum == EventHandler.Instance.spawnPoints.Length-1)
			{
				pointNum = 0;
			}
			else
			{
				pointNum++;
			}
		}
	}
}
