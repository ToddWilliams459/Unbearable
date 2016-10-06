using UnityEngine;
using System.Collections;

public class MovingPlat : MonoBehaviour {

	public Transform origin;
	public Transform destination;
	private Transform myTransform;
	private bool moveRight = false;
	public float speed = 5f;


	// Use this for initialization
	void Start () {
		myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (myTransform.position == origin.position)
		{
			moveRight= true;

		}

		if(moveRight==true && myTransform.position != destination.position)
		{
			myTransform.position = Vector3.MoveTowards(myTransform.position,destination.position, speed * Time.deltaTime);
		}

		 if (myTransform.position == destination.position)
		{
			moveRight=false;
		}

		if (moveRight==false && myTransform.position != origin.position)
		{
			myTransform.position = Vector3.MoveTowards(myTransform.position,origin.position, speed * Time.deltaTime);
		}
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.transform.parent = this.transform;
			//gameObject.SetActive(false);
		}
		//check if player lands on platform

		//if true col parent = this transform
	}

	void OnCollisionExit2D(Collision2D col)
	{
		//if player leaves
		if (col.gameObject.tag == "Player")
		{
			col.transform.parent = null;
		}
		// col parent = null
	}
}
