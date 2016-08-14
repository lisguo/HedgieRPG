using UnityEngine;


public class EnemyStateMachine : MonoBehaviour
{
	public Enemy enemy;

	//For progress bar
	float curr_cooldown = 0f;
	float max_cooldown = 5f;

	public enum TurnState
	{
		PROCESSING,
		ADDTOLIST,
		WAITING,
		SELECTING,
		ACTION,
		DEAD
	}

	public TurnState currState;


	void Start()
	{
		currState = TurnState.PROCESSING;

	}


	void Update()
	{
		switch (currState) {
			case (TurnState.PROCESSING):
				Debug.Log( enemy.name + "PROCESSING");
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

	void UpdateProgressBar()
	{
		curr_cooldown = curr_cooldown + Time.deltaTime;
		if (curr_cooldown >= max_cooldown) {
			currState = TurnState.ADDTOLIST;
		}
	}

}

