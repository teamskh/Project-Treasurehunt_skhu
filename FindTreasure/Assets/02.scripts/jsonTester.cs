using DataInfo;
using System.Collections;
using System.Collections.Generic;
using TTM.Save;
using UnityEngine;
using BackEnd;
using TTM.Server;
using TTM.Classes;

public class jsonTester : MonoBehaviour
{
    CompetitionDictionary dic;
    void Start()
    {
        Backend.Initialize(BRO =>
        {
            Debug.Log("Backend.Initialize " + BRO);
            // 성공
            if (BRO.IsSuccess())
            {
                 
            }
            // 실패
            else
            {
                Debug.LogError("Failed to initialize the backend");
            }
        });
       
       init();
    }
    public void init()
    {
        dic = new CompetitionDictionary();
        Competition comp = new Competition();
        comp.Mode = false;
        comp.Password = "1234";
        dic.Add("Test", comp);
        Debug.Log(comp.ToString());
        JsonFormatter.CompetitionFormatter(dic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
