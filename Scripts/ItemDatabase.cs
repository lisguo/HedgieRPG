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
			int id = (int)itemData[i]["id"];
			string itemName = (string) itemData [i] ["itemName"];
			int sellingValue = (int) itemData [i] ["sellingValue"];
			int buyingValue = (int) itemData [i] ["buyingValue"];
			string type = (string) itemData [i] ["type"];
			string description = (string) itemData [i] ["description"];

			//Check for category
			string category = (string)itemData[i]["category"];

			if(category == "Consumable"){
				int statBoost = (int)itemData[i]["statBoost"];
				database.Add(new Consumable(id, itemName, sellingValue, buyingValue, 
				                            type, description, statBoost));
			}
			else if(category == "Weapon"){
				int damage = (int)itemData[i]["damage"];
				database.Add(new Weapon(id, itemName, sellingValue, buyingValue, 
				                        type, description, damage));
			}
			else if(category == "Armor"){
				int defence = (int)itemData[i]["defence"];
				database.Add(new Armor(id, itemName, sellingValue, buyingValue,
										type, description, defence));
			}
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

	public Weapon(int id, string itemName, int sellingValue, int buyingValue, string type, string description, int damage){
		this.id = id;
		this.itemName = itemName;
		this.sellingValue = sellingValue;
		this.buyingValue = buyingValue;
		this.type = type;
		this.description = description;
		this.damage = damage;
	}
}

public class Armor : Item {

	public int defence;

	public Armor(int id, string itemName, int sellingValue, int buyingValue, string type, string description, int defence)
	{
		this.id = id;
		this.itemName = itemName;
		this.sellingValue = sellingValue;
		this.buyingValue = buyingValue;
		this.type = type;
		this.description = description;
		this.defence = defence;
	}
}

public class Consumable : Item{
	public int statBoost;

	public Consumable(int id, string itemName, int sellingValue, int buyingValue, string type, string description, int statBoost)
	{
		this.id = id;
		this.itemName = itemName;
		this.sellingValue = sellingValue;
		this.buyingValue = buyingValue;
		this.type = type;
		this.description = description;
		this.statBoost = statBoost;
	}
}

