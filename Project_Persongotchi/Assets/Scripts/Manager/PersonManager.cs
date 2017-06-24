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
  public float stressIncrease;
};

public class PersonManager : MonoBehaviour {

  public bool isPassive;
  public float stressAmt;
  public int maxEnergy, curEnergy; // curEnergy = maxEnergy - stressAmt
  public float[] maxStat; // need to correspond to enum
  public float[] curStat;
  public float[] passiveStatDelta;
  public Sprite[] personSprite;

  public Personality personality;
  public AFFLICT_TYPE afflictionType;
  private GameObject[] statBars;
  private float turnScale;
  private Text afflictionText;
  private Text[] passiveStatText;

	// Use this for initialization
	void Start () {
    passiveStatDelta = new float[3] { 0, 0, 0 };
    statBars = GameObject.FindGameObjectsWithTag("stat_bar");
    curEnergy = maxEnergy - (int)stressAmt;
    turnScale = 1.0f;
    afflictionText = GameObject.Find("affliction_text").GetComponent<Text>();

    // Passive text
    passiveStatText = new Text[3];
    passiveStatText[0] = GameObject.Find("sleep_rate").GetComponent<Text>();
    passiveStatText[1] = GameObject.Find("water_rate").GetComponent<Text>();
    passiveStatText[2] = GameObject.Find("exercise_rate").GetComponent<Text>();
    if (!isPassive) foreach (Text txt in passiveStatText) txt.enabled = false;

    SetAffliction(AFFLICT_TYPE.NONE);

    // set sprite
    int spriteNum = Random.Range(0, personSprite.Length);
    GetComponent<SpriteRenderer>().sprite = personSprite[spriteNum];
  }

  void Update()
  {
    if (isPassive)
    {
      UpdatePassiveStatTexts();
    }

    if (afflictionType != AFFLICT_TYPE.NONE)
    {
      GetComponent<Animator>().SetBool("run", true);
    }
  }

  void UpdateAllStatBars()
  {
    foreach (GameObject bar in statBars)
    {
      bar.GetComponent<StatScript>().SetBarHeight();
    }
  }

  void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0) && afflictionType != AFFLICT_TYPE.NONE)
    {
      Application.OpenURL("http://www.nomossgames.com/wegotchi/not-great.html");
    }
  }

  void UpdatePassiveStatTexts()
  {
    for(int i = 0; i < 3; ++i)
    {
      passiveStatText[i].text = passiveStatDelta[i].ToString("0") + "/-" + personality.statDelta[i].ToString("0");
    }
  }

  public void ReplenishEnergy()
  {
    if (isPassive) return;
    curEnergy = maxEnergy - (int)stressAmt;
  }

  public void SetAffliction(AFFLICT_TYPE type)
  {
    afflictionType = type;
    if (afflictionType != AFFLICT_TYPE.NONE)
    {
      afflictionText.text = afflictionType.ToString();

    }
    else
      afflictionText.text = "";
  }

  public void StepTurn(float stepAmt)
  {
    for (int i = 0; i < curStat.Length; ++i)
    {
      float amtToMinus = personality.statDelta[i];
      if (isPassive) amtToMinus -= passiveStatDelta[i];
      curStat[i] = Mathf.Max(0.0f, curStat[i] - amtToMinus * stepAmt);
    }

    // Advance stress amount
    stressAmt = Mathf.Min(10, stressAmt + personality.stressIncrease * stepAmt);
    if (curEnergy > maxEnergy - stressAmt) curEnergy = maxEnergy - (int)stressAmt;

    if (isPassive)
    {
      int curMaxEnergy = maxEnergy - (int)stressAmt;
      int spentEnergy = 0;
      foreach (int num in passiveStatDelta) spentEnergy += num;
      while (spentEnergy-- > curMaxEnergy)
      {
        int max = 0;
        for (int i = 0; i < 3; ++i)
          if (passiveStatDelta[i] > max) max = i;
        --passiveStatDelta[max];
      }
    }

    UpdateAllStatBars();
  }

  public float GetStatPercentage(STAT_TYPE type)
  {
    int index = (int)type;
    return curStat[index] / maxStat[index];
  }

  public float GetStressPercentage()
  {
    return stressAmt / maxEnergy;
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
    if (curEnergy > 0)
    {
      if (isPassive)
      {
        ++passiveStatDelta[(int)type];
      }
      else
      {
        if (curStat[(int)type] + amt < maxStat[(int)type])
        {
          curStat[(int)type] += amt;
        }
      }
      --curEnergy;
    }

  }

  public void DecreaseStat(STAT_TYPE type, int amt)
  {
    if (curEnergy < maxEnergy - stressAmt)
    {
      ++curEnergy;

      if (isPassive)
      {
        --passiveStatDelta[(int)type];
      }
      else
      {
        curStat[(int)type] -= amt;
      }
    }
  }
}
