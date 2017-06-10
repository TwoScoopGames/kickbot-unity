using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitThenChangeScene : MonoBehaviour {

	public string sceneName;
	public float secondsToWait;

	void Start() {
		Debug.Log ("I ran");
		Invoke ("LoadScene", secondsToWait);
	}

	public void LoadScene() {
		Debug.Log ("I am loadScene");
		Application.LoadLevel(sceneName);
	}
}
