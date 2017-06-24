using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour {

  public int dayTracker;
  private PersonManager personMan;
  private Text dayTrackerText;
  private AfflictionManager afflictMan;

  // Use this for initialization
  void Start () {
    dayTracker = 0;
    dayTrackerText = GameObject.Find("turn_tracker").GetComponent<Text>();
    personMan = GameObject.Find("person").GetComponent<PersonManager>();
    afflictMan = GameObject.Find("afflict_manager").GetComponent<AfflictionManager>();

    UpdateTurnTracker();
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  void UpdateTurnTracker()
  {
    dayTrackerText.text = "Day " + dayTracker.ToString();
  }

  public void AdvanceTurn()
  {
    ++dayTracker;
    UpdateTurnTracker();

    // check afflictions
    afflictMan.CheckAffliction();

    // update person
    personMan.StepTurn();
  }
}
