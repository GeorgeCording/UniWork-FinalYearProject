using UnityEngine;
using System.Collections;

public class ComboManager : MonoBehaviour 
{
	
	private static ComboManager _instance;
	public static Combo firstState;
	public ObjectList oList;
	public ObjectList wList;
	
	public static ComboManager Instance
	{
		get{
			if(_instance == null)
			{
				GameObject go = new GameObject("ComboManager");
				go.AddComponent<ComboManager>();
				
			}
		return _instance;
		}
	}
	
	void Awake()
	{
		_instance = this;
	}
	
	//
	public void CreateList(GameObject o)
	{
		if(oList != null)
			return;
		firstState = new Combo();
		firstState.BuildTree(o);
	}
	
	//called by the combo controller to get the root node of the data tree
	public Combo GetFirstState()
	{
		return firstState;
	}
}







