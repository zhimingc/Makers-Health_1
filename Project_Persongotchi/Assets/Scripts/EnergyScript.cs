using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour {

  public Image energyBar, stressBar;
  private PersonManager personMan;
  private Text energyText;

	// Use this for initialization
	void Start () {
    personMan = GameObject.Find("person").GetComponent<PersonManager>();
    energyText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
    UpdateBarHeight();
  }

  void UpdateBarHeight()
  {
    energyBar.fillAmount = personMan.GetEnergyPercentage();
    stressBar.fillAmount = personMan.GetStressPercentage();

    stressBar.GetComponentInChildren<Text>().text = personMan.stressAmt.ToString("0");
    energyText.text = personMan.curEnergy.ToString("0");
  }

}
