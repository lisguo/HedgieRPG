using UnityEngine;
using System.Collections;

public class MainEventManager : EventManager
{

	private int currentEventIndex;
	private EventObject currentEvent;


	// Use this for initialization
	void Start ()
	{
		eventsCompleted = new bool[events.Length];
		currentEvent = events [currentEventIndex];
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If an event is not active, start next event
		if (!currentEvent.isActiveAndEnabled && currentEventIndex < events.Length-1) {
			currentEventIndex += 1;
			currentEvent = events [currentEventIndex];
			currentEvent.startEvent ();
		}
	}
}

