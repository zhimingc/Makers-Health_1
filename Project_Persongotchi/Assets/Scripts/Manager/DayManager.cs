using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour {

  public bool isRealTime;
  public int dayTracker;
  public float lengthOfDay, curDayTimer;
  public Image dayTrackerImg;
  private PersonManager personMan;
  private Text dayTrackerText;
  private AfflictionManager afflictMan;

  // Use this for initialization
  void Start () {
    dayTracker = 0;
    curDayTimer = lengthOfDay;
    dayTrackerText = GameObject.Find("turn_tracker").GetComponent<Text>();
    personMan = GameObject.Find("person").GetComponent<PersonManager>();
    afflictMan = GameObject.Find("afflict_manager").GetComponent<AfflictionManager>();

    UpdateTurnTracker();
  }
	
	// Update is called once per frame
	void Update () {
		if (isRealTime && personMan.afflictionType == AFFLICT_TYPE.NONE)
    {
      RealTimeUpdate();
    }
	}

  void UpdateTurnTracker()
  {
    dayTrackerText.text = "Day " + dayTracker.ToString();
  }

  void RealTimeUpdate()
  {
    // check afflictions
    afflictMan.CheckAffliction();

    // update person
    personMan.StepTurn(Time.deltaTime);

    curDayTimer -= Time.deltaTime;
    if (curDayTimer <= 0.0f)
    {
      personMan.ReplenishEnergy();
      curDayTimer = lengthOfDay;
      ++dayTracker;
      UpdateTurnTracker();
    }
    dayTrackerImg.fillAmount = curDayTimer / lengthOfDay;
  }

  public void AdvanceTurn()
  {
    if (isRealTime) return;

    ++dayTracker;
    UpdateTurnTracker();

    // check afflictions
    afflictMan.CheckAffliction();

    // update person
    personMan.StepTurn(5);
    personMan.ReplenishEnergy();
  }
}
