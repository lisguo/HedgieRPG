using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
	public Text itemDescription;
	public InventoryManager invManager;

	bool isOver = false;

	void Start(){
		itemDescription = GameObject.FindWithTag("ItemDescription").GetComponent<Text>();
		invManager = GameObject.FindObjectOfType<InventoryManager>();
	}

	public void OnPointerEnter(PointerEventData eventData){
		isOver = true;
		//SHOW DESCRIPTION ON MOUSE OVER
		string itemName = this.GetComponentsInChildren<Text>()[0].text;
		itemDescription.text = getDescriptionByItemName(itemName);
	}

	public void OnPointerExit(PointerEventData eventData) {
		isOver = false;
		itemDescription.text = "";
	}

	string getDescriptionByItemName(string itemName){
		foreach (Item item in invManager.inventory){
				string currentName = item.itemName;
			if (itemName == currentName){
					//MATCH
					return item.description;
				}
			}
			return "ERROR: NO DESCRIPTION";
	}

}