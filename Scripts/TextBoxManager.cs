using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public Animator anim;
	public GameObject textBox;
	public Text theText;
	public Text characterName;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public PlayerMotion player;


	//Vars for text scrolling
	private bool isTyping;
	private bool cancelTyping;
	//TYPING SPEED HERE
	int typingDelay = 1;

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
	}

	void Update(){

		if (currentLine > endAtLine) {
			//Close textBox
			anim.SetBool ("dialogueOver", true);
			player.canMove = true;
		} else {
			//SHOW DIALOG
			if (currentLine % 2 == 0) {
				//Put Character Name
				characterName.text = textLines [currentLine];
				currentLine += 1;
			}
			//ACTUAL DIALOGUE
			if (currentLine % 2 == 1) {
				//TYPE TEXT
				StartCoroutine(typeText (textLines[currentLine]));
				//theText.text = textLines [currentLine];
			}
			if (Input.GetKeyDown (KeyCode.Return)) {
				//If dialog is typing, skip to end
				if (isTyping) {
					theText.text = textLines [currentLine];
					isTyping = false;
					cancelTyping = true;
				} else {
					currentLine += 1;
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
