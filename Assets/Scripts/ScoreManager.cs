﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int score;
	public static int bestScore = 0;
	public Text scoreText;
	public Text speedText;
	public Text bestScoreText;
	public Player player;

	[SerializeField] private float addScoreTime = 1f;
	[SerializeField] private float addScoreCounter;
	// Use this for initialization
	void Start () {
		score = 0;
		addScoreCounter = addScoreTime;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!Player.isDead) {
			addScoreCounter -= Time.deltaTime;
			if (addScoreCounter < 0) {
				//mornitor player score
				score++;
				scoreText.text = score.ToString ();
				addScoreCounter = addScoreTime;
			}
			//monitor player speed
			speedText.text = player.m_moveSpeed.ToString ();
			//update best score when player had dead
		} else if (Player.isDead) {
			if (score > bestScore) {
				bestScore = score;
			}
		}
		bestScoreText.text = bestScore.ToString ();
	}
}
