using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManagerOpening : MonoBehaviour {

	public Animator anim;
	public GameObject textBox;
	public Text theText;
	public Text characterName;
	public Animator screenFader;
	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	private PlayerMotion player;

	//Vars for character portraits
	public CharacterPortraitController characterPortrait;
	public Sprite pineconeSprite;
	public Sprite chestnutSprite;

	//Vars for text scrolling
	private bool isTyping;
	private bool cancelTyping;
	//TYPING SPEED HERE
	float typingDelay = 0.02f;

	bool showOnce = true;
	int counter;
	// Use this for initialization
	void Start () {
		anim = textBox.GetComponent<Animator> ();
		//Debug.Log ("Got animator");
		player = FindObjectOfType<PlayerMotion> ();

		if (textFile != null) {
			//Debug.Log ("Text file found");
			textLines = textFile.text.Split ('\n');
			anim.SetBool ("dialogueOver", false);
			player.canMove = false;
			player.GetComponent<Animator>().SetBool ("isWalking", false);
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}
		counter = 0;

	}

	void Update(){

		if (currentLine > endAtLine) {
			//Close textBox
			if (counter == 0) {
				anim.SetBool ("dialogueOver", true);
				screenFader.SetTrigger ("Fade In");



				//Display another textbox
				anim.SetBool ("dialogueOver", false);
				player.canMove = false;
				player.GetComponent<Animator>().SetBool ("isWalking", false);

				//Show left portrait
				characterPortrait.setImage(pineconeSprite);
				characterPortrait.showPortrait ();
				characterName.text = textLines [currentLine];
				currentLine += 1;
				StartCoroutine(typeText (textLines[currentLine]));

		
				counter += 1;
			}

			if (Input.GetKeyDown (Constants.CONFIRM)) {
				//If dialog is typing, skip to end
				if (isTyping) {
					theText.text = textLines [currentLine];
					isTyping = false;
					cancelTyping = true;
				} else {
					//START TEXT TYPING
					if (cancelTyping) {
						anim.SetBool("dialogueOver", true);
						player.canMove = true;

						//Disable portrait
						characterPortrait.disablePortrait();
					} else {
						if (currentLine % 2 == 1) {
							StartCoroutine (typeText (textLines [currentLine]));
						}
					}
				}
			}
		} else {
			//SHOW CHESTNUT
			characterPortrait.setImage (chestnutSprite);
			characterPortrait.showPortrait ();

			if (currentLine % 2 == 0) {
				//Put Character Name
				characterName.text = textLines [currentLine];
				currentLine += 1;
			}
			if (currentLine % 2 == 1 && showOnce) {
				StartCoroutine (typeText (textLines [currentLine]));
				showOnce = false;
			}
			if (Input.GetKeyDown (Constants.CONFIRM)) {
				//If dialog is typing, skip to end
				if (isTyping) {
					theText.text = textLines [currentLine];
					isTyping = false;
					cancelTyping = true;
				} else {
					//START TEXT TYPING
					if (cancelTyping) {

						//DISABLE CHESTNUT PORTRAIT
						characterPortrait.disablePortrait();
						currentLine += 1;
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

	public void EnableTextBox(){
		anim.SetBool("dialogueOver", false);
		anim.SetBool ("newDialogue", true);
	}
	public void DisableTextBox(){
		anim.SetBool ("newDialogue", false);
		anim.SetBool("dialogueOver", true);
	}

}
