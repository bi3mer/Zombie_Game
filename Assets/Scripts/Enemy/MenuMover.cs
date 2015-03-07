using UnityEngine;
using System.Collections;

public class MenuMover : MonoBehaviour {

	public Transform[] targets;
	public int pointNum = 0;
    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targets[pointNum].position, step);
		if(gameObject.transform.position == targets[pointNum].transform.position){
			if(pointNum == targets.Length-1){
				print("num");
				pointNum = 0;
			}else{
				pointNum++;
			}
		}
	}
}
