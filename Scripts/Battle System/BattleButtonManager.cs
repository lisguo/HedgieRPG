using UnityEngine;
public class BattleButtonManager : MonoBehaviour{
	public GameObject AttackBranch;
	public GameObject MagicBranch;
	public GameObject ItemBranch;
	public BattleStateMachine bsm;

	public void Attack(){
		AttackBranch.SetActive(true);
		bsm.actionSelected = "Attack";
	}

	public void Magic(){
		MagicBranch.SetActive(true);
		bsm.actionSelected = "Magic";
	}

	public void Item(){
		ItemBranch.SetActive(true);
		bsm.actionSelected = "Item";
	}

}

