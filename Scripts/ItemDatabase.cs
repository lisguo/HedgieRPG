using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
	private List<Item> database = new List<Item>();
	JsonData itemData;

	// Use this for initialization
	void Start ()
	{
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase ();
	}

	void ConstructItemDatabase(){
		for (int i = 0; i < itemData.Count; i++) {
			int id = (int )itemData [i] ["id"];
			string itemName = (string) itemData [i] ["itemName"];
			int sellingValue = (int) itemData [i] ["sellingValue"];
			int buyingValue = (int) itemData [i] ["buyingValue"];
			string type = (string) itemData [i] ["type"];
			string description = (string) itemData [i] ["description"];
			database.Add (new Item(id, itemName, sellingValue, buyingValue,
				type, description));
		}
	}

	public Item fetchItemByID(int id){
		for (int i = 0; i < database.Count; i++) {
			if (database [i].id == id) {
				return database [i];
			}
		}
		return null;
	}
}

public class Item
{
	public int id;
	public string itemName;
	public int sellingValue;
	public int buyingValue;
	public string type;
	public string description;

	public Item(){

	}
	public Item(int id, string itemName, int sellingValue, int buyingValue, string type, string description){
		this.id = id;
		this.itemName = itemName;
		this.sellingValue = sellingValue;
		this.buyingValue = buyingValue;
		this.type = type;
		this.description = description;
	}
}

public class Weapon : Item {

	public int damage;
	public Sprite img;
}

public class Armor : Item {

	public int def;
	public Sprite img;
}

public class Consumable : Item{
	public int statBoost;
}

