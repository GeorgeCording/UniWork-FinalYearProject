using UnityEngine;
using System.Collections;

public class SplashScreenScript : MonoBehaviour {


	public float DELAY = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DELAY -= Time.deltaTime;
		if (DELAY > 0 && !Input.GetKeyDown("escape")) {
			return;
		}
		Application.LoadLevel(1);
	}
}
