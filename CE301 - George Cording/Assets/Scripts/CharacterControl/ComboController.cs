using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	Each character has an attached ComboController which has functions called by either a character controller or an AI controller.
	This script handles the legality of attack actions and the instantiation of projectiles as a result of the input.
	The instantiation of projectiles are determined by passage through a state tree of attacks that is created and held in the ComboManager.
*/

public class ComboController : MonoBehaviour 
{
	//variables for deciding type/properties of shots.
	private int mainType = 0;
	private int secondType = 0;
	private int thirdType = 0;
	private int team = 0;
	
	//Variable that tracks which state the combo is in.
	private Combo currentState;
	
	//variables for determining where to spawn shots.
	public bool facingRight = true;
	
	//variables for time limit.
	public float comboTimeLimit = 0.5f;
	public bool startCounting = true;
	public bool moveTimeRunning = false;
	private IEnumerator timeOut;
	
	//Called by Unity during Initialization.
	void start()
	{
		timeOut = MoveTime();
		currentState = ComboManager.Instance.GetFirstState();
	}
	
	//Called by CharacterControllerScript to assign variables.
	public void SetTypes(int t1, int t2, int t3, int team)
	{
		mainType = t1;
		secondType = t2;
		thirdType = t3;
		this.team = team;
	}
	
	//function that counts down the time allowed between attacks before a combo is ended.
	IEnumerator MoveTime()
	{
		moveTimeRunning = true;
		while (startCounting)
		{
			yield return new WaitForFixedUpdate();
		}
		startCounting = false;
		yield return new WaitForSeconds(comboTimeLimit);
		currentState = ComboManager.Instance.GetFirstState();
		moveTimeRunning = false;
	}
	
	//function called by the Character controller script to perform a kick and produce the shots for it.
	public void Kick()
	{
		if(currentState == null)
			currentState = ComboManager.Instance.GetFirstState();
		currentState = currentState.GetKick();
		
		SpawnShots();
	}
	
	//function called by the Character controller script to perform a jab and produce the shots for it.
	public void Punch()
	{
		if(currentState == null)
			currentState = ComboManager.Instance.GetFirstState();
		currentState = currentState.GetJab();
		
		SpawnShots();
	}
	
	//function called by the Character controller script to perform a jump and produce the shots for it.
	public void Jump()
	{
		if(currentState == null)
			currentState = ComboManager.Instance.GetFirstState();
		currentState = currentState.GetJump();
		
		SpawnShots();
	}
	
	/*
		function called by each of the Jump, Punch and Kick Functions.
		If the time out coroutine is already running this will be stopped and reset.
		For each shot in the array of shots stored in the saved combo node:
			taking into account if the character is facingRight adjust the offset position
			create a shot of the correct type.
			
	*/
	void SpawnShots()
	{
		if(moveTimeRunning)
			StopCoroutine(timeOut);
		timeOut = MoveTime();
		StartCoroutine(timeOut);
		foreach(Shot s in currentState.GetShots())
		{
			GameObject temp;
			Vector3 offset = s.POSITION+gameObject.transform.position;
			if(!facingRight)
				offset = new Vector3(offset.x - 2* s.POSITION.x, offset.y, offset.z);
			if(s.TYPE == 0){
				temp = Instantiate(ComboManager.Instance.oList.SHOTLIST[GetShotType(s)],offset, Quaternion.Euler(s.ROTATION.x,facingRight? s.ROTATION.y:s.ROTATION.y+180,s.ROTATION.z)) as GameObject;
				temp.GetComponent<ShotController>().AddTeam(team);
			}else if(s.TYPE == 1)
			{
				temp = Instantiate(ComboManager.Instance.wList.SHOTLIST[GetShotType(s)],offset, Quaternion.Euler(s.ROTATION.x,facingRight? s.ROTATION.y:s.ROTATION.y+180,s.ROTATION.z)) as GameObject;
				temp.GetComponent<WaveControllerScript>().AddTeam(team);
			}
		}
	}
	
	//Sets if the character is facing right, as dictated by the Character controller script
	public void SetFace(bool dir){
		facingRight = dir;
	}
	
	public void CountTimeOut()
	{
		startCounting = false;
	}
	
	int GetShotType(Shot s){
		switch(s.GRADE)
		{
			case 1:
			return secondType;
			
			case 2:
			return thirdType;
			
			default:
			return mainType;
		}
	}
}