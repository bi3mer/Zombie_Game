using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {

	public Material startcolor;
	public Material hovercolor;
 
	string type;

	void OnMouseEnter() {
		if (Vector3.Distance(Camera.main.gameObject.transform.position, gameObject.transform.position) < 10f) {
			startcolor = renderer.material;
			renderer.material = hovercolor;
			print("1 Entered!");
		}
	}

	void OnMouseExit() {
		renderer.material = startcolor;
		print("0 Entered!");
	}
	
	void OnMouseDown() {
		
	}
}
