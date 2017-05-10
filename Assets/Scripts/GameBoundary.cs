using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoundary : MonoBehaviour {
	[SerializeField] private GameObject targetPlayer;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (targetPlayer.transform.position.x, transform.position.y, transform.position.z);		
	}

	void OnTriggerExit (Collider other) {
		if (other.CompareTag ("Player")) {
			other.gameObject.SetActive (false);
		} 
		if (other.CompareTag("Ground")) {
			Destroy (other.gameObject);
		}
	}
}
