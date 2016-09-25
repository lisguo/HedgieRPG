using UnityEngine;
using System.Collections;

[System.Serializable]
public class Enemy{
	public string name;

	public enum Rarity{
		COMMON,
		UNCOMMON,
		RARE,
		SUPERRARE
	}

	public Rarity rarity;

	public float baseHP;
	public float currHP;

	public float baseMP;
	public float currMP;

	public float baseATK;
	public float currATK;

	public float baseDEF;
	public float currDEF;

	public int level;
}
