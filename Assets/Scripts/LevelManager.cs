﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (int sceneIndex) {
		SceneManager.LoadScene (sceneIndex);
	}

	public void ContinueTutorial () {
		Player.autoRun = true;
	}
}
