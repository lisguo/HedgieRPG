//Changes camera to cutscene camera on enable
//Changes cutscene camera to main on disable

using UnityEngine;
using System.Collections;

public abstract class Cutscene : MonoBehaviour
{

	ScreenFader sf;
	public GameObject cutsceneCamera;
	bool isTransitioning = false;

	void Start()
	{
		//sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
	}


	void OnEnable()
	{
		//Make player freeze
		FindObjectOfType<PlayerMotion>().canMove = false;

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
		FindObjectOfType<PlayerMotion>().canMove = true;
	}

	public abstract void playCutscene();
}
