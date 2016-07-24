﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{
	Text itemDescription;
	InventoryManager invManager;
	public InventoryMenu invMenu;

	void Start(){
		itemDescription = GameObject.FindWithTag("ItemDescription").GetComponent<Text>();
		invManager = GameObject.FindObjectOfType<InventoryManager>();
		invMenu = GameObject.FindObjectOfType<InventoryMenu>();
	}

	public void OnPointerEnter(PointerEventData eventData){
		//SHOW DESCRIPTION ON MOUSE OVER
		string itemName = this.GetComponentsInChildren<Text>()[0].text;
		itemDescription.text = getItemByName(itemName).description;
	}

	public void OnPointerExit(PointerEventData eventData) {
		itemDescription.text = "";
	}

	public void OnPointerClick(PointerEventData eventData) {
		Item item = getItemByName(this.GetComponentsInChildren<Text>()[0].text);
		invMenu.itemActionPanel.GetComponent<InventoryItemAction>().setItem(item);
		invMenu.itemActionPanel.SetActive(true);
	}

	Item getItemByName(string itemName){
		foreach (Item item in invManager.inventory){
				string currentName = item.itemName;
			if (itemName == currentName){
				//MATCH
				return item;
				}
			}
			return null;
	}

}