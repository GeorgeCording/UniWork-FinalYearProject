using UnityEngine;
using System.Collections;

public class CharacterSpawner : MonoBehaviour 
{
	public GameObject p;
	public bool facingRight = false;
	public int team = 0;
	public string pNumber;
	public int t1 = 0;
	public int t2 = 0;
	public int t3 = 0;
	public HealthBarController hB;
	GameObject play;
	
	public void SetTypes(int m, int s, int t)
	{
		t1 = m;
		t2 = s;
		t3 = t;
	}
	
	public CharacterControllerScript Spawn()
	{
		play = Instantiate(p,transform.position, transform.rotation) as GameObject;
		CharacterControllerScript css = play.GetComponent<CharacterControllerScript>();
		css.facingRight = facingRight;
		css.TEAM = team;
		css.pNumber = pNumber;
		css.mainType = t1;
		css.secondType = t2;
		css.thirdType = t3;
		css.hbc = hB;
		css.CalculateDebuffs();
		return css;
	}
	
	public GameObject GetPlayer()
	{
		return play;
	}
}