using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Stores the rate at which the person's stats decrease
[System.Serializable]
public class Personality
{
  // amount lost per second
  public float[] statDelta;

  // per turn, change this with event later
  public int stressIncrease;
};

public class PersonManager : MonoBehaviour {

  public int maxEnergy, stressAmt, curEnergy; // curEnergy = maxEnergy - stressAmt
  public float[] maxStat; // need to correspond to enum
  public float[] curStat;
  public Personality personality;
  public AFFLICT_TYPE afflictionType;
  private GameObject[] statBars;
  private float turnScale;
  private Text afflictionText;

	// Use this for initialization
	void Start () {
    statBars = GameObject.FindGameObjectsWithTag("stat_bar");
    curEnergy = maxEnergy - stressAmt;
    turnScale = 60.0f;
    afflictionText = GameObject.Find("affliction_text").GetComponent<Text>();

    SetAffliction(AFFLICT_TYPE.NONE);
  }

  void UpdateAllStatBars()
  {
    foreach (GameObject bar in statBars)
    {
      bar.GetComponent<StatScript>().SetBarHeight();
    }
  }

  void ReplenishEnergy()
  {
    curEnergy = maxEnergy - stressAmt;
  }

  public void SetAffliction(AFFLICT_TYPE type)
  {
    afflictionType = type;
    if (afflictionType != AFFLICT_TYPE.NONE)
      afflictionText.text = afflictionType.ToString();
    else
      afflictionText.text = "";
  }

  public void StepTurn()
  {
    for (int i = 0; i < curStat.Length; ++i)
    {
      curStat[i] = Mathf.Max(0.0f, curStat[i] - personality.statDelta[i] * turnScale);
    }

    // Advance stress amount
    stressAmt = Mathf.Min(10, stressAmt + personality.stressIncrease);

    ReplenishEnergy();
    UpdateAllStatBars();
  }

  public float GetStatPercentage(STAT_TYPE type)
  {
    int index = (int)type;
    return curStat[index] / maxStat[index];
  }

  public float GetStressPercentage()
  {
    return (float)stressAmt / maxEnergy;
  }

  public float GetEnergyPercentage()
  {
    return (float)curEnergy / maxEnergy;
  }

  public float GetStatAmount(STAT_TYPE type)
  {
    return curStat[(int)type];
  }

  public void IncreaseStat(STAT_TYPE type, int amt)
  {
    if (curEnergy > 0 && curStat[(int)type] + amt < maxStat[(int)type])
    {
      --curEnergy;
      curStat[(int)type] += amt;
    }
  }

  public void DecreaseStat(STAT_TYPE type, int amt)
  {
    if (curEnergy < maxEnergy - stressAmt)
    {
      ++curEnergy;
      curStat[(int)type] -= amt;
    }
  }
}
