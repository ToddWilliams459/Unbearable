using UnityEngine;
using System.Collections;

public class BearNate : MonoBehaviour
{
	private Rigidbody2D bearBody;
	private Animator anim;
	public float speed;
	private bool Face = true;
	private bool canJump;
	public Transform startPos, endPos, endPosR, endPosL,endPosU;
	public LayerMask groundLayer,wallLayer;
	private int MAX_WALL_JUMPS = 4;
	private int wallJumps = 4;
	private int MAX_WALL_BUFFER = 20;
	private int onWallBuffer = 0;
	private bool onWall = false;
	private bool rJump,lJump =true;
	public GameObject roar;
	public float FireRate=1;
	private float _canFireIn;

	
	public float MaxHealth = 100;
	public float currentHealth {  get; private set;}
	//public int playerLives=3;

	
	
	// Use this for initialization
	void Start ()
	{
		bearBody = this.rigidbody2D;
		anim = GetComponent<Animator> ();
		currentHealth = MaxHealth;
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
		if(!onWall && onWallBuffer > 0) {
			//Debug.Log("Not on Wall");
			bearBody.velocity = new Vector2 (move * speed, bearBody.velocity.y);
		}
		// turn 180 degrees along the y axis
		//transform.rotation = Quaternion.Euler (0, 180, 0);
		
		// code below sets our in code bool to decide whether the parameter above is changed on each update call
		if (Face == true && move < 0) {
			Face = false;
		} else if (Face == false && move > 0) {
			Face = true;
		}
		
		//bear jumping code
		RaycastHit2D hitInfo = Physics2D.Linecast (startPos.position, endPos.position, groundLayer.value);  //ground
		RaycastHit2D hitInfoR = Physics2D.Linecast (startPos.position, endPosR.position, groundLayer.value); //right wall jump
		RaycastHit2D hitInfoL = Physics2D.Linecast (startPos.position, endPosL.position, groundLayer.value); // left wall jump
		RaycastHit2D hitInfoU = Physics2D.Linecast (startPos.position, endPosU.position, groundLayer.value); // head collider
		
		if (hitInfo.collider != null) {  //check if grounded  
			//Debug.Log("Ground");
			canJump = true;
			onWallBuffer = MAX_WALL_BUFFER;
			onWall = false;
			anim.SetBool ("Jump", false);
			wallJumps = MAX_WALL_JUMPS;
			if (Input.GetKeyDown (KeyCode.Space) && canJump == true) { //normal vertical jump code  
				anim.SetBool ("Jump", true);
				bearBody.velocity = new Vector2 (bearBody.velocity.x, 10);
				canJump = false;
				lJump=true;
				rJump=true;
			}
		} else if (hitInfo.collider == null) {
			//Debug.Log("In Air");
			//Debug.Log("WallJumps " + wallJumps);
			//Debug.Log("Hit top " + (hitInfoU.collider != null));
			//TODO Need to add a check to see if they have grabbed the ceiling and hitInfoU == null
			if(hitInfoU.collider != null) {
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					Debug.Log ("Ceiling Grab");
					//bearBody.velocity = new Vector2 (move * speed, 0);
					rigidbody2D.gravityScale = 0;
				} else if (Input.GetKeyUp (KeyCode.UpArrow) || hitInfoU == null) {
					rigidbody2D.gravityScale = 1.5f;
					
				}
			} else if (hitInfoR.collider != null) {
				if(onWallBuffer > 0)
					onWallBuffer--; //If we have room in buffer, decrease
				else
					onWall = true; //Else set to true
				if(canJump == false && wallJumps > 0 && Input.GetKeyDown (KeyCode.LeftArrow) && rJump==true) {
					Debug.Log ("right wall jump");
					bearBody.velocity = new Vector2 (bearBody.velocity.x, 10);
					wallJumps--;
					onWall = false;
					onWallBuffer = MAX_WALL_BUFFER; //Gotta let the bear move if we wall jumping
					rJump=false;
					lJump=true;
				}
			} else if (hitInfoL.collider != null) {
				if(onWallBuffer > 0)
					onWallBuffer--; //If we have room in buffer, decrease
				else
					onWall = true; //Else set to true
				if(canJump == false && wallJumps > 0 && Input.GetKeyDown (KeyCode.RightArrow) && lJump==true) {
					Debug.Log ("left wall jump");
					bearBody.velocity = new Vector2 (bearBody.velocity.x, 10);
					wallJumps--;
					onWall = false;
					onWallBuffer = MAX_WALL_BUFFER; //Gotta let the bear move if we wall jumping
					lJump=false;
					rJump=true;
				}
			}
			else {
				onWall = false;
			}
			
		}
		_canFireIn -= Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.B)) {
			if ((_canFireIn -= Time.deltaTime) > 0) 
			{

			} 
			else{
			if (Face) {
					GameObject clone = GameObject.Instantiate (roar, startPos.transform.position, Quaternion.identity) as GameObject;
					clone.GetComponent<Projectile> ().projectileSpeed = 10f;
					_canFireIn = FireRate;
							} 		
			else {
					GameObject clone = GameObject.Instantiate (roar, startPos.transform.position, Quaternion.Euler (0, 180, 0)) as GameObject;
					clone.GetComponent<Projectile> ().projectileSpeed = 10f;
					_canFireIn = FireRate;
				}
			}
											} 

		else {

				}
		//Debug.Log ("End " + onWallBuffer);
	}

	void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.gameObject.tag == "Projectile") {
			//Destroy(obj.gameObject);
			takeDamage (obj.gameObject);

				}
	}

	void takeDamage(GameObject hurtMe){
		float damageTaken = hurtMe.GetComponent<Projectile>().damage;
		currentHealth -= damageTaken;
		Destroy (hurtMe);
		Debug.Log (currentHealth);
	}


}

