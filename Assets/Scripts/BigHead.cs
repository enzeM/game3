using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHead : MonoBehaviour {

	private Animator anim;
	private float jumpTimer = 0;

	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		HandleRun ();
		HandleJump ();
	}

	private void HandleRun() {
		if (Input.GetAxis ("Horizontal") != 0) {
			float moveHori = Input.GetAxis ("Horizontal");
			if(Input.GetKey(KeyCode.RightShift)) {
				anim.SetInteger ("Speed", 2);
			} else {
				anim.SetInteger ("Speed", 1);
			} 
			transform.Translate (0f, 0f, moveHori * 5 * Time.deltaTime);
		} else {
			anim.SetInteger ("Speed", 0);
		}
	}

	private void flip() {

	}

	private void HandleJump() {
		if (Input.GetKey (KeyCode.Space)) {
			jumpTimer = 0.5f;
			anim.SetBool ("Jumping", true);
		}
		if (jumpTimer > 0.2f) {
			jumpTimer -= Time.deltaTime;
		} else if (anim.GetBool ("Jumping") == true) {
			anim.SetBool ("Jumping", false);
		}
	}
}
