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
}
