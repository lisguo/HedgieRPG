using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
	public InventoryManager invManager;
	public GameObject itemList;
	public GameObject itemSlot;
	public Font font;

	List<GameObject> slots = new List<GameObject>();

	// Use this for initialization
	void Start ()
	{
		List<Item> inventory = invManager.inventory;
		for(int i = 0; i < inventory.Count; i ++) {
			//Create item slots and put them in panel
			GameObject slot = Instantiate(itemSlot);

			GameObject itemName = new GameObject ();
			itemName.AddComponent<Text> ();
			itemName.GetComponent<Text>().text = inventory [i].itemName; 
			itemName.GetComponent<Text> ().font = font;
			itemName.GetComponent<Text> ().resizeTextForBestFit = true;
			itemName.GetComponent<Text> ().alignment = TextAnchor.UpperRight;

			GameObject quantity = new GameObject ();
			quantity.AddComponent<Text> ();
			quantity.GetComponent<Text> ().text = "x" + invManager.quantity [i]; 
			quantity.GetComponent<Text> ().font = font;
			quantity.GetComponent<Text> ().resizeTextForBestFit = true;
			quantity.GetComponent<Text> ().alignment = TextAnchor.UpperRight;

			itemName.transform.SetParent (slot.transform);
			quantity.transform.SetParent (slot.transform);

			slots.Add (slot);
			slots [i].transform.SetParent (itemList.transform);
			slots [i].transform.localScale = new Vector2(1,1);

		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}

