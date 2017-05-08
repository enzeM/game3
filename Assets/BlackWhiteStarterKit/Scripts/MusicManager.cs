using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
	public	static MusicManager		Instance;

	[Tooltip("Audio Source Of Game Events")]
	public	AudioSource				audioSourceGame;
	[Tooltip("Audio When Game Over")]
	public	AudioClip				audioClipGameOver;
	[Tooltip("Audio When Level Up")]
	public	AudioClip				audioClipLevelUp;
	[Tooltip("Audio Clips When Coin Collected")]
	public	AudioClip				audioClipCoinsCollected;


	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		if (!audioSourceGame)
			Debug.LogError ("Audio Source not set!!");
		if (!audioClipGameOver)
			Debug.LogError ("Audio Clip for Game Over not set!");
		if (!audioClipLevelUp)
			Debug.LogError ("Audio Clip for Level Up not set!");
		if (!audioClipCoinsCollected)
			Debug.LogError ("Audio Clip for Coin Collected not set!");

	}

	public void PlayGameOver ()
	{
		if (this.audioClipGameOver) {
			this.audioSourceGame.clip = this.audioClipGameOver;
			this.audioSourceGame.loop = false;
			this.audioSourceGame.Play ();
		}
	}
	
	public void PlayCoinCollected ()  // PLAY RANDOM CLIP WHEN ORBS ARE SMASHED..
	{
		this.audioSourceGame.clip = this.audioClipCoinsCollected;
		this.audioSourceGame.loop = false;
		this.audioSourceGame.Play ();
	}

	public void PlayLevelUp ()
	{
		if (this.audioClipLevelUp) {
			this.audioSourceGame.clip = this.audioClipLevelUp;
			this.audioSourceGame.loop = false;
			this.audioSourceGame.Play ();
		}
	}
	


}
