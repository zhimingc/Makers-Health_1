using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatButton : MonoBehaviour {

  public STAT_TYPE statType;
  private PersonManager personMan;

	// Use this for initialization
	void Start () {
    personMan = GameObject.Find("person").GetComponent<PersonManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0))
    {
      personMan.IncreaseStat(statType, 5);
    }
    if (Input.GetMouseButtonDown(1))
    {
      personMan.DecreaseStat(statType, 5);
    }
  }


}
