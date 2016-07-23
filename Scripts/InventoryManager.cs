using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{

	public ItemDatabase database;
	public List<Item> inventory;
	public List<int> quantity;


	//TEST
	public List<string> inventoryStr = new List<string>();


	// Use this for initialization
	void Start ()
	{
		inventory = new List<Item> ();
		quantity = new List<int> ();

		//TESTT
		addItemByID (3,3);
		addItemByID (0, 5);
		addItemByID (0,5);

		//TEST
		foreach(Item item in inventory){
			inventoryStr.Add (item.itemName);
		}
	}


	void addItemByID(int id, int quantity){
		Item item = database.fetchItemByID (id);
		bool itemExists = false;
		int index = -1;
		//Check if item exists
		foreach(Item i in inventory){
			if (i.id == item.id) {
				//Debug.Log ("ITEM EXISTS");
				//If so, set index to index of prev item
				index = inventory.IndexOf (i);
				itemExists = true;
			}
		}

		if (!itemExists) {
			inventory.Add (item);
			index = inventory.IndexOf (item);
		}

		//SET QUANTITY
		//If item quantity is -1, change to 0
		if (index < this.quantity.Count) {
			this.quantity[index] =  this.quantity[index] + quantity;
		} else {
			this.quantity.Add (quantity);
		}
		Debug.Log ("Added " + inventory[index].itemName + " x" + quantity);
	}
		
}

