using UnityEngine;
using System.Collections;

public class NPCMotion : MonoBehaviour {


	//Private vars
	Rigidbody2D rbody;
	public GameObject NPC;
	private Animator anim;
	public bool canMove;

	private Vector2 movement_vector = new Vector2(0,0);
	private bool change_direction = true;
	//SPEED
	float npc_speed = 0.01f;


	// Use this for initialization
	void Start () {
		rbody = NPC.GetComponent<Rigidbody2D> ();
		anim = NPC.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!canMove) {
			//Stop NPC from moving
			anim.SetBool ("isWalking", false);
			movement_vector = Vector2.zero;
			rbody.constraints = RigidbodyConstraints2D.FreezeAll;
		} else {
			rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
			if (change_direction) {
				StartCoroutine (changeDirection ());
			}
			if (movement_vector != Vector2.zero) {
				anim.SetBool ("isWalking", true);
				anim.SetFloat ("input_x", movement_vector.x);
				anim.SetFloat ("input_y", movement_vector.y);
			} else {
				anim.SetBool ("isWalking", false);
			}
			rbody.MovePosition (rbody.position + movement_vector * npc_speed);
		}
	
	}

	IEnumerator changeDirection(){
		change_direction = false;
		movement_vector = new Vector2(Random.Range(-1,2), Random.Range(-1,2));
		//Debug.Log ("direction changed");
		yield return new WaitForSeconds (2);
		change_direction = true;
	}

}
