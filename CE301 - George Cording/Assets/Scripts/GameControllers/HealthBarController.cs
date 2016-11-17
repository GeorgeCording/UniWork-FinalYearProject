using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarController : MonoBehaviour {

	float health = 1000;
	float maxHealth = 1000;
	public bool left;
	public RectTransform hBar;
	public Image image;

	//changes the value of the health and adjusts the visual image displayed.
	public void UpdateHealth(float val){
		health = val;
		if (health > maxHealth)
			health = maxHealth;
		
		Vector3 position;
		if(left)
			position = new Vector3 ((maxHealth-health)/(maxHealth/100),0);
		else
			position = new Vector3 (((maxHealth-health)/(maxHealth/100))-100,0);

		image.GetComponent<RectTransform> ().localPosition = position;
	}
	
	//simple function to set the value of the maxHealth and health to a value.
	public void setMaxHealth(float val){
		maxHealth = val;
		health = val;
	}
	
	//Simple function to set the value of the left boolean.
	public void setLeft(bool val){
		left = val;
	}
	
}
