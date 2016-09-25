using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartyManager: MonoBehaviour {

	public List<PartyMember> party;

	public Sprite pineconeStatus;
	public Sprite chestnutStatus;
	public Sprite bunnyStatus;
	public Sprite ennaStatus;
	public Sprite skippyStatus;
	public Sprite cielStatus;

	public PartyMember getPartyMember(string characterName){
		//Loop through party for member
		foreach(PartyMember pm in party){
			if(pm.characterName == characterName){
				return pm;
			}
		}
		//If no party member is found
		return null;
	}
}
