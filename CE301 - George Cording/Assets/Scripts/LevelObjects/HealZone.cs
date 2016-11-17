using UnityEngine;
using System.Collections;

public class HealZone : MonoBehaviour {

	public float rate;
	
	//While a player stays within the zone their health is changed by the rate. This can be set to negative in the editor.
	void OnTriggerStay(Collider other) {
		if (other.tag == "Player"){
			try{
				other.GetComponent<CharacterControllerScript>().heal(rate);
			}
			catch(UnityException e){
				Debug.Log(e);
			}
		}
	}
}
