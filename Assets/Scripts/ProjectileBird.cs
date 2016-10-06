using UnityEngine;
using System.Collections;

public class ProjectileBird : MonoBehaviour {

	public GameObject Projectile;
	public GameObject startPos;
	public float speed=2;
	private Transform birdTransform;

	public Transform start, floorCheck,findPlayer;
	public LayerMask groundLayer,playerLayer;
	public float FireRate=1;
	public bool facingRight=false;
	public float damageValue;
	public int enemyHp;

	private float _canFireIn;

	// Use this for initialization
	void Start () {

		birdTransform = this.transform;

	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHp == 0) {
						Destroy (this.gameObject);
				} else {
						_canFireIn -= Time.deltaTime;
						//this.transform.position  += new Vector3 (-speed * Time.deltaTime, 0, 0);

						RaycastHit2D Walk = Physics2D.Linecast (start.position, floorCheck.position, groundLayer.value);


						if (Walk.collider == null) {
								if (facingRight == false) {
										birdTransform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
								} else if (facingRight == true) {
										birdTransform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
								}
						} else if (Walk.collider != null) {
								if (facingRight == false) {
										birdTransform.rotation = Quaternion.Euler (0, 180, 0);
								} else if (facingRight == true) {
										birdTransform.rotation = Quaternion.Euler (0, 0, 0);
								}
								facingRight = !facingRight;
						}

						RaycastHit2D shoot = Physics2D.Linecast (start.position, findPlayer.position, playerLayer.value);
						if (shoot.collider == null) {

						} else if (shoot.collider != null) {
								if ((_canFireIn -= Time.deltaTime) > 0) {
								} else {
										FireProjectile ();
										Debug.Log ("playerFound");
										_canFireIn = FireRate;
								}
						}
				}




	
	}

	private void FireProjectile()
	{

		GameObject clone= GameObject.Instantiate (Projectile,startPos.transform.position, Quaternion.identity) as GameObject;
		//clone.AddComponent<Rigidbody2D> (); 
		//clone.rigidbody2D.isKinematic = true;
		if (facingRight) {
						clone.GetComponent<Projectile> ().projectileSpeed = 10f;

				} else {
						clone.GetComponent<Projectile> ().projectileSpeed = -10f;
				}
		clone.GetComponent<Projectile> ().damage = damageValue;

		if (clone.transform.position.x > 5) {
			Debug.Log("Hi hater");
		}

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "PlayerShot") {
			Destroy(coll.gameObject);
			enemyHp -=1;
				}

	}
}
