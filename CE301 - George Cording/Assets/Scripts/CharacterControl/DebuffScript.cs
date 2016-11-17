using UnityEngine;
using System.Collections;

public class DebuffScript : MonoBehaviour 
{
	int count = 0;
	
	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
	
	void Update()
	{
		if(transform.rotation != Quaternion.Euler(0,180,0))
		{
			transform.eulerAngles = new Vector3(0,180,0);
		}
	}
	
	public void show()
	{
		if(count == 0)
			gameObject.SetActive(true);
		count++;
	}
	
	public void hide()
	{
		count --;
		if(count == 0)
			gameObject.SetActive(false);
		
	}
}
