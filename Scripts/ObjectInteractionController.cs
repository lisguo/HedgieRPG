using UnityEngine;
using System.Collections;

public abstract class ObjectInteractionController : MonoBehaviour {

	public GameObject Object;
	private bool isTalkable;

	// Use this for initialization
	void Start () {
		isTalkable = false;
	}

	// Update is called once per frame
	void Update () {
		//If player is in range of NPC
		if (isTalkable) {
			//If player confirms in range of npc
			if(Input.GetKeyDown(Constants.CONFIRM)){
				action ();
			}
		}
	}

	public abstract void action();
	//public abstract IEnumerator action();
	public abstract void OnTriggerExit2D2();
	public static void showTextBox(GameObject theObject){
		theObject.GetComponent<TextBoxManager> ().enabled = true;
	}
	void OnTriggerEnter2D(){
		isTalkable = true;
	}

	void OnTriggerExit2D(){
		isTalkable = false;
		Object.GetComponent<TextBoxManager> ().enabled = false;
		OnTriggerExit2D2 ();
	}
}
