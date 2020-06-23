using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

using DataInfo;

using TTM.Classes;
using TTM.Save;

public class rank : MonoBehaviour
{
    public Text Name;
    public Text NowTime;
    public InputField ConTestNa;
    private TimeSpan times;

    private void Start()
    {
        Name.text = PlayerPrefs.GetString("nickna");
        gameman.Instance.score = 0;
        
    }

    public void Update()
    {

    }

    public void Loadrank()
    {
        gameman.Instance.loadRankChek = true;
        Debug.Log("create");
    }

    public void Test()
    {
        PlayerPrefs.SetString("ConName",ConTestNa.ToString());
        PlayerPrefs.SetInt("Score", gameman.Instance.score);
    }
}
