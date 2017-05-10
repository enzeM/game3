﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
	[SerializeField] private Transform target;
	[SerializeField] private float rotSpeed = 1.5f;
	[SerializeField] private float rotY;
	[SerializeField] private float height = 1.5f;
	[SerializeField] private Vector3 offset; //offset between camera and target

	void Start() {
		rotY = transform.eulerAngles.y;
		offset = target.position - transform.position;
	}

	void LateUpdate() {
		//press E or Q to rotate camera clockwise/anti-clockwise
		if (Input.GetKey (KeyCode.E)) {
			rotY += 1f * rotSpeed;
		} else if (Input.GetKey (KeyCode.Q)) {
			rotY -= 1f * rotSpeed;
		} 
		Quaternion rotation = Quaternion.Euler (0f, rotY, 0f);
		transform.position = target.position + offset;
		//remain offset when camera is rotating
		transform.position = target.position - (rotation * offset);
		//keep track with the target
		transform.LookAt (target.position + new Vector3 (0f, height, 0f));
	}
}
