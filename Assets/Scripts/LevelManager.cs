using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public Transform spawn;
	public Transform currentCheckpoint;
	private GameObject player = null;
	public GameObject playerType;

	public float healthTrack;
	private float playerLives;
	public float maxLives;
	public float score;
	public bool instaDeath = false;

	// Use this for initialization
	void Start () {
		playerLives = maxLives;

		 if (player == null) {
						spawnPlayer ();
				} else {
			Debug.Log("Player already exists");
				}   


	}
	
	// Update is called once per frame
	void Update () {
		healthTrack = player.GetComponent<BearNate> ().currentHealth;

		if (healthTrack <= 0) 
		{
			playerDeath();
			if(playerLives > 0)
			{
				spawnPlayer();
				playerLives -=1;
				Debug.Log(playerLives);
			}
			else
			{
				currentCheckpoint = spawn;
				playerLives = maxLives;
				spawnPlayer ();
			}

		}
		else{

		}
		if (instaDeath) {
			playerDeath();
			if(playerLives > 0)
			{
				spawnPlayer();
				playerLives -=1;
				instaDeath=false;
				Debug.Log(playerLives);
			}
			else
			{
				currentCheckpoint = spawn;
				playerLives = maxLives;
				spawnPlayer ();
				instaDeath=false;
			}

				}
	
	}

	void spawnPlayer(){
		player = GameObject.Instantiate (playerType,currentCheckpoint.transform.position, Quaternion.identity) as GameObject;
	}

	void playerDeath(){
		Destroy (player.gameObject);
	}


}
