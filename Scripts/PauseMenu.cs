using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public PartyManager partyManager;

	//Character Status Portraits
	public GameObject party_0;
	public GameObject party_1;
	public GameObject party_2;
	public GameObject party_3;

	//Character Status
	public Text stat_0;
	public Text stat_1;
	public Text stat_2;
	public Text stat_3;

	public Sprite pineconePortrait;
	public Sprite chestnutPortrait;
	public Sprite bunnyPortrait;
	public Sprite ennaPortrait;
	public Sprite skippyPortrait;
	public Sprite cielPortrait;


	//Menus
	public GameObject inventoryMenu;
	public GameObject statusMenu;

	//MAIN BUTTONS
	public void Items(){
		PauseMenuController.state = 1;
		inventoryMenu.SetActive (true);
		PauseMenuController.setCurrentMenu (inventoryMenu);
	}

	public void Status(){
		PauseMenuController.state = 1;
		statusMenu.SetActive(true);
		PauseMenuController.setCurrentMenu(statusMenu);
	}

	// Update is called once per frame
	void Update () {
		for(int i = 0; i < partyManager.party.Count && i < 4; i++) {
			showPortrait (i, partyManager.party [i].characterName);
		}
	}

	void showPortrait(int index, string characterName){
		if (index == 0) {
			//Show status
			stat_0.text = "LEVEL: " + partyManager.party[0].level + "\n"
				+ "HP: " + partyManager.party [0].hp + " / " + partyManager.party[0].fullHP + "\n"
				+ "MP: " + partyManager.party [0].mp + " / " + partyManager.party[0].fullMP + "\n"
				+ "EXP: " + partyManager.party [0].exp + "/" + partyManager.party [0].expToLevel;

			//Show portraits
			if (characterName == "Pinecone") {
				party_0.GetComponent<Image> ().sprite = pineconePortrait;
			} else if (characterName == "Chestnut") {
				party_0.GetComponent<Image> ().sprite = chestnutPortrait;
			}else if (characterName == "Bunny") {
				party_0.GetComponent<Image>().sprite = bunnyPortrait;
			}else if(characterName == "Enna"){
				party_0.GetComponent<Image>().sprite = ennaPortrait;
			}else if (characterName == "Skippy") {
				party_0.GetComponent<Image>().sprite = skippyPortrait;
			}else if (characterName == "Ciel") {
				party_0.GetComponent<Image>().sprite = cielPortrait;
			}
		}
		else if (index == 1) {
			//Show status
			stat_1.text = "LEVEL: " + partyManager.party[1].level + "\n"
				+ "HP: " + partyManager.party [1].hp + " / " + partyManager.party[1].fullHP + "\n"
				+ "MP: " + partyManager.party [1].mp + " / " + partyManager.party[1].fullMP + "\n"
				+ "EXP: " + partyManager.party [1].exp + "/" + partyManager.party [1].expToLevel;

			//Show portraits
			if (characterName == "Pinecone") {
				party_1.GetComponent<Image> ().sprite = pineconePortrait;
			} else if (characterName == "Chestnut") {
				party_1.GetComponent<Image> ().sprite = chestnutPortrait;
			}else if (characterName == "Bunny") {
				party_1.GetComponent<Image>().sprite = bunnyPortrait;
			}else if (characterName == "Enna") {
				party_1.GetComponent<Image>().sprite = ennaPortrait;
			}else if (characterName == "Skippy") {
				party_1.GetComponent<Image>().sprite = skippyPortrait;
			} else if (characterName == "Ciel") {
				party_1.GetComponent<Image>().sprite = cielPortrait;
			}
		}
		else if (index == 2) {
			//Show status
			stat_2.text = "LEVEL: " + partyManager.party[2].level + "\n"
				+ "HP: " + partyManager.party [2].hp + " / " + partyManager.party[2].fullHP + "\n"
				+ "MP: " + partyManager.party [2].mp + " / " + partyManager.party[2].fullMP + "\n"
				+ "EXP: " + partyManager.party [2].exp + "/" + partyManager.party [2].expToLevel;

			//Show portraits
			if (characterName == "Pinecone") {
				party_2.GetComponent<Image> ().sprite = pineconePortrait;
			} else if (characterName == "Chestnut") {
				party_2.GetComponent<Image> ().sprite = chestnutPortrait;
			}else if (characterName == "Bunny") {
				party_2.GetComponent<Image>().sprite = bunnyPortrait;
			}else if (characterName == "Enna") {
				party_2.GetComponent<Image>().sprite = ennaPortrait;
			}else if (characterName == "Skippy") {
				party_2.GetComponent<Image>().sprite = skippyPortrait;
			} else if (characterName == "Ciel") {
				party_2.GetComponent<Image>().sprite = cielPortrait;
			}
		}

		else if (index == 3) {
			//Show status
			stat_3.text = "LEVEL: " + partyManager.party[3].level + "\n"
				+ "HP: " + partyManager.party [3].hp + " / " + partyManager.party[3].fullHP + "\n"
				+ "MP: " + partyManager.party [3].mp + " / " + partyManager.party[3].fullMP + "\n"
				+ "EXP: " + partyManager.party [3].exp + "/" + partyManager.party [3].expToLevel;

			//Show portraits
			if (characterName == "Pinecone") {
				party_3.GetComponent<Image> ().sprite = pineconePortrait;
			} else if (characterName == "Chestnut") {
				party_3.GetComponent<Image> ().sprite = chestnutPortrait;
			}else if (characterName == "Bunny") {
				party_3.GetComponent<Image>().sprite = bunnyPortrait;
			}else if (characterName == "Enna") {
				party_3.GetComponent<Image>().sprite = ennaPortrait;
			}else if (characterName == "Skippy") {
				party_3.GetComponent<Image>().sprite = skippyPortrait;
			} else if (characterName == "Ciel") {
				party_3.GetComponent<Image>().sprite = cielPortrait;
			}
		}
	}
}
