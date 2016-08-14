using UnityEngine;
using System.Collections;

public class Cutscene_0 : Cutscene{

	public Transform cameraTarget;
	public Animation bobAnimation;
	public TextBoxManager dialog_1;
	public Animation rogue_1_animation;
	public Animation rogue_2_animation;
	public TextBoxManager dialog_2;


	public override void playCutscene(){
		//Move camera
		cutsceneCamera.GetComponent<CameraFollow>().target = cameraTarget;
		cutsceneCamera.GetComponent<CameraFollow>().enabled = true;

		//Animate bob
		bobAnimation.Play();
		if (bobAnimation.IsPlaying("cutscene_0-bobMovement") == true) {
			StartCoroutine(cutsceneSequence());
		}
	}


	private IEnumerator cutsceneSequence(){
		yield return new WaitForSeconds(2);

		//Show dialog 1
		dialog_1.enabled = true;
		while (dialog_1.getDiagDone() == false) {
			yield return null;
			//Do nothing
		}

		//Animate rogues
		rogue_1_animation.Play();
		rogue_2_animation.Play();
		while (rogue_2_animation.isPlaying || rogue_1_animation.isPlaying) {
			yield return null;
			//Do nothing
		}

		//Show dialog 2
		dialog_2.enabled = true;
		while (dialog_2.getDiagDone() == false) {
			yield return null;
			//Do nothing
		}

		//Show animation for battle
		Animation cameraAnim = cutsceneCamera.GetComponent<Animation>();
		cameraAnim.clip = cameraAnim.GetClip("BlurToBattle");
		cameraAnim.Play();
	}
}