using UnityEngine;
using System.Collections;

public class Scenery : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col)
	{
		if (col.gameObject.tag == "Projectile" || col.gameObject.tag == "PlayerShot") 
		{
			Destroy (col.gameObject);
		}
	}
}
