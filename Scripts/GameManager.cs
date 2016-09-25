using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public enum GameState
	{
		OPENWORLD,
		PAUSED,
		CUTSCENE,
		END_CUTSCENE,
		START_BATTLE,
		IN_BATTLE
	}

	public PauseMenuController pmc;
	public GameObject battleSystem;
	public GameState currState = GameState.OPENWORLD;

	void Update(){
		switch(currState){
			case (GameState.OPENWORLD):
				Time.timeScale = 1;
				CheckIfPaused();
				break;
			case (GameState.PAUSED):
				Debug.Log("GAME PAUSED");
				StopTime();
				CheckIfUnpaused();
				break;
			case (GameState.CUTSCENE):
				DisableMovement();
				break;
			case (GameState.END_CUTSCENE):
				EnableMovement();
				break;
			case (GameState.START_BATTLE): 
				DisableMovement();
				PrepareBattle();
				currState = GameState.IN_BATTLE;
				break;
			case (GameState.IN_BATTLE):
				//Debug.Log("IN BATTLE");
				break;
		}
	}

	void PrepareBattle(){
		//Disable pause menu controller
		pmc.enabled = false;

		//Enable battle system
		battleSystem.SetActive(true);

	}

	void StopTime(){
		Time.timeScale = 0;
	}

	void CheckIfPaused(){
		if(pmc.getState() > -1){
			currState = GameState.PAUSED;
		}
	}

	void CheckIfUnpaused(){
		if(pmc.getState() < 0){
			currState = GameState.OPENWORLD;
		}
	}
	void DisableMovement(){
		FindObjectOfType<PlayerMotion>().canMove = false;
	}

	void EnableMovement(){
		FindObjectOfType<PlayerMotion>().canMove = true;
	}
}

