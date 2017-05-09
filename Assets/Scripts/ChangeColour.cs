using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour {
	public Material red;
	public Material blue;
	public Material def;

	private GameObject playerColour;
	private Renderer rend;
	// Use this for initialization
	void Start () {
		playerColour = GameObject.FindGameObjectWithTag ("PlayerColour");
		rend = playerColour.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	//J to Red, K to Blue and L to Default
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.J)) {
			rend.material = blue;
			Player.PlayerColour = Player.BLUE;
		} else if (Input.GetKeyDown (KeyCode.K)) {
			rend.material = red;
			Player.PlayerColour = Player.RED;
		} else if (Input.GetKeyDown (KeyCode.L)) {
			rend.material = def;
			Player.PlayerColour = Player.DEFAULT;
		}
	}
}
