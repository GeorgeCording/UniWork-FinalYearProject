using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour
{
	public Rigidbody cubes;
	public int type;
	public GameObject POW;
	public int team = 0;
	//static Quaternion powRotation = Quaternion.Euler(0.0f,0.0f,0.0f);

	// Use this for initialization
	//Gets an easy refference to it's own transform 
	void Start ()
	{
		Transform trans = GetComponent<Transform> ();
		trans.rotation.eulerAngles.Set(3.0f,0f,0f);
		cubes.AddRelativeForce(Vector3.right*400);
		Destroy (cubes.gameObject, 4);
	}

	//When something enters the shot (even if it is the shot that's moving).
	void OnTriggerEnter(Collider other)
	{
		//The shot checks to see if the object is a player.
		if (other.tag == "Player")
		{
			if(other.GetComponent<CharacterControllerScript>().TEAM != team)
			{
				try
				{
					//If the object is a player it will call the player's attached charactercontrollerscript's hit function, providing the type.
					other.GetComponent<CharacterControllerScript>().hit(type);
					Instantiate(POW,this.gameObject.GetComponent<Rigidbody>().position,this.gameObject.GetComponent<Rigidbody>().rotation);
				}
				catch(UnityException e)
				{
					Debug.Log(e);
				}
				//after the shot has hit the player it will destroy itself.
				Destroy (this.gameObject);
			}
		}else if(other.tag == "Wall")
		{
			//if the shot hits a wall, an object it can't pass through it will destroy itself.
			Destroy(cubes.gameObject);
		}
	}
	
	public void AddTeam(int team)
	{
		this.team = team;
	}
}























