using UnityEngine;
using System.Collections;

public class Event0_ChangeClothes : ObjectInteractionController {

	public EventObject theEvent;
	public GameObject player;
	public RuntimeAnimatorController nextAC;

	override public void action(){
		//CHANGE SPRITES HERE
		player.GetComponent<Animator>().runtimeAnimatorController = nextAC;


		ObjectInteractionController.showTextBox (base.Object); 
	}

	override public void OnTriggerExit2D2(){

		//DISABLE EVENT 0
		theEvent.endEvent();
	}
}
