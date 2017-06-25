using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLManager : MonoBehaviour {

  private string url;
  private Text nameText;

	// Use this for initialization
	void Start () {
    url = Application.absoluteURL;
    SetName();
  }

  void SetName()
  {
    //url = "http://www.nomossgames.com/wegotchi/we/?n=Tania&otherstuff";
    int nameGet = url.IndexOf("n=");
    if (nameGet != -1)
    {
      nameGet += 2;
      nameText = GameObject.Find("name_text").GetComponent<Text>();
      int nextGet = url.IndexOf("&", nameGet);
      if (nextGet == -1) nextGet = url.Length;

      string nameString = url.Substring(nameGet, nextGet - nameGet);

      nameText.text = nameString;

    }
  }

  // Update is called once per frame
  void Update () {
		
	}
}
