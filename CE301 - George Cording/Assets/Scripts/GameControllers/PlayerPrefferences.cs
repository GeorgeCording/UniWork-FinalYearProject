using UnityEngine;
using System.Collections;

public class PlayerPrefferences : MonoBehaviour 
{
	public CharacterData[] cData;
	private static PlayerPrefferences _instance;
	public int players = 0;

	public static PlayerPrefferences Instance
	{
		get{
			if(_instance == null)
			{
				GameObject go = new GameObject("PlayerPrefferences");
				go.tag = "Prefferences";
				go.AddComponent<PlayerPrefferences>();
				DontDestroyOnLoad(go);
			}
			return _instance;
		}
	}
	
	void Awake()
	{
		_instance = this;
	}
	
	void Start()
	{
		cData = new CharacterData[4];
		for (int i = 0; i < 4; i++){
			cData[i] = new CharacterData();
		}
	}
	
	static public void SetPlayer(int player, int t1, int t2, int t3, int apperance, int team)
	{
		if(player < 0 || player > 3)
			return;
		Instance.cData[player].playerNumber = player;
		Instance.cData[player].inPlay = true;
		Instance.cData[player].mainType = t1;
		Instance.cData[player].secondType = t2;
		Instance.cData[player].thirdType = t3;
		Instance.cData[player].apperance = apperance;
		Instance.cData[player].team = team;
		Instance.players++;
	}
	
	public static PlayerPrefferences WakeUp()
	{
		return Instance;
	}
	
	public static void Terminate()
	{
		Destroy(Instance.gameObject);
		_instance = null;
	}
}
