using UnityEngine;
using System.Collections;

public class EventObject : MonoBehaviour {


	public int eventNumber;
	public EventManager theEM;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startEvent(){
		gameObject.SetActive (true);
	}

	public void endEvent(){
		theEM.eventsCompleted [eventNumber] = true;
		gameObject.SetActive (false);
	}
}
