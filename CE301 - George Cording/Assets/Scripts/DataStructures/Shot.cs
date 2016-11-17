using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour 
{
	
	/*
		grades is an integer that denotes the type of element: Primary 0, Secondary 1, Tertiary 2.
		Positions is a Vector that denotes the positions of the shots spawning.
		Rotations is Quaternions thats denote the rotations of the shots.
	*/
	public int TYPE = 0;
	public int GRADE = 0;
	public Vector3 POSITION;
	public Vector3 ROTATION;
	
	/*public Shot(int g, Vector3 pos, Vector3 rot)
	{
		TYPE = 0;
		GRADE = g;
		POSITION = pos;
		ROTATION = rot;
	}*/
}