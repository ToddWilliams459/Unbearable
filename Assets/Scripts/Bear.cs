using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour
{
	private Rigidbody2D bearBody;
	private Animator anim;
	public float speed;
	private bool Face=true;
	private bool canJump;
	public Transform startPos,endPos,endPosR,endPosL;
	public LayerMask groundLayer;

	// Use this for initialization
	void Start ()
	{
		bearBody = this.rigidbody2D;
		anim = GetComponent<Animator> ();
	}


	// Update is called once per frame
	void Update ()
	{
				//everytime update is called the current parameter is set to equal our in code bool
				anim.SetBool ("FacingRight", (Face));

				float move = Input.GetAxisRaw ("Horizontal");
				//play mechanim run animation
				//anim.SetFloat ("Speed",Mathf.Abs (move));

				//The speed parameter is equal to our move which getaxis raw returns either 1,0, or -1 value
				anim.SetFloat ("Speed", (move));
				bearBody.velocity = new Vector2 (move * speed, bearBody.velocity.y);
				// turn 180 degrees along the y axis
				//transform.rotation = Quaternion.Euler (0, 180, 0);

				// code below sets our in code bool to decide whether the parameter above is changed on each update call
				if (Face == true && move < 0) {
						Face = false;
		
				} else if (Face == false && move > 0) {
						Face = true;
				}

				//bear jumping code

				RaycastHit2D hitInfo = Physics2D.Linecast (startPos.position, endPos.position, groundLayer.value);
				RaycastHit2D hitInfoR = Physics2D.Linecast (startPos.position, endPosR.position, groundLayer.value);
				RaycastHit2D hitInfoL = Physics2D.Linecast (startPos.position, endPosL.position, groundLayer.value);
		
				if (hitInfo.collider != null)  //check if grounded
				{  
					Debug.Log ("HIT");
					canJump = true;
					anim.SetBool ("Jump", false);
					if (Input.GetKeyDown (KeyCode.Space) && canJump == true) //normal vertical jump code
					{  
						anim.SetBool ("Jump", true);
						bearBody.velocity = new Vector2 (bearBody.velocity.x, 10);
						canJump = false;
					}
				} 
				else if (hitInfo.collider == null) 
				{
					if (hitInfoR.collider != null && canJump == false &&  Input.GetKeyDown (KeyCode.LeftArrow)) 
					{
					Debug.Log ("right wall jump");
					bearBody.velocity = new Vector2 (bearBody.velocity.x, 10);
					
					}
				    else if (hitInfoL.collider != null && canJump == false &&  Input.GetKeyDown (KeyCode.RightArrow)) 
					{
						Debug.Log ("left wall jump");
						bearBody.velocity = new Vector2 (bearBody.velocity.x, 10);
						
					}

				}
	}
}
