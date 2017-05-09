using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColour : MonoBehaviour {

	[SerializeField] private Renderer rend;
	[SerializeField] private int myColour;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		GetObjectColour ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay(Collision other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (myColour == Player.PlayerColour) {
				print ("same colour");
			} else {
				print ("not same colour destory");
			}
		}
	}

	void GetObjectColour () {
		if (rend.material.color == Color.red) {
			myColour = Player.RED;
		} else if (rend.material.color == Color.blue) {
			myColour = Player.BLUE;
		} else {
			myColour = Player.DEFAULT;
		}
	}
}
