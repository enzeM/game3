using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public static Spawner Instace;

	public GameObject[] cubes;
	public GameObject   parentOfGroup;

	public int preCreateNumOfCubes = 10;
	private GameObject[] 	createdCubes;


	void Awake() {

		Instace = this;
	}

	// Use this for initialization
	void Start () {

		CreateCubes();

	}
	
	// Update is called once per frame
	void Update () {

		if( parentOfGroup.transform.position.x < -15 )
		{
			// Every turn, the player must hit at least one cube! If not the Game is Over .. 
			if( parentOfGroup.transform.childCount == preCreateNumOfCubes ){

				GameManager.Instance.SetGameIsOver(true); // Tell the GameManager that the Game is Over..
			}

			// Prepare new Row.. 
			DeleteOldCubes();
			MoveCubes.Instance.changeSpeed(); // Make the Cubes faster..
			parentOfGroup.transform.position   = new Vector3(7, parentOfGroup.transform.position.y, parentOfGroup.transform.position.z);
			CreateCubes   ();
		}


	}


	public void Reset()
	{
		parentOfGroup.transform.position   = new Vector3(7, parentOfGroup.transform.position.y, parentOfGroup.transform.position.z);
		DeleteOldCubes ();
		CreateCubes    ();
	}


	void CreateCubes(){

		float posx = 0f;
		float posy = -0.2510071f;
		float posz = -3.170543f;
		
		//Create Array.. 
		createdCubes = new GameObject[preCreateNumOfCubes];

		//Create Cubes..
		for( int i = 0; i< preCreateNumOfCubes; i ++ )
		{
			GameObject newCube = (GameObject)Instantiate(cubes[(int)Random.Range(0,cubes.Length)]); 
			newCube.transform.parent        = parentOfGroup.transform;
			newCube.transform.localPosition = new Vector3(posx, posy, posz); // MOVE TO POS.. LOCAL BECAUSE IT IS IN A GROUP..
			createdCubes[i]    				= newCube;
			
			posx += 1;
		}

	}

	void DeleteOldCubes(){

		for(int i = 0; i< createdCubes.Length; i++ )
		{
			Destroy( createdCubes[i] );
		}
	}
}
