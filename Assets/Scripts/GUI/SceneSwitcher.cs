using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {
	
	public string scene = "gameScene";
	
	public void StartGame(string s)
	{
		if (s == null)
			s = scene;
		Application.LoadLevel(s);
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
