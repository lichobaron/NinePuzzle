using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIndicator : MonoBehaviour {

	public Material red;
	public Material green;
	private GameObject indicator;

	// Use this for initialization
	void Start () {
		indicator = GameObject.FindWithTag("indicator");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		indicator.GetComponent<Renderer>().material = red;
	}

	void OnCollisionExit(Collision collision)
	{
		indicator.GetComponent<Renderer>().material = green;
	}
}
