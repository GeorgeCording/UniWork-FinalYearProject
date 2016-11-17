using UnityEngine;
using System.Collections;

public class SpawnMenuCharacterScript : MonoBehaviour 
{
	private int curentValue;
	public GameObject[] model;
	public GameObject currentModel;
	
	public void ShowCharacter(int i)
	{
		if(curentValue == i)
			return;
		curentValue = i;
		if(currentModel != null)
			Destroy(currentModel);
		currentModel = Instantiate(model[curentValue],transform.position,transform.rotation) as GameObject;
	}
	
	void OnEnable()
	{
		currentModel = Instantiate(model[curentValue],transform.position,transform.rotation) as GameObject;
	}
	
	void OnDisable()
	{
		Destroy(currentModel);
	}
}
