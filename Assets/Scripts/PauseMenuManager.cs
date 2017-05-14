using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {
	public GameObject pauseMenu;
	private bool isPause;
	private float delayTime = 2f;
	private float delayCounter;

	// Use this for initialization
	void Start () {
		isPause = false;			
		delayCounter = delayTime;
	}

	// Update is called once per frame
	void Update () {
		HandleInput ();
		ManagePause ();	
		if (Player.isDead) {
			delayCounter -= Time.deltaTime;
			if (delayCounter < 0f) {
				isPause = true;	
				delayCounter = delayTime;
			}
		}
	}

	void HandleInput() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPause = !isPause;
		} 
	}

	void ManagePause () {
		if (isPause) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
		pauseMenu.SetActive (isPause);
	}

	public void Restart() {
		string currentSceneName = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene (currentSceneName);
	}

	public void MainMenu() {
		SceneManager.LoadScene (0);
	}

	public void Quit() {
		Application.Quit ();
	}

}
