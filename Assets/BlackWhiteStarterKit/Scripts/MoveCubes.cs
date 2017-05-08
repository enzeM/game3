using UnityEngine;
using System.Collections;

public class MoveCubes : MonoBehaviour {

	public static MoveCubes Instance;

	public   float 		x_offset; 			// By how much the character should move in the X direction
	public   float 		y_offset; 			// By how much the character should move in the Y direction
	public   float 		speed_x  = 1f;		// INITIAL SPEED OF THE CUBES.. 1f is slow.. it will be faster and faster..

	private  Vector3 	newPos; 		// New Position for the cube
	private	 bool    	exeCuteTimer = false;
	private  float  	speed_x_old;

	void Awake() {

		Instance = this;

	}
	
		
	// TELL THE MOVE CUBES THAT THE GAME IS OVER AND RESET ALL SETTINGS..
	public void StopAllGameIsOver()
	{
		exeCuteTimer = false;
		StopCoroutine (moveTheCubes());
		speed_x      = 1f;
	}

	public void SetExeCuteTimer(bool _value)
	{
		exeCuteTimer = _value;
	}


	public void changeSpeed()
	{
		if( speed_x >= 0.3f )
			speed_x -= 0.2f;	
		else
			speed_x  = 0.2f;
	}


	// -->> MOVE THE CUBES EVERY TIME STEP --> DEFINED IN SPEED_X
	public IEnumerator moveTheCubes(){

		while(exeCuteTimer)
		{
			moveCube2();
			yield return new WaitForSeconds(speed_x);
		}
	}

	public void moveCube2(){

		//print ("MOVE CUBES..");
		Vector3 position = new Vector3(transform.position.x - x_offset, transform.position.y, transform.position.z);
		transform.position = position;
	}


}
