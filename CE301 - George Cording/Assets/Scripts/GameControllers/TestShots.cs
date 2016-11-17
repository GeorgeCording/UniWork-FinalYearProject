using UnityEngine;
using System.Collections;

public class TestShots : MonoBehaviour 
{

	public bool shoot = false;
	public Shot[] shots;

	// Use this for initialization
	void Start ()
	{
		shots = gameObject.GetComponents<Shot>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(shoot)
		{
			foreach(Shot s in shots)
			{
				GameObject temp;
				temp = Instantiate(ComboManager.Instance.oList.SHOTLIST[s.GRADE],s.POSITION+gameObject.transform.position, Quaternion.Euler(s.ROTATION.x,s.ROTATION.y,s.ROTATION.z)) as GameObject;
				temp.GetComponent<ShotController>().AddTeam(0);
			}
			shoot = false;
		}
	}
}
