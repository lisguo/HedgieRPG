using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryItemAction : MonoBehaviour
{
	public Item item;
	public GameObject buttonList;
	public GameObject button;

	public GameObject partySelect;


	public InventoryItemAction(Item item)
	{
		this.item = item;
	}

	public void setItem(Item itemToSet)
	{
		item = itemToSet;
	}

	void OnEnable()
	{
		if(item is Consumable){
			//Instantiate button prefab
			GameObject useButton = Instantiate(button);
			useButton.GetComponentInChildren<Text>().text = "Use";
			useButton.transform.SetParent(buttonList.transform);
			useButton.transform.localScale = new Vector2(1, 1);
			//Attach script
			useButton.AddComponent<UseButton>();
			useButton.GetComponent<UseButton>().partySelect = this.partySelect;
			useButton.GetComponent<UseButton>().inventoryItemAction = this.gameObject;

			GameObject destroyButton = Instantiate(button);
			destroyButton.GetComponentInChildren<Text>().text = "Destroy";
			destroyButton.transform.SetParent(buttonList.transform);
			destroyButton.transform.localScale = new Vector2(1, 1);
			//Attach script
			destroyButton.AddComponent<DestroyButton>();
			destroyButton.GetComponent<DestroyButton>().inventoryItemAction = this.gameObject;
		}
		else if(item is Armor || item is Weapon){
			GameObject equipButton = Instantiate(button);
			equipButton.GetComponentInChildren<Text>().text = "Equip";
			equipButton.transform.SetParent(buttonList.transform);
			equipButton.transform.localScale = new Vector2(1, 1);

			GameObject destroyButton = Instantiate(button);
			destroyButton.GetComponentInChildren<Text>().text = "Destroy";
			destroyButton.transform.SetParent(buttonList.transform);
			destroyButton.transform.localScale = new Vector2(1, 1);
			//Script
			destroyButton.AddComponent<DestroyButton>();
			destroyButton.GetComponent<DestroyButton>().inventoryItemAction = this.gameObject;
		}
		else{
			GameObject destroyButton = Instantiate(button);
			destroyButton.GetComponentInChildren<Text>().text = "Destroy";
			destroyButton.transform.SetParent(buttonList.transform);
			destroyButton.transform.localScale = new Vector2(1, 1);
			//Script
			destroyButton.AddComponent<DestroyButton>();
			destroyButton.GetComponent<DestroyButton>().inventoryItemAction = this.gameObject;
		}
	}

	void OnDisable(){
		//Remove Children in buttonList
		for (int i = 0; i < buttonList.transform.childCount; i ++){
			GameObject.Destroy(buttonList.transform.GetChild(i).gameObject);
		}
	}
}
