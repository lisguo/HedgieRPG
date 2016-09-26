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
	public GameObject partyListObject;
	public GameObject enemyListObject;

	public string actionSelected;



	void Start(){
		battleStates = PerformAction.WAIT;

		//Initialize party and enemy lists
		EnemyStateMachine[] enemies = enemyListObject.GetComponentsInChildren<EnemyStateMachine> ();
		for(int i = 0; i < enemies.Length; i++) {
			enemyList.Add(enemies [i].gameObject);
		}
		PartyMemberStateMachine[] partyMembers = partyListObject.GetComponentsInChildren<PartyMemberStateMachine> ();
		for(int i = 0; i < partyMembers.Length; i++) {
			partyList.Add(partyMembers [i].gameObject);
		}

		menuButtons.SetActive(false);
		actionSelected = "";
	}

	void Update(){
		switch(battleStates){
			case (PerformAction.WAIT):
				//Debug.Log("WAITING FOR ACTION");
				if(performList.Count > 0){
					//If there is something in the list
					battleStates = PerformAction.TAKEACTION;
				}
				checkIfUserIsSelecting();
     
				HighlightSelected();
				break;
				
			case (PerformAction.TAKEACTION):
				//Debug.Log("TAKE ACTION");
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

			//Handle Attack
			HandleAttack(currTurn.Attacker, currTurn.Receiver);
		}
		while(animation.isPlaying){
			yield return null;
		}
		//Debug.Log("Animation stopped");


	}

	void HandleAttack(GameObject attacker, GameObject receiver){
		if(attacker.GetComponent<PartyMemberStateMachine>() != null){
			//Attacker is party member
			PartyMember pm = attacker.GetComponent<PartyMemberStateMachine>().partyMember;
			int attack = pm.getAttack();

			Enemy enemy = receiver.GetComponent<EnemyStateMachine>().enemy;
			int defense = (int)enemy.currDEF;
			int netLevel = pm.level - enemy.level;

			int damage = CalculateDamage(attack, defense, netLevel);
			Debug.Log("Damage: " + damage);

			receiver.GetComponent<EnemyStateMachine>().enemy.currHP -= damage;
			//SHOW HP BAR ANIMATION
			ShowHPBarAndDamage(receiver, damage);
		}
	}

	void ShowHPBarAndDamage(GameObject receiver, int damage){
		//Set damage and play damage animation
		GameObject damageObject = receiver.transform.FindChild("Damage Dealt").gameObject;
		Animation damageAnim = damageObject.GetComponent<Animation> ();
		damageAnim.clip = damageAnim.GetClip ("ShowDamage");
		Debug.Log ("CLIP: "+ damageAnim.clip);
		damageAnim.Play ();
		Debug.Log ("Playing animation");

		//Show HP Bar
		GameObject HPBar = receiver.transform.FindChild("HP Bar").gameObject;
		HPBar.SetActive (true);
		Image HPFill = HPBar.GetComponentsInChildren<Image> () [1];

		//Calculate percentage of currHP
		EnemyStateMachine esm = receiver.GetComponent<EnemyStateMachine>();
		if (esm != null) {
			HPFill.fillAmount = esm.enemy.currHP / esm.enemy.baseHP;
		} else {
			PartyMemberStateMachine psm = receiver.GetComponent<PartyMemberStateMachine>();
			HPFill.fillAmount = psm.partyMember.hp / psm.partyMember.fullHP;
		}
	}

	int CalculateDamage(int attack, int defense, int netLevel){
		int levelCalc = netLevel;
		if(netLevel < 0){
			levelCalc = 1;
		}

		int damage = ((attack * netLevel) / 250 - (defense * netLevel) / 250) * (int)Random.Range(1, 1.5f);
		return damage;
	}
	public void ActionSelected(){
		//Get giver and remove it from list
		PartyMemberStateMachine giver = partyPerformList[0];
		partyPerformList.RemoveAt(0);

		//Get button infoo
		GameObject buttonObject = EventSystem.current.currentSelectedGameObject;


		//Get receiver
		int receiverIndex = -1;
		Button[] buttonList = GameObject.FindGameObjectWithTag ("Attack Choices").GetComponentsInChildren<Button> ();
		//Debug.Log (buttonObject.name + " SELECTED");
		for(int i = 0; i < buttonList.Length; i++){
			if (buttonList[i].gameObject == buttonObject) {
				receiverIndex = i;
				//Debug.Log ("INDEX: " + i);
			}
		}
		//Debug.Log (enemyList [receiverIndex].name);
		if(actionSelected == "Attack"){
			EnemyStateMachine receiver = enemyList[receiverIndex].GetComponent<EnemyStateMachine>();
			if(receiver == null){
				return;
			}
			//Debug.Log("Receiver: " + receiver.name);

			HandleTurn turn = new HandleTurn();
			turn.Attacker = giver.gameObject;
			turn.Receiver = receiver.gameObject;
			turn.AttackerName = giver.partyMember.name;
			turn.turnType = "Attack";

			//Add turn to perform listt
			performList.Add(turn);
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

	public PartyMemberStateMachine getPartyMemberByName(string name){
		foreach(GameObject go in partyList){
			PartyMemberStateMachine partyMember = go.GetComponent<PartyMemberStateMachine>();
			string partyName = partyMember.partyMember.characterName;
			if(partyName == name){
				return partyMember;
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
