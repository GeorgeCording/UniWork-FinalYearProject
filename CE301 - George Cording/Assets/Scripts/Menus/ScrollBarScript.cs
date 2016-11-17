using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollBarScript : MonoBehaviour 
{
	public string playerInput = "P1_";
	public bool inPlay = false;
	public float currentCharacterValue = 0;
	public float currentTeamValue = 0;
	public Scrollbar characterBar;
	public Scrollbar teamBar;
	public SpawnMenuCharacterScript cs;
	public GameObject spawnPoint;
	public GameObject typeBox;
	public Text teamLabel;
	public Text characterLabel;
	public Text addLabel;
	public CharacterSelectionTypesScript[] c;
	public GameObject[] selectionArrow;
	public float INPUTDELAY = 0.3f;
	float delayTime = 0.0f;
	int activeSelection = 0;
	int typeSelection = 0;
	
	void Start()
	{
		TogglePlayer();
	}
	
	void Update()
	{
		if(inPlay)
			{
			if(currentCharacterValue != characterBar.value)
			{
				currentCharacterValue = characterBar.value;
				cs.ShowCharacter((int)(currentCharacterValue*5));
				characterLabel.text = "Character: "+GetElement(currentCharacterValue*5);
			}
			if(currentTeamValue != teamBar.value)
			{
				currentTeamValue = teamBar.value;
				teamLabel.text = "Team: "+(1+currentTeamValue*3);
			}
			if(Input.GetAxis (playerInput+"Vertical") < -0.3 && delayTime < Time.time)
			{
				if(typeSelection == 0|| activeSelection != 3)
				{
					activeSelection ++;
					delayTime = INPUTDELAY+Time.time;
					SetArrow();
				}else
				{
					c[typeSelection-1].Down();
					delayTime = (INPUTDELAY/2)+Time.time;
					SetArrow();
				}
			} 
			else if(Input.GetAxis (playerInput+"Vertical") > 0.3 && delayTime < Time.time)
			{
				if(typeSelection == 0|| activeSelection != 3)
				{
					activeSelection --;
					delayTime = INPUTDELAY+Time.time;
					SetArrow();
				}else
				{
					c[typeSelection-1].Up();
					delayTime = (INPUTDELAY/2)+Time.time;
					SetArrow();
				}
			} 
			else if(Input.GetAxis(playerInput+"Horizontal") > 0.3 && delayTime < Time.time)
			{
				if(activeSelection == 3)
				{	
					typeSelection++;
					delayTime = INPUTDELAY+Time.time;
					SetArrow();
				}
				else
				{
					MoveSlider(1.0f);
					delayTime = INPUTDELAY+Time.time;
				}
			}
			else if(Input.GetAxis(playerInput+"Horizontal") < -0.3 && delayTime < Time.time)
			{
				if(activeSelection == 3)
				{	
					typeSelection--;
					delayTime = INPUTDELAY+Time.time;
					SetArrow();
				}else
				{
					MoveSlider(-1.0f);
					delayTime = INPUTDELAY+Time.time;
				}
			}
		}else if(activeSelection != 0)
		{
			activeSelection = 0;
		}
		if(Input.GetButtonDown (playerInput+"Fire1") && delayTime < Time.time && activeSelection == 0)
		{
			TogglePlayer();
			delayTime = INPUTDELAY+Time.time;
		}
	}
	
	public void TogglePlayer()
	{
		inPlay = !inPlay;
		spawnPoint.SetActive(inPlay);
		characterBar.gameObject.SetActive(inPlay);
		teamBar.gameObject.SetActive(inPlay);
		teamLabel.gameObject.SetActive(inPlay);
		characterLabel.gameObject.SetActive(inPlay);
		typeBox.gameObject.SetActive(inPlay);
		foreach(GameObject g in selectionArrow)
		{
			g.SetActive(false);
		}
		if(inPlay)
			addLabel.text = "Remove";
		else
			addLabel.text = "Add";
		if(inPlay)
		{
			SetArrow();
		}
	}
	
	public int[] GetTypes()
	{
		int[] toReturn = new int[3];
		for(int i = 0; i < 3; i++)
		{
			toReturn[i] = c[i].num;
		}
		return toReturn;
	}
	
	static string GetElement(float i)
	{
		switch((int)i)
		{
			case 1:
			return "Fire";
			
			case 2:
			return "Wind";
			
			case 3:
			return "Earth";
			
			case 4:
			return "Lightning";
			
			case 5:
			return "Water";
			
			default:
			return "Neutral";
		}
	}
	
	void OnEnable()
	{
		cs.gameObject.SetActive(inPlay);
	}
	
	void OnDisable()
	{
		cs.gameObject.SetActive(false);
	}
	
	public void SetArrow()
	{
		foreach(GameObject g in selectionArrow)
		{
			g.SetActive(false);
		}
		if (activeSelection > 3)
			activeSelection = 3;
		else if(activeSelection < 0)
			activeSelection = 0;
		if(activeSelection > 0)
			selectionArrow[activeSelection-1].SetActive(true);
		if (typeSelection < 0)
			typeSelection = 0;
		else if(typeSelection > 3)
			typeSelection = 3;
		if(activeSelection == 3)
		{
			foreach(CharacterSelectionTypesScript cs in c)
			{
				cs.Hide();
			}
			if(typeSelection > 0)
				c[typeSelection - 1].Show();
		}
		else
		{
			foreach(CharacterSelectionTypesScript cs in c)
			{
				cs.Show();
			}
		}
	}
	
	void MoveSlider(float amount)
	{
		if(activeSelection == 1)
		{
			float temp = teamBar.value + amount/teamBar.numberOfSteps;
			teamBar.value = temp;
			if (teamBar.value > 1)
				teamBar.value = 1;
			else if(teamBar.value < 0)
				teamBar.value = 0;
		}
		else if(activeSelection == 2)
		{
			characterBar.value += amount/teamBar.numberOfSteps;
			if (characterBar.value > 1)
				characterBar.value = 1;
			else if(characterBar.value < 0)
				characterBar.value = 0;
		}
	}
}
