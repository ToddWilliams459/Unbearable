using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour {
	public GameObject manager;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			manager.GetComponent<LevelManager>().instaDeath = true;
				}
	}


}
