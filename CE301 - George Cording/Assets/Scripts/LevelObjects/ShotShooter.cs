using UnityEngine;
using System.Collections;

public class ShotShooter : MonoBehaviour {

	public GameObject projectile;
	public float delay = 10.0f;
	public float initialDelay = 0.0f;
	public float countDown;
	
	void Start()
	{
		countDown = initialDelay+Time.time;
		if(delay < 0.1f)
			delay = 0.1f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(countDown < Time.time)
		{
			countDown = delay + Time.time;
			GameObject temp = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
			//temp.GetComponent<WaveControllerScript>().AddTeam(6);
		}
	}
}
