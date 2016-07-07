using UnityEngine;
using System.Collections;

public class Event1_Errands : NPCInteractionController
{
	public EventObject theEvent;

	// Use this for initialization
	void Start ()
	{
		base.player = FindObjectOfType<PlayerMotion> ();
		base.npcAnim = NPC.GetComponent<Animator> ();
		isTalkable = false;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If player is in range of NPC
		if (isTalkable) {
			//If player confirms in range of npc
			if(Input.GetKeyDown(Constants.CONFIRM)){
				turnNPCToPlayer ();
				NPC.GetComponent<TextBoxManager> ().enabled = true;
			}
		}
	}
		
	void OnTriggerExit2D(){
		base.isTalkable = false;
		NPC.GetComponent<TextBoxManager> ().enabled = false;
		theEvent.endEvent();
	}
}

