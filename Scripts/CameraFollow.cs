using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target; //Stores data
	Camera myCam;
	public float cameraSpeed = 0.1f;

	// Use this for initialization
	void Start () {

		myCam = GetComponent<Camera> (); //Gets camera associated with this object

	}
	
	// Update is called once per frame
	void Update () {
	
		myCam.orthographicSize = (Screen.height / 100f) / 1.5f;
		if (target) {
			transform.position = Vector3.Lerp (transform.position, target.position, cameraSpeed) + new Vector3(0,0,-10);
		}
	}
}
