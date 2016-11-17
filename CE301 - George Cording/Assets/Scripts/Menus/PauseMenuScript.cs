using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuScript : MonoBehaviour 
{
	
	public static PauseMenuScript PS;
	public bool visible = false;
	public GameObject ui1;
	public GameObject ui2;
	public GameObject endScreen;
	public Text gameOverText;
	public int soundLevel = 50;
	public int musicLevel = 50;


	void Awake()
	{
		endScreen.SetActive(false);
		if (PS == null) {
			PS = this;
			DontDestroyOnLoad(this);
		} else if (PS != this) {
			Destroy(gameObject);
		}
		PS.ui1.SetActive(visible);
	}
	
	void Update()
	{
		if ((Input.GetKeyDown (KeyCode.Escape)) && Application.loadedLevel > 1) {
			visible = !visible;
			ui2.setActive(false);
			ui1.SetActive(visible);
		}
		if(!visible)
			Time.timeScale = 1.0f;
		else
			Time.timeScale = 0.0f;
		
	}

	public void QuitGame()
	{
		Application.Quit();
	}
	
	public void MainMenu()
	{
		endScreen.SetActive(false);
		FaderScript fs = GameObject.FindGameObjectWithTag("FaderScript").GetComponent<FaderScript>();
		StartCoroutine(fs.BeginLevelFade(1));
		visible = false;
		ui1.SetActive(visible);
		Time.timeScale = 1.0f;
	}
	
	public void Resume(){
		Debug.Log("Resume");
		visible = false;
		ui1.SetActive(visible);
	}
	
	public void toggleMenu()
	{
		ui1.SetActive(!ui1.active);
		ui2.SetActive(!ui2.active);
	}
	
	public void GameOver(int winner)
	{
		gameOverText.text = "Game Over, Team: " + winner + " Won!";
		endScreen.SetActive(true);
	}
	
	static public void UpdateSound(int s, int m)
	{
		PS.soundLevel = s;
		PS.musicLevel = m;
	}
}
