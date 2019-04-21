using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour {

	public static float[] EmptyPos { get; set; }
	public static GameObject[] cubes;
	private GameObject message;

	// Use this for initialization
	void Start () {
		EmptyPos = new float[2];
		UpdateEmpty(5, 5);

		cubes = new GameObject[8];
		for (int i = 1; i < 9; i++)
		{
			if (i < 3)
			{
				cubes[i - 1] = GameObject.FindWithTag("cube" + i);
			}
			else
			{
				int k = i+1;
				cubes[i - 1] = GameObject.FindWithTag("cube" + k);
			}
				
		}

		message = GameObject.FindWithTag("message");
	}
	
	// Update is called once per frame
	void Update () {
		if (FinishedGame())
		{
			message.GetComponent<TextMesh>().text = "You win!";
		}
	}

	public static void UpdateEmpty(float x, float y)
	{
		EmptyPos[0] = x;
		EmptyPos[1] = y;
	}

	private bool FinishedGame() {
		if (cubes[0].transform.position.x == 5 && cubes[0].transform.position.y == 25 &&
			cubes[1].transform.position.x == 15 && cubes[1].transform.position.y == 25 &&
			cubes[2].transform.position.x == 5 && cubes[2].transform.position.y == 15 &&
			cubes[3].transform.position.x == 15 && cubes[3].transform.position.y == 15 &&
			cubes[4].transform.position.x == 25 && cubes[4].transform.position.y == 15 &&
			cubes[5].transform.position.x == 5 && cubes[5].transform.position.y == 5 &&
			cubes[6].transform.position.x == 15 && cubes[6].transform.position.y == 5 &&
			cubes[7].transform.position.x == 25 && cubes[7].transform.position.y == 5)
		{
			return true;
		}
		return false;
	}
}
