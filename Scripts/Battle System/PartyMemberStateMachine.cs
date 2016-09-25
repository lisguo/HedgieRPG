using System;
using UnityEngine;
using UnityEngine.UI;


public class PartyMemberStateMachine : MonoBehaviour
{
	public PartyMember partyMember;
	public Image progressBar;

	private BattleStateMachine BSM;

	private Vector2 startPosition;

	//For progress bar
	float curr_cooldown = 0f;
	float max_cooldown = 5f;

	public bool isSelected = false;

	public enum TurnState
	{
		PROCESSING,
		ADDTOLIST,
		WAITING,
		SELECTING,
		RESET,
		ACTION,
		DEAD
	}

	public TurnState currState;


	void Start()
	{
		currState = TurnState.PROCESSING;
		BSM = GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>();
		startPosition = transform.position;
	}


	void Update()
	{
		switch (currState) {
			case (TurnState.PROCESSING):
				Debug.Log(partyMember.name + " PROCESSING");
				UpdateProgressBar();
				break;

			case (TurnState.ADDTOLIST):
				Debug.Log(partyMember.name + " ADDTOLIST");
				AddToPartyPerformList();
				break;

			case (TurnState.WAITING):
				Debug.Log(partyMember.name + " WAITING");
				break;

			case (TurnState.SELECTING):
				Debug.Log(partyMember.name + " SELECTING");
				SetSelected();
				break;

			case (TurnState.RESET):
				Debug.Log(partyMember.name + " RESET");
				ResetProgressBar();
				Unselect();
				break;

			case (TurnState.DEAD):

				break;

		}
	}

	void SetSelected(){
		isSelected = true;
	}

	void Unselect(){
		isSelected = false;
	}

	void ResetProgressBar(){
		progressBar.transform.localScale = new Vector2(0, progressBar.transform.localScale.y);
		curr_cooldown = 0f;
		currState = TurnState.PROCESSING;
	}

	void UpdateProgressBar()
	{
		curr_cooldown = curr_cooldown + Time.deltaTime;
		float calc_cooldown = curr_cooldown / max_cooldown;
		progressBar.transform.localScale = new Vector2(Mathf.Clamp(calc_cooldown, 0, 1), progressBar.transform.localScale.y);
		if (curr_cooldown >= max_cooldown) {
			currState = TurnState.ADDTOLIST;
		}
	}

	void AddToPartyPerformList()
	{
		BSM.partyPerformList.Add(this);
		currState = TurnState.WAITING;
	}
}

