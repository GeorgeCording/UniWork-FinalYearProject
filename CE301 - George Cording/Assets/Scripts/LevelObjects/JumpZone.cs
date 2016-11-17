using UnityEngine;
using System.Collections;

public class JumpZone : MonoBehaviour {

 	public float force = 10;
	
	//When a character enters the zone their jumpSpeed is increased by the force.
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Player"){
			try
			{
				CharacterControllerScript cc = other.GetComponent<CharacterControllerScript>();
				cc.jumpSpeed += force;
			}
			catch(UnityException e)
			{
				Debug.Log(e);
			}
		}
	}
	
	//When the player leaves the zone thier jumpSpeed is decreased back to normal.
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			try
			{
				CharacterControllerScript cc = other.GetComponent<CharacterControllerScript>();
				cc.jumpSpeed -= force;
			}
			catch(UnityException e)
			{
				Debug.Log(e);
			}
		}
	}
}
