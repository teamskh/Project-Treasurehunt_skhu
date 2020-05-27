using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenResolutionM : MonoBehaviour
{
    // Start is called before the first frame update
     void Start()
     {
         Screen.SetResolution(Screen.width, (Screen.width*16)/9, true);
        //Camera.main.orthographicSize = 1920/ (100.0f * 2.0f);
    }
}
