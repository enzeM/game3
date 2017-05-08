using UnityEngine;
using System.Collections;

public class RandomPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// x > -6 && x < 5 

		// SET RANDOM POSITION FOR ALL CHILDs..
		foreach (Transform child in transform){
			Vector3 newPosition = new Vector3(Random.Range(-6f,5f), Random.Range(1f,7f), transform.position.z);
			child.transform.position = newPosition;
		}
	
	}
	

}
