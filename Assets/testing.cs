using UnityEngine;
using System.Collections;

public class testing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print ("position: " + this.transform.position.x + "," + this.transform.position.y + ", " + this.transform.position.z);
		print ("local   : " + this.transform.localPosition.x + "," + this.transform.localPosition.y + ", " + this.transform.localPosition.z);
	}
}
