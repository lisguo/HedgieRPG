using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryMenu : MonoBehaviour
{
	public InventoryManager invManager;
	public PartyManager partyManager;

	public GameObject itemList;
	public GameObject itemSlot;
	public Font font;

	public GameObject partyList;
	public GameObject partySlot;

	List<GameObject> slots;
	List<GameObject> pSlots;

	// Use this for initialization
	void Start()
	{
		initializeInventory();
		initializePartyMembers();
	}

	// Update is called once per frame
	void Update()
	{

	}


	void initializePartyMembers()
	{
		pSlots = new List<GameObject>();
		List<PartyMember> party = partyManager.party;
		for (int i = 0; i < party.Count; i++)
		{
			//CREATE PARTY SLOTS
			GameObject slot = Instantiate(partySlot);

			//Set portrait
			Image[] images = slot.GetComponentsInChildren<Image>();
			images[1].sprite = getStatusPortrait(party[i].characterName);

			Text[] texts = slot.GetComponentsInChildren<Text>();
			texts[0].text = party[i].characterName;
			texts[1].text = "Level: " + party[i].level;
			texts[2].text = "HP: " + party[i].hp + " / " + party[i].fullHP;
			texts[3].text = "MP: " + party[i].mp + " / " + party[i].fullMP;
			texts[4].text = "EXP: " + party[i].exp + " / " + party[i].expToLevel;

			pSlots.Add(slot);
			pSlots[i].transform.SetParent(partyList.transform);
			pSlots[i].transform.localScale = new Vector2(1, 1);
		}
	}

	Sprite getStatusPortrait(string character)
	{
		if (character == "Pinecone")
		{
			return partyManager.pineconeStatus;
		}
		else if (character == "Chestnut")
		{
			return partyManager.chestnutStatus;
		}
		return null;
	}
	void initializeInventory()
	{

		//DELETE CHILDREN IF THERE ARE ANY IN LIST
		while (itemList.transform.childCount > 0)
		{
			GameObject.Destroy(itemList.transform.GetChild(0));
		}


		//INITIALIZE INVENTORY
		slots = new List<GameObject>();
		List<Item> inventory = invManager.inventory;
		for (int i = 0; i < inventory.Count; i++)
		{
			//Create item slots and put them in panel
			GameObject slot = Instantiate(itemSlot);

			GameObject itemName = new GameObject();
			itemName.AddComponent<Text>();
			itemName.GetComponent<Text>().text = inventory[i].itemName;
			itemName.GetComponent<Text>().font = font;
			itemName.GetComponent<Text>().resizeTextForBestFit = true;
			itemName.GetComponent<Text>().alignment = TextAnchor.UpperRight;

			GameObject quantity = new GameObject();
			quantity.AddComponent<Text>();
			quantity.GetComponent<Text>().text = "x" + invManager.quantity[i];
			quantity.GetComponent<Text>().font = font;
			quantity.GetComponent<Text>().resizeTextForBestFit = true;
			quantity.GetComponent<Text>().alignment = TextAnchor.UpperRight;

			itemName.transform.SetParent(slot.transform);
			quantity.transform.SetParent(slot.transform);


			slots.Add(slot);
			slots[i].transform.SetParent(itemList.transform);
			slots[i].transform.localScale = new Vector2(1, 1);

		}

	}
}

