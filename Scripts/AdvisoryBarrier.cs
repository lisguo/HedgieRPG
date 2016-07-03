using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdvisoryBarrier : MonoBehaviour {

	public Animator anim;
	public GameObject textBox;
	public Text theText;
	public Text characterName;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	private PlayerMotion player;

	//Vars for character portraits
	public Sprite characterPortrait;
	private CharacterPortraitController controller;

	//Vars for text scrolling
	private bool isTyping;
	private bool cancelTyping;
	//TYPING SPEED HERE
	float typingDelay = 0.02f;

	void init () {
		anim = textBox.GetComponent<Animator> ();
		//Debug.Log ("Got animator");
		player = FindObjectOfType<PlayerMotion> ();
		controller = FindObjectOfType<CharacterPortraitController> ();

		if (textFile != null) {
			//Debug.Log ("Text file found");
			textLines = textFile.text.Split ('\n');
			anim.SetBool ("dialogueOver", false); //OPEN DIALOG
			player.canMove = false;
			player.GetComponent<Animator>().SetBool ("isWalking", false);
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}
	}

	private IEnumerator typeText(string lineOfText){
		isTyping = true;
		cancelTyping = false;
		//Init Dialog
		theText.text = ""; //Default to nothing
		int stringIndex = 0; 

		while (isTyping && !cancelTyping && (stringIndex < lineOfText.Length - 1)) {
			theText.text += lineOfText [stringIndex]; //String is literally an array[char]
			stringIndex += 1;
			yield return new WaitForSeconds (typingDelay);
		}
		//After animation, text stays the same
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = true;

	}

	void OnTriggerEnter2D(){
		init ();
		player.canMove = false;
		if (currentLine % 2 == 0) {
			//Put Character Name
			characterName.text = textLines [currentLine];
			controller.setImage (characterPortrait);
			controller.showPortrait ();
			currentLine += 1;
		}
		if (currentLine % 2 == 1) {
			StartCoroutine(typeText (textLines[currentLine]));
		}

	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			//If dialog is typing, skip to end

			Debug.Log ("is Typing: " + isTyping);
			if (isTyping) {
				theText.text = textLines [currentLine];
				anim.SetBool ("dialogueOver", false); //Open dialog
				controller.showPortrait(); //Keep portrait open
				isTyping = false;
				cancelTyping = true;
			} 
			else if (cancelTyping) {
				anim.SetBool ("dialogueOver", true); //Close dialog
				controller.disablePortrait();
				cancelTyping = false;
				currentLine -= 1; //Used for future purposes
			}
		}
	}
}
