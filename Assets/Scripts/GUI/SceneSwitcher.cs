using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {
	
	public string scene = "gameScene";
	
	private bool CountBack = false;
	private float Speed = 0.01f;
	private float t = 0f;
	private float final_t;
	private bool MouseOver = false;
	
	public void StartGame(string s)
	{
		if (s == null)
			s = scene;
		Application.LoadLevel(s);
	}
	
	void Update () {
        if (!CountBack) {
            t += Speed;
            if (t >= 1f) CountBack = true;
        } else {
            t -= Speed;
            if (t <= Speed) CountBack = false;
        }
        final_t = t;
        print (CountBack + "("+final_t+")");
        renderer.material.color = Color.Lerp(Color.red, Color.green, final_t);
    }
		
	public void QuitGame(){
		Application.Quit();
	}
	
	public void Toggle(GameObject window){
		print("JHUN1");
		if(window.activeSelf == false)
			window.SetActive(true);
		else if(window.activeSelf == true)
			window.SetActive(false);
	}
}
