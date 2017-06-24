using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButton : MonoBehaviour {

  private DayManager dayMan;

	// Use this for initialization
	void Start () {
    dayMan = GameObject.Find("day_manager").GetComponent<DayManager>();
    if (dayMan.isRealTime) gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0))
    {
      dayMan.AdvanceTurn();
    }
  }
}
