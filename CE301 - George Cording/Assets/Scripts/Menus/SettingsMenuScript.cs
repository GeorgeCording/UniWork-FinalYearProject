using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenuScript : MonoBehaviour {

	
	public int soundLevel = 50;
	public int musicLevel = 50;
	public Slider sound;
	public Slider music;

	// Use this for initialization
	void Start () {
		soundLevel = PauseMenuScript.PS.soundLevel;
		musicLevel = PauseMenuScript.PS.musicLevel;
		sound.value = soundLevel/100.0f;
		music.value = musicLevel/100.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool changed = false;
		if(soundLevel != (int)(sound.value*100.0f))
		{
			soundLevel = (int)(sound.value*100.0f);
			changed = true;
		}
		if(musicLevel != (int)(music.value*100.0f))
		{
			musicLevel = (int)(music.value*100.0f);
			changed = true;
		}
		if(changed)
		{
			PauseMenuScript.UpdateSound(soundLevel, musicLevel);
		}
	}
}
