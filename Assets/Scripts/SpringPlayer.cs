using UnityEngine;
using System.Collections;

public class SpringPlayer : MonoBehaviour {

	private BearNate player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
				if (col.gameObject.tag == "Player") {
						col.transform.position += new Vector3 (0, 5, 0);
						//gameObject.SetActive(false);
				}
		}
}
