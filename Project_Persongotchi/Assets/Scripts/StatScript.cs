using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum STAT_TYPE
{
  SLEEP,
  WATER,
  EXERCISE
};

public class StatScript : MonoBehaviour {

  public STAT_TYPE statType;
  public Image backingImg;
  private PersonManager personMan;
  private Text statAmtText;

  // Use this for initialization
  void Start () {
    personMan = GameObject.Find("person").GetComponent<PersonManager>();
    statAmtText = GetComponentInChildren<Text>();
    SetBarHeight();
  }
	
	// Update is called once per frame
	void Update () {
    UpdateBarHeight();
  }

  public void SetBarHeight()
  {
    GetComponent<Image>().fillAmount = personMan.GetStatPercentage(statType);
  }

  void UpdateBarHeight()
  {
    //GetComponent<Image>().fillAmount = personMan.GetStatPercentage(statType);
    backingImg.fillAmount = personMan.GetStatPercentage(statType);
    statAmtText.text = personMan.GetStatAmount(statType).ToString("0");
  }
}
