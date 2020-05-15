﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenResolutionM : MonoBehaviour
{
    // Start is called before the first frame update
 /*    void Start()
     {
         Screen.SetResolution(1080, 1920, true);
        Camera.main.orthographicSize = 1920/ (100.0f * 2.0f);
    }*/

    // Update is called once per frame
    /*  void Awake()
      {
          Camera camera = GetComponent<Camera>();
          Rect rect = camera.rect;
          print((Screen.width / Screen.height));
          float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
          float scalewidth = 1f / scaleheight;
          if (scaleheight < 1)
          {
              rect.height = scaleheight;
              rect.y = (1f - scaleheight) / 2f;
          }
          else
          {
              rect.width = scalewidth;
              rect.x = (1f - scalewidth) / 2f;
          }
          camera.rect = rect;

      }*/
    #region Pola
    private int ScreenSizeX = 0;
    private int ScreenSizeY = 0;
    #endregion

    #region metody

    #region rescale camera
    private void RescaleCamera()
    {

        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;

        float targetaspect = 9.0f / 16.0f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = GetComponent<Camera>();

        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }

        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }
    #endregion

    #endregion

    #region metody unity

    void OnPreCull()
    {
        if (Application.isEditor) return;
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;

    }

    // Use this for initialization
    void Start()
    {
        RescaleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        RescaleCamera();
    }
    #endregion
}
