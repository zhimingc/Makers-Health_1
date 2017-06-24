using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelDensityCamera : MonoBehaviour
{


  public float pixelsToUnits = 100;

  void Update()
  {
    Camera.main.orthographicSize = Screen.height / pixelsToUnits / 2;
  }
}