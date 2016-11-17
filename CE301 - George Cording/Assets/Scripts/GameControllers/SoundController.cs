using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public bool MUSIC = true;
	public float SPEED = 0.5f;
	public float level = 0.0f;
	public float targetVolume = 0.0f;
	public bool fadeOut = false;
	public float waitUntil = 0.0f;
	AudioSource aS;
	
	// Use this for initialization
	void Start () 
	{
		aS = GetComponent<AudioSource>();
		if(MUSIC)
			targetVolume = PauseMenuScript.PS.musicLevel/100.0f;
		else
			targetVolume = PauseMenuScript.PS.soundLevel/100.0f;
		aS.volume = level;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Re-checking of the target volume is performed roughly once a second to limit calls to PS
		if (waitUntil < Time.time && !fadeOut)
		{
			if(MUSIC)
				targetVolume = PauseMenuScript.PS.musicLevel/100.0f;
			else
				targetVolume = PauseMenuScript.PS.soundLevel/100.0f;
			waitUntil = Time.time + 1.0f;
		}
		
		if(level < targetVolume)
		{
			if(targetVolume - level < SPEED)
				level = targetVolume;
			else
				level += SPEED;
			aS.volume = level;
		}
		else if(level > targetVolume)
		{
			if(level - targetVolume < SPEED)
				level = targetVolume;
			else
				level -= SPEED;
			aS.volume = level;
		}
	}
	
	public void FadeOut()
	{
		fadeOut = true;
		targetVolume = 0.0f;
	}
}
