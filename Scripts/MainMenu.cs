using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string startLevel;

	public string fileSelect;

	public void NewGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (startLevel);
	}

	public void LoadGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (fileSelect);
	}

	public void QuitGame(){
		Application.Quit();
	}
}
