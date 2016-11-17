using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	GameController gC;
	public float xPanDistance = 0.0f;
	public float yPanDistance = 0.0f;
	public float xOffset;
	public float yOffset;
	public GameObject focus;
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		focus = new GameObject();
		if (gC == null)
			gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		focus.transform.position = GetDestination();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = GetDestination();
		float step = speed * Time.deltaTime / (Vector3.Distance(focus.transform.position, temp));
		focus.transform.position = Vector3.MoveTowards(focus.transform.position, temp, step);
		transform.LookAt(focus.transform);
	}
	
	Vector3 GetDestination()
	{
		Vector3 temp = gC.GetCenterPoint();
		if(temp.x > xPanDistance+xOffset)
		{
			temp.x = xPanDistance+xOffset;
		}
		else if (temp.x < xOffset-xPanDistance)
		{
			temp.x = xOffset-xPanDistance;
		}
		if (temp.y > yPanDistance+yOffset)
		{
			temp.y = yPanDistance+yOffset;
		}
		else if (temp.y < yOffset-yPanDistance)
		{
			temp.y = yOffset-yPanDistance;
		}
		return temp;
	}
}
