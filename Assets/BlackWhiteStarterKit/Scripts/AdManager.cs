using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour 
{
	[SerializeField] string gameIDAndroid = "41052"; //YOUR GAMID FROM THE UNITYADS FOR THE ANDROID
	[SerializeField] string gameIDIOS     = "41052"; //YOUR GAMID FROM THE UNITYADS FOR THE IOS

	private string gameID = "41052";

	public static AdManager Instance;
	
	void Awake()
	{
		Instance = this;


		DontDestroyOnLoad(gameObject);

		#if UNITY_ANDROID 
		gameID = gameIDAndroid;
		#else
		gameID = gameIDIOS;
		#endif

		Advertisement.Initialize (gameID, false);
	}
	
	public void ShowAd(string zone = "")
	{
		#if UNITY_EDITOR
		StartCoroutine(WaitForAd ());
		#endif
		
		if (string.Equals (zone, ""))
			zone = null;
		
		ShowOptions options = new ShowOptions ();
		options.resultCallback = AdCallbackhandler;
		
		if (Advertisement.isReady (zone))
			Advertisement.Show (zone, options);
	}
	
	void AdCallbackhandler (ShowResult result)
	{
		switch(result)
		{
			case ShowResult.Finished:
				Debug.Log ("Ad Finished. Rewarding player...");
				break;
			case ShowResult.Skipped:
				Debug.Log ("Ad skipped. Son, I am dissapointed in you");
				break;
			case ShowResult.Failed:
				Debug.Log("I swear this has never happened to me before");
				break;
		}
	}
	
	IEnumerator WaitForAd()
	{
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;
		
		while (Advertisement.isShowing)
			yield return null;
		
		Time.timeScale = currentTimeScale;
	}
}
