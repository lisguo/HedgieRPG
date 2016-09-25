//Changes camera to cutscene camera on enable
//Changes cutscene camera to main on disable

using UnityEngine;
using System.Collections;

public abstract class Cutscene : MonoBehaviour
{
	public GameManager gm;
	ScreenFader sf;
	public GameObject cutsceneCamera;
	bool isTransitioning = false;

	void Start()
	{
	}


	void OnEnable()
	{

		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		//Set game state to cutscene
		gm.currState = GameManager.GameState.CUTSCENE;

		//Get transform position vals of main camera
		cutsceneCamera.GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position;
		//Switch camera
		cutsceneCamera.SetActive(true);
		isTransitioning = true;
		wait();
		playCutscene();
	}

	IEnumerator wait()
	{
		while (isTransitioning) {
			yield return null;
		}
	}

	void AnimationComplete()
	{
		isTransitioning = false;
	}

	void OnDisable()
	{
		//Switch camera
		cutsceneCamera.SetActive(false);
		gm.currState = GameManager.GameState.END_CUTSCENE;
	}

	public abstract void playCutscene();
}
