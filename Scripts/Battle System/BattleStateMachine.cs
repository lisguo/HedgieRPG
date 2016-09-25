using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour{
	public enum PerformAction{
		WAIT,
		TAKEACTION,
		PERFORMACTION
	}

	public PerformAction battleStates;

	public List<PartyMemberStateMachine> partyPerformList = new List<PartyMemberStateMachine>();
	public List<HandleTurn> performList = new List<HandleTurn>();
	public List<GameObject> partyList = new List<GameObject>();
	public List<GameObject> enemyList = new List<GameObject>();

	//Game Objects
	public GameObject menuButtons;
	public GameObject statusPane;

	public string actionSelected;



	void Start(){
		battleStates = PerformAction.WAIT;
		enemyList.AddRange(GameObject.FindGameObjectsWithTag("EnemyInBattle"));
		partyList.AddRange(GameObject.FindGameObjectsWithTag("PartyMemberInBattle"));
		menuButtons.SetActive(false);
		actionSelected = "";
	}

	void Update(){
		switch(battleStates){
			case (PerformAction.WAIT):
				Debug.Log("WAITING FOR ACTION");
				if(performList.Count > 0){
					//If there is something in the list
					battleStates = PerformAction.TAKEACTION;
				}
				checkIfUserIsSelecting();
     
				HighlightSelected();
				break;
				
			case (PerformAction.TAKEACTION):
				Debug.Log("TAKE ACTION");
				StartCoroutine(ProcessNextTurn());
				battleStates = PerformAction.WAIT;
				checkIfUserIsSelecting();
				HighlightSelected();
				break;
		}
	}

	public IEnumerator ProcessNextTurn(){
		HandleTurn currTurn = performList[0];
		performList.RemoveAt(0);

		//Start animation
		Animation animation = currTurn.Attacker.gameObject.GetComponent<Animation>();
		AnimationClip clipToPlay = animation.clip;
		if(currTurn.turnType == "Attack"){
			clipToPlay = animation.GetClip("attack");
			animation.clip = clipToPlay;
			animation.Play();
			Debug.Log("Playing animation!");
		}
		while(animation.isPlaying){
			yield return null;
		}
		Debug.Log("Animation stopped");


	}
	public void ActionSelected(){
		//Get giver and remove it from list
		PartyMemberStateMachine giver = partyPerformList[0];
		partyPerformList.RemoveAt(0);

		//Get button infoo
		GameObject buttonObject = EventSystem.current.currentSelectedGameObject;


		//Get receiver
		string receiverName  = buttonObject.GetComponentInChildren<Text>().text;

		if(actionSelected == "Attack"){
			EnemyStateMachine receiver = getEnemyByName(receiverName);
			if(receiver == null){
				return;
			}
			Debug.Log("Receiver: " + receiver.name);

			HandleTurn turn = new HandleTurn();
			turn.Attacker = giver.gameObject;
			turn.Receiver = receiver.gameObject;
			turn.AttackerName = giver.partyMember.name;
			turn.turnType = "Attack";

			//Add turn to perform listt
			performList.Add(turn);
			Debug.Log("ADDED TO PERFORM LIST");
		}

		//Make party member wait againn
		giver.currState = PartyMemberStateMachine.TurnState.RESET;

		//Reset menu
		ResetMenu();

	}

	public void ResetMenu(){
		GameObject buttonPlane = menuButtons.GetComponentInParent<Transform>().gameObject;
		GameObject[] battleMenusToDisable = GameObject.FindGameObjectsWithTag("BattleMenuBranch");
		//Disable menus
		foreach(GameObject go in battleMenusToDisable){
			go.SetActive(false);
		}
	}
	public EnemyStateMachine getEnemyByName(string name){
		foreach(GameObject go in enemyList){
			EnemyStateMachine enemy = go.GetComponent<EnemyStateMachine>();
			string enemyName = enemy.enemy.name;
			if(enemyName == name){
				return enemy;
			}
		}
		return null;
	}

	void checkIfUserIsSelecting(){
		if (partyPerformList.Count > 0) {
			partyPerformList[0].currState = PartyMemberStateMachine.TurnState.SELECTING;
			//Enable menu if there is a party perform list and menu is inactive
			if(menuButtons.activeSelf == false){
				menuButtons.SetActive(true);
			}
		} else {
			menuButtons.SetActive(false);
		}
	}

	void HighlightSelected(){
		for (int i = 0; i < partyList.Count; i ++){
			PartyMemberStateMachine pm = partyList[i].GetComponent<PartyMemberStateMachine>();
			if(pm.isSelected){
				statusPane.transform.GetChild(i).GetComponent<Image>().enabled = true;
			}
			else{
				statusPane.transform.GetChild(i).GetComponent<Image>().enabled = false;;
			}
		}
	}


	public void CollectActions(HandleTurn input){
		performList.Add(input);
	}
}
