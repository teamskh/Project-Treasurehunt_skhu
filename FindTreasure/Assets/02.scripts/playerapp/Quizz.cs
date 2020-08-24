using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ExtendedButtons;
using System;

public class Quizz : MonoBehaviour
{
    ExtendedButtons.Button3D[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentsInChildren<ExtendedButtons.Button3D>();
        SetPositiveListener( buttons[0]);
        SetNegativeListener( buttons[1]);
    }

    private void SetNegativeListener( Button3D button3D)
    {
        //button3D.onClick.AddListener();
        throw new NotImplementedException();
    }

    private void SetPositiveListener( Button3D button3D)
    {
        //button3D.onClick.AddListener();
        throw new NotImplementedException();
    }
}
