using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int score = 0;
	public Text scoreText;

	[SerializeField] private float addScoreTime = 1f;
	[SerializeField] private float addScoreCounter;
	// Use this for initialization
	void Start () {
		addScoreCounter = addScoreTime;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!Player.isDead) {
			addScoreCounter -= Time.deltaTime;
			if (addScoreCounter < 0) {
				score++;
				scoreText.text = score.ToString();
				addScoreCounter = addScoreTime;
			}
		}
	}
}
