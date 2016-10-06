using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {

	public float scoreValue;
	private float scoreTrack;
	public GameObject manager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coin)
	{
		if (coin.gameObject.tag == "Player") 
		{
			Destroy (this.gameObject);
			manager.GetComponent<LevelManager>().score += scoreValue;
			Debug.Log(manager.GetComponent<LevelManager>().score);
		}
	}
}
