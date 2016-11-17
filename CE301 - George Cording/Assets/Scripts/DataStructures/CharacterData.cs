using UnityEngine;
using System.Collections;

public class CharacterData
{
	public bool inPlay = false;
	public int playerNumber = 0;
	public int apperance = 0;
	public int mainType = 0;
	public int secondType = 0;
	public int thirdType = 0;
	public int team = 0;
	
	public string GetPNumber()
	{
		return "P"+(1+playerNumber)+"_";
	}
}
