using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public Animator textAnim;
	public GameObject textBox;
	public Text theText;
	public Text characterName;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;

	private int startingLine;
	public int endAtLine;

	private PlayerMotion player;

	//Vars for showing portrait
	public CharacterPortraitController characterPortraitCtrl;
	public Sprite[] portraits;
	private int currPortraitIndex;
	private string currCharName;

	//Vars for text scrolling
	private bool isTyping;
	private bool cancelTyping;
	private bool firstDialogue;

	private bool diagDone = false;

	//TYPING SPEED HERE
	float typingDelay = 0.02f;

	// Use this for initialization
	void Start () {
		textAnim = textBox.GetComponent<Animator> ();
		//Debug.Log ("Got animator");
		player = FindObjectOfType<PlayerMotion> ();

		if (textFile != null) {
			//Debug.Log ("Text file found");
			textLines = textFile.text.Split ('\n');
			EnableTextBox ();
			player.canMove = false;
			player.GetComponent<Animator>().SetBool ("isWalking", false);


			currPortraitIndex = 0;
			currCharName = textLines [currentLine];
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}
		firstDialogue = true;
		startingLine = currentLine;
	}

	void Update(){

		//If finished convo
		if (currentLine > endAtLine) {
			
			//Close textBox and disable portrait
			DisableTextBox();
			player.canMove = true;
			characterPortraitCtrl.disablePortrait ();
			diagDone = true;
		} else {
			
			//SHOW DIALOGE
			EnableTextBox();
			showPortraitAndName ();

			//SHOW DIALOGUE WHEN LINE IS ODD AND ITS FIRST DIAG
			if (currentLine % 2 == 1 && firstDialogue) {
				//TYPE TEXT
				StartCoroutine (typeText (textLines [currentLine]));
				firstDialogue = false;
			}
			if (Input.GetKeyDown (Constants.CONFIRM)) {
				//If dialog is typing, skip to end
				if (isTyping) {
					theText.text = textLines [currentLine];
					isTyping = false;
					cancelTyping = true;
					characterPortraitCtrl.showPortrait (); //BAND AID FIX FOR PORTRAIT NOT SHOWING AFTER SKIP
				}
				else {
					currentLine += 1;
					if (currentLine <= endAtLine) {
						showPortraitAndName ();
						StartCoroutine (typeText (textLines [currentLine]));
					}
				}
			}
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

	void showPortraitAndName(){
		if (currentLine % 2 == 0) {
			//Check if char name changed, if so then move to next sprite
			string nextCharName = textLines[currentLine];
			if (!currCharName.Equals (nextCharName)) {
				currPortraitIndex += 1;
				currCharName = nextCharName;
			}
			//Show Portrait
			characterPortraitCtrl.setImage (portraits [currPortraitIndex]);
			characterPortraitCtrl.showPortrait ();

			//Put Character Name
			characterName.text = textLines [currentLine];
			currentLine += 1;
		}
	}

	public void EnableTextBox(){
		textAnim.SetBool("dialogueOver", false);
	}
	public void DisableTextBox(){
		textAnim.SetBool ("dialogueOver", true);
	}


	public bool getDiagDone(){
		return diagDone;
	}
}
