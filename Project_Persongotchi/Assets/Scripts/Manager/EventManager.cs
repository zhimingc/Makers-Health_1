using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

  Encode:
   1) day
   2) stress
   3) sleep, water, exercise amts

*/

public class EventManager : MonoBehaviour {

  public bool eventsOn;
  public GameObject eventObj;
  public Text eventText;
  public bool isEvent;
  string eventURL, finalURL;
  private DayManager dayMan;
  private 
	// Use this for initialization
	void Start () {
    isEvent = false;
    eventObj.SetActive(isEvent);
    eventURL = "http://nomossgames.com/wegotchi/events.html";
    dayMan = GameObject.Find("day_manager").GetComponent<DayManager>();
  }
	
  public void SendEvent()
  {
    if (!eventsOn || dayMan.dayTracker <= 0) return;
    isEvent = true;

    eventObj.SetActive(isEvent);

    /*

      Encode:
       1) day
       2) stress
       3) sleep, water, exercise amts

    */
    // Attach game state
    //finalURL = eventURL + "?day=" + dayMan.dayTracker.ToString() + "&";
    //finalURL = finalURL + "st=" + p

    // Attach event object
    eventText.text = "Day " + dayMan.dayTracker.ToString() + " event";

    // Get event index
    int eventIndex = dayMan.dayTracker % 3;
    finalURL = eventURL + "?ev=" + eventIndex.ToString();
  }

  void Update()
  {
    if (isEvent && Input.anyKeyDown)
    {
      isEvent = false;
      eventObj.SetActive(isEvent);
      Application.OpenURL(finalURL);
    }
  }
}
