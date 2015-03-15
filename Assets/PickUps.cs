using UnityEngine;
using System.Collections;
using UnityEngine.UI; 	

public class PickUps : MonoBehaviour {

	public Material startcolor;
	public Material hovercolor;

	MeshRenderer cRenderer;
	private GameObject manager;
	public GameObject player;
	Player playerVars;
	CollectorManager ColMan;

	public Text text;
	
	private Vector3 pos1;
	private Vector3 pos2;
	private Vector3 offset;
	private Vector3 moveTo;
	private float moveSpeed = 0.01f;
	
	public string inText = "Text Goes Here";

	bool inRange = false;

	// Use this for initialization
	void Start () {
		playerVars = player.GetComponent<Player>();		
		cRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
		manager = this.transform.parent.gameObject;
		ColMan = manager.GetComponentInChildren<CollectorManager>();
		offset = Vector3.down;
		pos1 = transform.position;
		pos2 = transform.position + offset;
	}
	
	void Update () {
	
        transform.Rotate (0,50*Time.deltaTime,0);
		if(transform.position == pos1) {
			moveTo = pos2;
		}
		
		if(transform.position == pos2) {
			moveTo = pos1;
		}
		
		transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed);
    }
	
	void OnMouseEnter() {
		if (Vector3.Distance(Camera.main.gameObject.transform.position, gameObject.transform.position) < 10f) {
			cRenderer.material = hovercolor;
			inRange = true;
		}
	}
	
	void OnMouseOver(){
		if (Input.GetAxis("XFire1") == -1 || Input.GetAxis("XFire1") == 1)
			this.collect();
	}
	
	void OnMouseExit() {
		cRenderer.material = startcolor;
	}
	
	void OnMouseDown(){
		this.collect();
	}
	
	void collect(){
		if (inRange){
			playerVars.setText(inText);
			playerVars.activateColWin();
			ColMan.decrementCount();
			gameObject.SetActive(false);
		}
	}
}
