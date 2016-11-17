using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
	
	public CharacterControllerScript[] playerScript;
	public GameObject comboList;
	public HealthBarController[] healthBars;
	public CharacterSpawner[] spawnPoint;
	public GameObject[] CHARACTER;
	
	void Awake()
	{
		comboList = GameObject.FindGameObjectWithTag("ComboManager");
		if(comboList == null)
			Debug.Log("Forgot the Combo List");
		ComboManager.Instance.CreateList(comboList);
	}
	
	void Start()
	{
		comboList = GameObject.FindGameObjectWithTag("ComboManager");
		playerScript = new CharacterControllerScript[4];
		CharacterData[] c = PlayerPrefferences.Instance.cData;
		CharacterSpawner[] updatedList = new CharacterSpawner[PlayerPrefferences.Instance.players];
		int track = 0;
		for (int i = 0; i< 4; i++)
		{
			if (c[i].inPlay)
			{
				spawnPoint[i].p = CHARACTER[c[i].apperance];
				if(i%2 == 0)
					spawnPoint[i].facingRight = true;
				
				spawnPoint[i].team = c[i].team;
				spawnPoint[i].pNumber = c[i].GetPNumber();
				spawnPoint[i].SetTypes(c[i].mainType,c[i].secondType,c[i].thirdType);
				spawnPoint[i].hB = healthBars[i];
				playerScript[i] = spawnPoint[i].Spawn();
				updatedList[track] = spawnPoint[i];
				track++;
			}
			else
			{
				healthBars[i].gameObject.SetActive(false);
			}
		}
		spawnPoint = updatedList;
		StartCoroutine(CheckForDead());
		PlayerPrefferences.Terminate();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
	
	IEnumerator CheckForDead()
	{
		yield return new WaitForSeconds(0.5f);
		int firstTeamFound = -1;
		int livingPlayers = 0;
		bool multipleTeams = false;
		foreach(CharacterControllerScript c in playerScript)
		{
			if(c != null)
			{
				if(c.ALIVE)
				{
					livingPlayers ++;
					if(firstTeamFound == -1)
					{
						firstTeamFound = c.TEAM;
					}
					else if(firstTeamFound != c.TEAM)
					{
						multipleTeams = true;
					}
				}
				else
				{
					c.gameObject.SetActive(false);
					Destroy(c.gameObject);
				}
			}
		}
		if(!multipleTeams)
		{
			PauseMenuScript.PS.GameOver(firstTeamFound);
		}
		else
		{
			StartCoroutine(CheckForDead());
		}
	}
	
	public Vector3 GetCenterPoint()
	{
		
		Vector3 toReturn = new Vector3(0,0,0);
		int count = 0;
		foreach (CharacterSpawner cs in spawnPoint)
		{
			GameObject o = cs.GetPlayer();
			if(o != null)
			{
				toReturn = toReturn + o.transform.position;
				count++;
			}
		}
		toReturn = toReturn / count;
		return toReturn;
	}
}



















