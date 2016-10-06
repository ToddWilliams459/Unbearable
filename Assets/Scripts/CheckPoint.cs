using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public Transform levelStart;
	public GameObject manager;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D user)
	{
		if (user.gameObject.tag == "Player")
		{
			Debug.Log("Player touched checkpoint");
			manager.GetComponent<LevelManager>().currentCheckpoint = this.transform;
		}
	}
}
