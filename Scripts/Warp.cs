using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	public Transform warpTarget;

	IEnumerator OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

			Debug.Log("Fading to Black");
			yield return StartCoroutine(sf.FadeToBlack());
			other.gameObject.transform.position = warpTarget.position;
			Camera.main.transform.position = warpTarget.position;

			//wait a few seconds
			yield return new WaitForSeconds(0.1f);

			Debug.Log("Fading Out");
			yield return StartCoroutine(sf.FadeToClear());

		}
	}
}
