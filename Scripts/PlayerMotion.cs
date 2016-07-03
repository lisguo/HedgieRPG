using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour {
	//Private vars
	Rigidbody2D rbody;
	Animator anim;
	public bool canMove;

	//SPEED
	float playerSpeed = 0.07f;

	// Use this for initialization
	void Start () {
	
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!canMove) {
			rbody.velocity = Vector2.zero;
			return;
		}
		Vector2 movement_vector = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		//Debug.Log ("MOVEMENT VECTOR: " + movement_vector);

		if(movement_vector != Vector2.zero){
			anim.SetBool("isWalking", true);
			anim.SetFloat ("input_x", movement_vector.x);
			anim.SetFloat ("input_y", movement_vector.y);
		}
		else{
			anim.SetBool ("isWalking", false);
		}
		//Debug.Log ("Moving by " + movement_vector * Time.deltaTime * 4);
		rbody.MovePosition (rbody.position + movement_vector * playerSpeed);
	}
}
