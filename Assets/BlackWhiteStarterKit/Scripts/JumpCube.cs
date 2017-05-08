using UnityEngine;
using System.Collections;

public class JumpCube : MonoBehaviour {

	public static JumpCube Instance;
	private bool  isJumping  = false;
	public float jumpPower   = 100f;
	public float jumpYEndPos = 1.1f;  
	public bool  usePhysJump = false;
	public float pointsToGet = 10f;
	public float score       = 0f;
	
	public GameObject myCoin;

	[HideInInspector]
	public int  firstTime = 2;
	private bool canJump  = false;


	void Awake(){

		Instance = this;
	}

	// Use this for initialization
	void Start () {

		score	  = 0f;
		firstTime = 2;
		canJump   = true;
	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetMouseButtonDown(0) && canJump)
		{
			isJumping = true;

			if( usePhysJump )
				Jump();
			else 
				Jump2();

			firstTime--;
			if( firstTime == 0 ){
				// SHOW THE CUBES TO MOVE NOW AND HIDE THE TUTORIAL..
				MoveCubes.Instance.SetExeCuteTimer(true);  //ENABLES THE MOVING..
				StartCoroutine(MoveCubes.Instance.moveTheCubes());
				MenuManager.Instance.HideTutorial(false);	 //HIDE THE TUTORIAL IMAGE..
			}

		}
			
	}


	public void SetCanJump(bool _value){
		canJump = _value;
	}


	public void Jump()
	{
		print("jump --> AddForce");
		GetComponent<Rigidbody>().AddForce(transform.up*jumpPower);
	}

	public void Jump2()
	{
		print("jump --> position");
		Vector3 newPos = new Vector3(transform.position.x, jumpYEndPos, transform.position.z);
		transform.position = newPos;
	}


	void OnCollisionEnter(Collision collision) {

		string 		col_tag = collision.gameObject.tag;
		GameObject  box 	= collision.gameObject;

		if( col_tag == "GOODCUBE" ) 
		{
			MusicManager.Instance.PlayCoinCollected(); // COLLECTED COINS..

			/*	  COINS ... BEGIN */
			Vector3 newPos2 = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z); // Set the POsition to the POsition above the player.. 

			GameObject coin = (GameObject)Instantiate(myCoin); 													 // Instatia the Coin on the Hitted Position and let it fly down..
			coin.transform.position = newPos2;
			coin.AddComponent<Rigidbody>().AddForce(coin.transform.up*100f);									 // Add an RigidBody to the COin.. so it will fall down..

			Destroy(coin,0.6f);
			/*	  COINS ... END */

			// DESTROY THE BOX ... SO IT WILL DISAPEAR.. 
			if( box && !box.GetComponent<Rigidbody>() ) // Check if box hitted and if it has an rigidbody.. 
			{
				//box.AddComponent<Rigidbody>().AddForce(box.transform.up*500f); //Will add an rigidbody to the box and shoot it into the air.. 

				/*		SCORE 		*/
				score += pointsToGet;
				/*		SCORE 		*/

				Destroy(box);  //REMOVE IT
			}
		}
		else if( col_tag == "BADCUBE")
		{
			//print ("HITTED THE BAD CUBE");
			MusicManager.Instance.PlayGameOver(); // PLAY GAME OVER SOUND..
			GameManager.Instance.SetGameIsOver( true ); // Tell GAMEMANAGER that the game is over..

		}
	
	}


}
