using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	private int activeMenu = 0;
	public GameObject[] menuList;
	public ScrollBarScript[] scrollBars;
	public Button[] buttons;
	public float INPUTDELAY = 0.1f;
	int selectedMenu = 0;
	float delayTime = 0.0f;
	Color unSelectedColour;
	private bool loadingLevel = false;
	
	void Start()
	{
		unSelectedColour = buttons[0].image.color;
		foreach(GameObject g in menuList)
		{
			g.SetActive(true);
			g.SetActive(false);
		}
		PlayerPrefferences.WakeUp();
		menuList[activeMenu].SetActive(true);
	}
	
	void Update()
	{
		if(activeMenu == 0)
		{
			if(Input.GetButtonDown ("P1_Fire1") && delayTime < Time.time)
			{
				if(selectedMenu == 1)
				{
					LoadLevel(2);
				}
				else if(selectedMenu < menuList.Length+1)
				{
					delayTime = INPUTDELAY+Time.time;
					SwitchMenu(selectedMenu-1);
				}else if(selectedMenu >= menuList.Length+1)
					Quit();
			}
			else if(Input.GetAxis ("P1_Vertical") > 0.3 && delayTime < Time.time)
			{
				selectedMenu --;
				delayTime = Time.time + INPUTDELAY;
				SetButtonColours();
			}
			else if(Input.GetAxis ("P1_Vertical") < -0.3 && delayTime < Time.time)
			{
				selectedMenu ++;
				delayTime = Time.time + INPUTDELAY;
				SetButtonColours();
			}
		}else if(Input.GetButtonDown ("P1_Fire2"))
		{
			delayTime = INPUTDELAY+Time.time;
			SwitchMenu(0);
		}
	}

	public void LoadLevel(int level)
	{
		if(loadingLevel)
			return;
		
		loadingLevel = true;
		FaderScript fs = GameObject.FindGameObjectWithTag("FaderScript").GetComponent<FaderScript>();
		for(int i = 0; i < 4; i++)
		{
			if(scrollBars[i].inPlay)
			{
				//int player, int t1, int t2, int t3, int apperance, int team
				int[] types = scrollBars[i].GetTypes();
				PlayerPrefferences.SetPlayer(i,types[0],types[1],types[2],(int)(scrollBars[i].currentCharacterValue*5),(int)(1+scrollBars[i].currentTeamValue*3));
			}
		}
		StartCoroutine(fs.BeginLevelFade(level));
	}
	
	public void Quit()
	{
		Application.Quit();
	}
	
	public void SwitchMenu(int menu)
	{
		menuList[activeMenu].SetActive(false);
		menuList[menu].SetActive(true);
		activeMenu = menu;
	}
	
	void SetButtonColours()
	{
		if (selectedMenu < 0)
			selectedMenu = 0;
		else if (selectedMenu > 5)
			selectedMenu = 5;
		foreach(Button b in buttons)
		{
			b.image.color = unSelectedColour;
		}
		if(selectedMenu > 0)
			buttons[selectedMenu - 1].image.color = new Color(0.796f,0.796f,1,1);
	}
}
