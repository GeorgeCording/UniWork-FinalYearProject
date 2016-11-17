using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectionTypesScript : MonoBehaviour 
{
	public Text txt;
	public int num = 0;
	public Image bg;
	public GameObject UP;
	public GameObject DOWN;
	
	void Start()
	{
		txt.text = GetCharacter(num);
		bg.color = GetBGColour(num);
	}
	
	public void Up()
	{
		num++;
		if (num == 6)
			num = 0;
		txt.text = GetCharacter(num);
		bg.color = GetBGColour(num);
	}
	
	public void Down()
	{
		num--;
		if (num == -1)
			num = 5;
		txt.text = GetCharacter(num);
		bg.color = GetBGColour(num);
	}
	
	static string GetCharacter(int n)
	{
		switch(n)
		{
			case 1:
			return "F";
			case 2:
			return "A";
			case 3:
			return "E";
			case 4:
			return "L";
			case 5:
			return "W";
			default:
			return "N";
		}
	}
	
	static Color GetBGColour(int n)
	{
		switch(n)
		{
			case 0:
			return new Color(1,1,0.741f);
			case 1:
			return new Color(1,0.741f,0.741f);
			case 2:
			return new Color(0.741f,1,1);
			case 3:
			return new Color(0.741f,1,0.741f);
			case 4:
			return new Color(1,0.741f,1);
			default:
			return new Color(0.741f,0.741f,1);
		}
	}
	
	public void Show()
	{
		UP.SetActive(true);
		DOWN.SetActive(true);
	}
	
	public void Hide()
	{
		UP.SetActive(false);
		DOWN.SetActive(false);
	}
}
