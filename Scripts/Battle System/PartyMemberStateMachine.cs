using System;
using UnityEngine;
using UnityEngine.UI;


public class PartyMemberStateMachine : MonoBehaviour
{
	public PartyMember partyMember;
	public Image progressBar;

	//For progress bar
	float curr_cooldown = 0f;
	float max_cooldown = 5f;

	public enum TurnState{
		PROCESSING,
		ADDTOLIST,
		WAITING,
		SELECTING,
		ACTION,
		DEAD
	}

	public TurnState currState;


	void Start(){
		currState = TurnState.PROCESSING;

	}


	void Update(){
		switch(currState){
			case (TurnState.PROCESSING):
				Debug.Log(partyMember.name + " PROCESSING");
				UpdateProgressBar();
				break;

			case (TurnState.ADDTOLIST):

				break;

			case (TurnState.WAITING):

				break;

			case (TurnState.ACTION):

				break;

			case (TurnState.DEAD):

				break;

		}
	}

	void UpdateProgressBar(){
		curr_cooldown = curr_cooldown + Time.deltaTime;
		float calc_cooldown = curr_cooldown / max_cooldown;
		progressBar.transform.localScale = new Vector2(Mathf.Clamp(calc_cooldown, 0, 1), progressBar.transform.localScale.y);
		if(curr_cooldown >= max_cooldown){
			currState = TurnState.ADDTOLIST;
		}
	}


}

