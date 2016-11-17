using UnityEngine;
using System.Collections;

public class SelfKill : MonoBehaviour {
	/*
		Script attached to the POW prefabs that position them correctly when they are instantiated and killed off when the time is up
		This time can be adjusted as necessary.
	*/
	public float lifeTime = 1;
	// Use this for initialization
	void Start () {
		Vector3 pos = this.gameObject.GetComponent<Transform>().position;
		pos.z -= 1;
		//pos.x += 0.5f;
		pos.y += 0.5f;
		this.gameObject.GetComponent<Transform>().position = pos;
		Destroy(this.gameObject,lifeTime);
	}
}
