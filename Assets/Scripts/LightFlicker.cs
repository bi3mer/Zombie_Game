using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Light))]

public class LightFlicker : MonoBehaviour {
	
	public float interval = 0.7F;
	private Color col;
	private float flick = 0.0F;

	
	void Start () {
		col = light.color;

	}
	
		void Update () {
			if (Random.value >= interval) {
				flick = Random.value;
				light.color = new Color(col.r * flick, col.g * flick, col.b * flick);

		}
	}
}