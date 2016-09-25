using UnityEngine;
using System.Collections;

public class NPCInteractionController : MonoBehaviour {

	public GameObject NPC;
	public PlayerMotion player;
	public bool isTalkable;
	public Animator npcAnim;
	public TextBoxManager textBox;
	Animator textAnim;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerMotion> ();
		npcAnim = NPC.GetComponent<Animator> ();
		textAnim = GameObject.FindGameObjectWithTag("TextBox").GetComponent<Animator>();
		isTalkable = false;
	}
	
	// Update is called once per frame
	void Update () {
		//If there is no dialogue and player is in range of npc
		if (textAnim.GetBool("dialogueOver") == true) {
			if (isTalkable) {
				//If player confirms in range of npc
				if (Input.GetKeyDown (Constants.CONFIRM)) {
					player.canMove = false;
					turnNPCToPlayer ();
					textBox.enabled = true;
					textBox.EnableTextBox ();
				}
			}
		}
		//If dialogue is over and npc is a moving npc, move npc
		if (textAnim.GetBool("dialogueOver") == true) {
			if (NPC.GetComponent<NPCMotion> () != null) {
				NPC.GetComponent<NPCMotion> ().canMove = true;
				player.canMove = true;
			}
		} 
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			isTalkable = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			isTalkable = false;
			textBox.enabled = false;
		}
	}
	public void turnNPCToPlayer(){
		Animator playerAnim = player.GetComponent<Animator> ();
		//Pause NPC
		//If NPC is a moving NPC
		if(NPC.GetComponent<NPCMotion>() != null){
			NPC.GetComponent<NPCMotion> ().canMove = false;
		}
		//Set NPC facing opposite of player
		npcAnim.SetFloat ("input_x", playerAnim.GetFloat ("input_x")/-1);
		npcAnim.SetFloat ("input_y", playerAnim.GetFloat ("input_y")/-1);
	}
}
