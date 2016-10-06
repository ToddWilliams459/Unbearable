using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float projectileSpeed= -2f;
	public float damage= 25f;
	private float initial;

	// Use this for initialization
	void Start () {
	initial = this.transform.position.x;


	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (projectileSpeed * Time.deltaTime, 0, 0);
		if(Mathf.Abs(initial - this.transform.position.x) >= 25)
		   {
			Destroy (this.gameObject);
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Scenery")
		{
			Destroy(this.gameObject);
			//gameObject.SetActive(false);
		}
		//check if player lands on platform
		
		//if true col parent = this transform
	}



}
