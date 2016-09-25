using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterPortraitController : MonoBehaviour {
	//Possible images
	public Sprite[] portraitSprites;
	public GameObject portrait;

	void Awake(){
		portraitSprites = Resources.LoadAll<Sprite> ("portrait");

	}
	void start(){
		portrait = new GameObject ();
	}

	public void setImage(Sprite img){
		portrait.GetComponent<Image> ().sprite = img;
	}
	public void showPortrait(){
		portrait.GetComponent<Image> ().enabled = true;
	}
	public void disablePortrait(){
		portrait.GetComponent<Image> ().enabled = false;
	}
}
