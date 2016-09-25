using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject inventoryMenu;
	public GameObject statusMenu;
	public GameObject statusScreen;
	public GameObject itemActionScreen;

	public static GameObject currentMenu;
	GameObject nextMenu;

	public static int state;
	/* Pause Menu not open : -1
	 * Main menu : 0
	 * Inventory/Equip/etc : 1
	 * */
	// Use this for initialization
	void Start () {
		state = -1;
	}

	// Update is called once per frame
	void Update () {
		//When player presses menu button, pause and show menu
		if (Input.GetKeyDown (Constants.MENU)) {
			//Pause

			//show/close menu
			if (state == -1) {
				pauseMenu.SetActive (!pauseMenu.activeSelf);
				state = 0;
			} else if(state == 0){
				pauseMenu.SetActive (!pauseMenu.activeSelf);
				state = -1;
					
			} else if (state == 1) {
				state = 0;
				currentMenu.SetActive (false);
			}
			else if(state == 2){
				state = 1;
				if(currentMenu == statusScreen){
					nextMenu = statusMenu;
				}
				else if(currentMenu == itemActionScreen){
					nextMenu = inventoryMenu;
				}
				currentMenu.SetActive(false);
				currentMenu = nextMenu;
			}

			Debug.Log("Pause state: " + state);
		}
	}

	public int getState(){
		return state;
	}

	public static void setCurrentMenu(GameObject menu){
		currentMenu = menu;
	}

	public void backButtonOnItemAction(){
		state = 1;
		currentMenu.SetActive(false);
		currentMenu = inventoryMenu;
		//Set buttons interacteable
		inventoryMenu.GetComponent<InventoryMenu>().interactButtons();
	}
}
