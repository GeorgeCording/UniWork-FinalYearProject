using UnityEngine;
using System.Collections;

public class PushZone : MonoBehaviour {

	public float distance = 0.2f;

	void OnTriggerStay(Collider other) 
	{
		if (other.tag == "Projectile")
		{
			Destroy(other.gameObject);
		}
		//other.attachedRigidbody.AddForce(Vector3.right*distance);
		Vector3 temp = other.gameObject.transform.position;
		temp = new Vector3 (temp.x+distance,temp.y,temp.z);
		other.gameObject.transform.position = temp;
	}
}
