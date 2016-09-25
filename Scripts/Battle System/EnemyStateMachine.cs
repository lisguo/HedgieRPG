using UnityEngine;
using System.Collections;

public class EnemyStateMachine : MonoBehaviour
{
	public Enemy enemy;

	private BattleStateMachine BSM;



	//For progress bar
	float curr_cooldown = 0f;
	float max_cooldown = 5f;

	//Save positions
	private Vector2 startPosition;

	//Vars for action
	private bool actionStarted = false;

	private GameObject targetToAttack;

	public enum TurnState
	{
		PROCESSING,
		CHOOSEACTION,
		WAITING,
		SELECTING,
		ACTION,
		DEAD
	}

	public TurnState currState;
	private float animSpeed = 5f;

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
				UpdateProgressBar();
				break;

			case (TurnState.CHOOSEACTION):
				ChooseAction();
				currState = TurnState.WAITING;
				break;

			case (TurnState.WAITING):
				//idle
				break;

			case (TurnState.ACTION):
				StartCoroutine(TimeForAction());
				break;

			case (TurnState.DEAD):

				break;

		}
	}

	void UpdateProgressBar()
	{
		curr_cooldown = curr_cooldown + Time.deltaTime;
		if (curr_cooldown >= max_cooldown) {
			//currState = TurnState.CHOOSEACTION; //UNCOMMENT LATER
		}
	}

	void ChooseAction(){
		HandleTurn attack = new HandleTurn();
		attack.AttackerName = enemy.name;
		attack.Attacker = this.gameObject;

		//Attack random party member
		attack.Receiver = BSM.partyList[Random.Range(0, BSM.partyList.Count)];
		BSM.CollectActions(attack);

	}

	private IEnumerator TimeForAction(){
		if(actionStarted){
			yield break;
		}
		actionStarted = true;

		//Animate enemy to attack
		Vector2 targetPosition = new Vector2(targetToAttack.transform.position.x, 
		                                     targetToAttack.transform.position.y);

		while(MoveTowardsEnemy(targetPosition)){
			yield return null;
		}


		//wait


		//do damage


		//animate back to start position

		//Remove this action from list

		//Reset battle state machine back to wait

		actionStarted = false;
		//Reset enemy state 
		curr_cooldown = 0f;
		currState = TurnState.PROCESSING;
	}

	private bool MoveTowardsEnemy(Vector3 target){
		return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
	}

}

