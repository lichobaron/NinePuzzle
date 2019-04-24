using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCube : MonoBehaviour {

	private bool mouseState = false;
	private bool touching = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			mouseState = true;
		}
			
		else if (Input.GetMouseButtonUp(0))
		{
			mouseState = false;
		}
		if (touching)
		{
			Move();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("Colision "+ gameObject.tag);
		if (collision.gameObject.tag == "cursor")
		{
			touching = true;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "cursor")
		{
			touching = false;
		}
	}

	/*
	* Can Move:
	*  0 -> Can't move
	*  1 -> Can move up
	*  2 -> Can move down
	*  3 -> Can move right
	*  4 -> Can move left
	*/
	private int CanMove(float[] EmptyPos, float x, float y)
	{
		//Position 1
		if (EmptyPos[0] == 5 && EmptyPos[1] == 25)
		{
			if (x == 5 && y == 15)
				return 1;
			if (x == 15 && y == 25)
				return 4;
		}
		//Position 2
		else if (EmptyPos[0] == 15 && EmptyPos[1] == 25)
		{
			if (x == 5 && y == 25)
				return 3;
			if (x == 15 && y == 15)
				return 1;
			if (x == 25 && y == 25)
				return 4;
		}
		//Position 3
		else if (EmptyPos[0] == 25 && EmptyPos[1] == 25)
		{
			if (x == 15 && y == 25)
				return 3;
			if (x == 25 && y == 15)
				return 1;
		}
		//Position 4
		else if (EmptyPos[0] == 5 && EmptyPos[1] == 15)
		{
			if (x == 5 && y == 5)
				return 1;
			if (x == 15 && y == 15)
				return 4;
			if (x == 5 && y == 25)
				return 2;
		}
		//Position 5
		else if (EmptyPos[0] == 15 && EmptyPos[1] == 15)
		{
			if (x == 5 && y == 15)
				return 3;
			if (x == 15 && y == 5)
				return 1;
			if (x == 25 && y == 15)
				return 4;
			if (x == 15 && y == 25)
				return 2;
		}
		//Position 6
		else if (EmptyPos[0] == 25 && EmptyPos[1] == 15)
		{
			if (x == 15 && y == 15)
				return 3;
			if (x == 25 && y == 5)
				return 1;
			if (x == 25 && y == 25)
				return 2;
		}
		//Position 7
		else if (EmptyPos[0] == 5 && EmptyPos[1] == 5)
		{
			if (x == 5 && y == 15)
				return 2;
			if (x == 15 && y == 5)
				return 4;
		}
		//Position 8
		else if (EmptyPos[0] == 15 && EmptyPos[1] == 5)
		{
			if (x == 5 && y == 5)
				return 3;
			if (x == 15 && y == 15)
				return 2;
			if (x == 25 && y == 5)
				return 4;
		}
		//Position 9
		else if (EmptyPos[0] == 25 && EmptyPos[1] == 5)
		{
			if (x == 15 && y == 5)
				return 3;
			if (x == 25 && y == 15)
				return 2;
		}
		return 0;
	}

	private void Move()
	{
		if (true)
		{
			int move = CanMove(PlayGame.EmptyPos, gameObject.transform.position.x, gameObject.transform.position.y);
			//Debug.Log("Colision " + "cursor " + move);
			if (move != 0)
			{
				PlayGame.UpdateEmpty(gameObject.transform.position.x, gameObject.transform.position.y);
				if (move == 1)
				{
					transform.Translate(new Vector3(0, -10, 0));
				}
				else if (move == 2)
				{
					transform.Translate(new Vector3(0, 10, 0));
				}
				else if (move == 3)
				{
					transform.Translate(new Vector3(-10, 0, 0));
				}
				else if (move == 4)
				{
					transform.Translate(new Vector3(10, 0, 0));
				}
			}
		}
	}
}
