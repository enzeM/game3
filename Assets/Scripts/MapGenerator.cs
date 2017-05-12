using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	[SerializeField] private GameObject[] Grounds;
	private bool isGenerated;
	[SerializeField] private GameObject[] GeneratedPoints;

	// Use this for initialization
	void Start () {
		isGenerated = false;
	}
	
	// Update is called once per frame
	void Update () {
		//print (Player.OnLowGenPot);	
	}

	void OnTriggerEnter(Collider other) {
		if (! isGenerated && other.CompareTag ("Player")) {
			HandleMapGeneration ();
		}
	}

	/*
	* 1. generate item type and genPoint position randomly
	* 2. create item based on generate point
	* 3. make sure mid generate point exist at least once when low generate point existed
	* 4. add-on random: the generate point position should be random in a reasonable range as well
	* 5. always check the lower limit of the player
	*/
	private void HandleMapGeneration () {
		int typeIndex = Random.Range (0, Grounds.Length);
		int randVirtDist = Random.Range (16, 21);
		int randHoriDist = Random.Range (-1, 1);

		if (! Player.OnLowGenPot) {
			int genPotIndex = Random.Range (0, GeneratedPoints.Length);
			//check ground is generated at "low" point
			if (genPotIndex == 2) {
				Player.OnLowGenPot = true;
			}
			Vector3 genPotPos = GeneratedPoints [genPotIndex].transform.position;
			float genPotY = randHoriDist + genPotPos.y;
			//check lower limit
			if (genPotY < Player.LowerLimit) {
				genPotY = Player.LowerLimit + 2;
			}

			Vector3 itemPos = new Vector3 (genPotPos.x + randVirtDist, genPotY, genPotPos.z);
			Instantiate (Grounds [typeIndex], itemPos, Quaternion.Euler (new Vector3 (0, 0, 0)));
			isGenerated = true;
		} else if(Player.OnLowGenPot) {
			Vector3 genPotPos = GeneratedPoints [0].transform.position; //next item occur in the high
			float genPotY = randHoriDist + genPotPos.y;
			//check lower limit
			if (genPotY < Player.LowerLimit) {
				genPotY = Player.LowerLimit + 2;
			}
			Vector3 itemPos = new Vector3 (genPotPos.x + randVirtDist, genPotY, genPotPos.z);
			Instantiate (Grounds [typeIndex], itemPos, Quaternion.Euler (new Vector3 (0, 0, 0)));
			isGenerated = true;
			Player.OnLowGenPot = false; //reset on low generate point to false
		}
	}
}
