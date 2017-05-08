using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public static MenuManager Instance;

	// ---- PANELS ----
	public Transform panelGameOver;
	public Transform panelStartMenu;
	public Transform panelHud;
	public Image     imageTutorial;
	public Text 	 curScore;
	// --> GAME OVER PANEL 
	public Text      topScoreGO, curScoreGO;
	// --> START SCREEN TOP SCORE..
	public Text  	 topScoreStart;

	void Awake() {

		Instance = this;
	}

	// Use this for initialization
	void Start () {

		if (!panelGameOver)
			Debug.LogError ("Panel for Game Over is not set in the MenuManager");
	
		if (!panelStartMenu)
			Debug.LogError ("Panel for Start Menu is not set in the MenuManager");
	
		if (!panelHud)
			Debug.LogError ("Panel for HUD is not set in the MenuManager");

		if (!curScore)
			Debug.LogError ("Cur Score TXT object ist not set");

		if (!topScoreStart)
			Debug.LogError ("Top Score Start not set..");

		//IF THE GAME STARTS.. SHOW THE START MENU..
		panelStartMenu.gameObject.SetActive (true );
		panelGameOver.gameObject.SetActive  (false);
		panelHud.gameObject.SetActive       (false);

		//SHOW THE TOP SCORE ON THE START..
		topScoreStart.text = PlayerPrefs.GetFloat ("topscore").ToString ();

	}


	public void ShowGameOver()
	{
		panelStartMenu.gameObject.SetActive (false);
		panelGameOver.gameObject.SetActive  (true );
		panelHud.gameObject.SetActive       (false);

	}

	public void ShowHud(bool _showTutorial = true)
	{
		panelStartMenu.gameObject.SetActive (false);
		panelGameOver.gameObject.SetActive  (false);
		panelHud.gameObject.SetActive       (true );
		imageTutorial.gameObject.SetActive  (_showTutorial);
	}

	public void HideTutorial(bool _value)
	{
		imageTutorial.gameObject.SetActive (_value);
	}

	public void UpdateScore(string _score)
	{
		curScore.text = _score;
	}

	public void ShowTopScore( string _topscore, string _current){
		topScoreGO.text = _topscore;
		curScoreGO.text = _current ;
	}

}
