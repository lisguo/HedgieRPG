using UnityEngine;
using System.Collections;

public class PartyMember : MonoBehaviour {


	public ItemDatabase itemDatabase;

	public string bio;

	//Stats
	public int level;
	public int baseHP;
	public int hp;
	public int baseMP;
	public int fullHP;
	public int mp;
	public int fullMP;
	public int exp;
	public int expToLevel;
	public string characterName;

	public int strength;
	public int intelligence;
	public int vitality;
	public int speed;

	int attack;
	int defence;

	//Armor
	public Armor hat;
	public Armor body;
	public Armor feet;
	public Armor pendant;

	//Weapon
	public Weapon weap;

	//Images
	public Sprite statusScreen;


	//Getters
	public int getAttack(){
		return attack;
	}
	public int getDefence(){
		return defence;
	}
	// Use this for initialization
	void Start () {
		level = 1;
		exp = 0;

		expToLevel = (level + 10 )* 2;
		fullHP = baseHP + vitality + ((level + 5) * 2);
		fullMP = baseMP + intelligence + ((level + 2) * 2);

		//Weapon info
		if(characterName == "Chestnut"){
			weap = (Weapon)itemDatabase.fetchItemByID(3);
		}
		else if(characterName == "Pinecone"){
			weap = (Weapon)itemDatabase.fetchItemByID(2);
		}else if (characterName == "Bunny") {
			weap = (Weapon)itemDatabase.fetchItemByID(2); //CHANGE
		}else if (characterName == "Enna") {
			weap = (Weapon)itemDatabase.fetchItemByID(2); //CHANGE
		}else if (characterName == "Skippy") {
			weap = (Weapon)itemDatabase.fetchItemByID(2); //CHANGE
		}else if (characterName == "Ciel") {
			weap = (Weapon)itemDatabase.fetchItemByID(2); //CHANGE
		}
		attack = attack + weap.damage;

		hp = fullHP;
		mp = fullMP;

		//Give armor
	}
	
	// Update is called once per frame
	void Update () {
		if (exp >= expToLevel) {
			levelUp();
		}
	}

	void levelUp() {
		level++;
		exp = 0;
		expToLevel = (level + 10) * 2;
		fullHP = baseHP + vitality + ((level + 5) * 2);
		fullMP = baseMP + intelligence + ((level + 2) * 2);
		attack = attack + weap.damage;
		defence = defence + hat.defence + body.defence + feet.defence + pendant.defence;
	}

	public void addHP(int points){
		hp += points;
		if(hp > fullHP){
			hp = fullHP;
		}
	}

	public void addMP(int points){
		mp += points;
		if(mp > fullMP){
			mp = fullMP;
		}
	}
}
