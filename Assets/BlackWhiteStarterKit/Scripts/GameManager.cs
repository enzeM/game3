using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public 	static	GameManager	Instance;

	private bool 		gameIsOver = false;
	private GameObject 	player;
	private float      	actualScore;
	private int 		counters = 0;
	private bool       	showOnce = true;


	void Awake ()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		player         = GameObject.FindGameObjectWithTag("Player");
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if( gameIsOver && showOnce )
		{
			showOnce = false;
			print("Game is Over");
			StartCoroutine(restartNow());
		}

		//Update Score 
		MenuManager.Instance.UpdateScore (player.GetComponent<JumpCube> ().score.ToString ());
	
	}


	public void SetGameIsOver(bool _gameOver)
	{
		gameIsOver = _gameOver;
	}


	IEnumerator restartNow()
	{
		yield return 0;
		restartCurrentScene ();
	}


	public void restartCurrentScene(){

		JumpCube.Instance.SetCanJump (false);

		// SHO THE AD TO THE USER.. 
		AdManager.Instance.ShowAd ();

		//Save the Score before restart.. 
		float myBestScr = PlayerPrefs.GetFloat("topscore");
		float myCurrent = player.GetComponent<JumpCube>().score;

		if( myBestScr < myCurrent )
			PlayerPrefs.SetFloat("topscore", myCurrent);

		MoveCubes.Instance.StopAllGameIsOver (); // STOP MOVING THE CUBES..
		MenuManager.Instance.ShowTopScore (PlayerPrefs.GetFloat ("topscore").ToString (), myCurrent.ToString());
		MenuManager.Instance.ShowGameOver    (); // SHOW THE GAME OVER MANAGER
	}


	public void RestartScene()
	{
		gameIsOver = false;
		showOnce   = true;

		player.GetComponent<JumpCube> ().score = 0;

		MoveCubes.Instance.StopAllGameIsOver ();
		Spawner.Instace.Reset ();
		JumpCube.Instance.firstTime = 1;
		JumpCube.Instance.SetCanJump (true);
		MenuManager.Instance.HideTutorial(true);	 //HIDE THE TUTORIAL IMAGE..
	}
	


}
