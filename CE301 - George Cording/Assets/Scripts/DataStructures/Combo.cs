using UnityEngine;
using System.Collections;

public class Combo {
	
	/*
		type denotes the type of attack as an integer, Jab 0, Kick 1, Jump 2. A value of 3 denotes it is none of them, For use at the top level;
		shots is a list of shots that are spawned when this state is reached.
		children is a list of Combo states that follow on from the current one.
	*/
	
	public int type = 0;
	public Shot[] shots;
	private Combo[] children = {null, null, null};
	
	//Simple call to get children of a certain type (Jab).
	public Combo GetJab()
	{
		if(children[0] == null)
			return ComboManager.Instance.GetFirstState().GetJab();
		return children[0];
	}
	
	//Simple call to get children of a certain type (Kick).
	public Combo GetKick()
	{
		if(children[1] == null)
			return ComboManager.Instance.GetFirstState().GetKick();
		return children[1];
	}
	
	//Simple call to get children of a certain type (Jump).
	public Combo GetJump()
	{
		if(children[2] == null)
			return ComboManager.Instance.GetFirstState().GetJump();
		return children[2];
	}
	
	//Simple call to get list of shots fired.
	public Shot[] GetShots()
	{
		return shots;
	}
	
	public void BuildTree(GameObject o)
	{
		shots = o.GetComponents<Shot>();
		switch(o.tag){
			case "JabSpawn":
			type = 0;
			break;
			
			case "KickSpawn":
			type = 1;
			break;
			
			case "JumpSpawn":
			type = 2;
			break;
			
			default:
			type = 3;
			break;
		}
		//Counts how many children there are and for each one attempts to build a combo and call the BuildTree function in it.
		int childrenCount = o.transform.childCount;
		for(int i = 0; i < childrenCount; i++)
		{
			Transform t = o.transform.GetChild(i);
			switch(t.gameObject.tag)
			{
				case "JabSpawn":
				
				children[0] = new Combo();
				children[0].BuildTree(t.gameObject);
				break;
				
				case "KickSpawn":
				
				children[1] = new Combo();
				children[1].BuildTree(t.gameObject);
				break;
				
				case "JumpSpawn":
				
				children[2] = new Combo();
				children[2].BuildTree(t.gameObject);
				break;
			}
		}
	}
}







