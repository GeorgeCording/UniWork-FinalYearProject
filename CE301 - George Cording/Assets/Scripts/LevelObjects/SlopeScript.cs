using UnityEngine;
using System.Collections;

public class SlopeScript : MonoBehaviour 
{
	Vector3 downSpeed;
	
	void Start()
	{
		downSpeed = new Vector3 (0,-10,0);
	}
	
	void OnTriggerStay(Collider other) 
	{
		if (other.tag == "Player")
		{
			CharacterControllerScript c = other.GetComponent<CharacterControllerScript>();
			if(!c.jumping&&!c.cc.isGrounded)
			{
				c.cc.Move (downSpeed*Time.deltaTime);
			}
		}
	}
}
