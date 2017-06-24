using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AFFLICT_TYPE
{
  COLD,
  ALLERGIES,
  INFECTION,
  NONE
}

[System.Serializable]
public class AfflictConditions
{
  //public int sleepThreshold;
  //public int waterThreshold;
  //public int exerciseThreshold;
  public GameObject[] afflictBar;
  public int[] threshold;
}

[ExecuteInEditMode]
public class AfflictionManager : MonoBehaviour {

  //public AfflictConditions[] afflictLimit;
  public AfflictConditions coldLimit;
  public AfflictConditions allergiesLimit;
  public AfflictConditions infectionLimit;
  private GameObject[] afflictBars;
  private GameObject[] statBars;
  private PersonManager person;
  private Text afflictionText;

	// Use this for initialization
	void Start () {
    afflictBars = GameObject.FindGameObjectsWithTag("afflict_bar");
    statBars = GameObject.FindGameObjectsWithTag("stat_bar");
    person = GameObject.Find("person").GetComponent<PersonManager>();
    afflictionText = GameObject.Find("affliction_text").GetComponent<Text>();

    SetAfflictBars();
  }
	
  void Update()
  {
    SetAfflictBars();
  }

	void SetAfflictBars()
  {
    for (int i = 0; i < (int)AFFLICT_TYPE.NONE; ++i)
    {
      RectTransform t = statBars[i].GetComponent<RectTransform>();
      int ai = (int)statBars[i].GetComponent<StatScript>().statType;
      Vector3 newPos = new Vector3(t.anchoredPosition.x, t.anchoredPosition.y - t.rect.height / 2.0f, 0);

      int afflictLimit = GetThreshold((AFFLICT_TYPE)ai);
      
      //newPos.y += t.rect.height * (afflictLimit[i].threshold[0] / person.maxStat[i]);
      newPos.y += t.rect.height * (afflictLimit / person.maxStat[i]);
      afflictBars[ai].transform.position = newPos;
    }
  }

  int GetThreshold(AFFLICT_TYPE type)
  {
    int retVal = 0;
    switch (type)
    {
      case AFFLICT_TYPE.COLD:
        retVal = coldLimit.threshold[0];
        break;
      case AFFLICT_TYPE.ALLERGIES:
        retVal = allergiesLimit.threshold[0];
        break;
      case AFFLICT_TYPE.INFECTION:
        retVal = infectionLimit.threshold[0];
        break;
    }

    return retVal;
  }

  public void CheckAffliction()
  {
    for (int i = 0; i < (int)AFFLICT_TYPE.NONE; ++i)
    {
      int statAmt = (int)person.curStat[i];
      int threshold = GetThreshold((AFFLICT_TYPE)i);

      //if (statAmt < afflictLimit[i].threshold[0])
      if (statAmt < threshold)
      {
        person.SetAffliction((AFFLICT_TYPE)i);
        return;
      }
    }
  }
}
