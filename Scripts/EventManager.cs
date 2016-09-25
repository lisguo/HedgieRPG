using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public EventObject[] events;
	public bool[] eventsCompleted;

	// Use this for initialization
	void Start () {
		eventsCompleted = new bool[events.Length];
	}
	
	// Update is called once per frame
	void Update () {
	}
}
