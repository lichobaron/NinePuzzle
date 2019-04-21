using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchOption : MonoBehaviour
{

	private GameObject[] cubes;
	private bool mouseState = false;
	private bool touching = false;

	// Use this for initialization
	void Start()
	{
		cubes = new GameObject[8];
		for (int i = 1; i < 9; i++)
		{
			if (i < 3)
			{
				cubes[i - 1] = GameObject.FindWithTag("cube" + i);
			}
			else
			{
				int k = i + 1;
				cubes[i - 1] = GameObject.FindWithTag("cube" + k);
			}

		}
	}

	// Update is called once per frame
	void Update()
	{
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
			ButtonsLogic();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
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

	private void ButtonsLogic()
	{
		if (mouseState)
		{
			if (gameObject.tag == "reset")
			{
				Debug.Log("ENTER");
				ResetGame();
			}
			else if (gameObject.tag == "help")
			{
				Debug.Log("ENTER");
				Help();
			}
		}
	}

	private void ResetGame()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	private void Help()
	{
		cubes[0].transform.position.Set(0, 0, 0);
		cubes[1].transform.position.Set(0, 0, 0);
		cubes[2].transform.position.Set(0, 0, 0);
		cubes[3].transform.position.Set(0, 0, 0);
		cubes[4].transform.position.Set(0, 0, 0);
		cubes[5].transform.position.Set(0, 0, 0);
		cubes[6].transform.position.Set(0, 0, 0);
		cubes[7].transform.position.Set(0, 0, 0);

		cubes[0].transform.position.Set(5, 25, 0);
		cubes[1].transform.position.Set(15, 25, 0);
		cubes[2].transform.position.Set(5, 15, 0);
		cubes[3].transform.position.Set(15, 15, 0);
		cubes[4].transform.position.Set(25, 25, 0);
		cubes[5].transform.position.Set(5, 5, 0);
		cubes[6].transform.position.Set(15, 5, 0);
		cubes[7].transform.position.Set(25, 5, 0);
	}
}
