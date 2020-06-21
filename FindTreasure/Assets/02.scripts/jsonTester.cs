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
    string id = "Admin";
    string pw = "toomuch";

    BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;
    void Start()
    {
        Backend.Initialize(BRO =>
        {
            Debug.Log("Backend.Initialize " + BRO);
            // 성공
            if (BRO.IsSuccess())
            {
                Backend.BMember.CustomLogin(id, pw, callback =>
                {
                    Debug.Log("CustomLogin " + callback);
                    isSuccess = callback.IsSuccess();
                    bro = callback;
                });
            }
            // 실패
            else
            {
                Debug.LogError("Failed to initialize the backend");
            }
        });
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
        isSuccess = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSuccess)
        {
            Debug.Log("-------------Update(SaveToken)-------------");
            BackendReturnObject saveToken = Backend.BMember.SaveToken(bro);
            if (saveToken.IsSuccess())
            {
                Debug.Log("로그인 성공");
                //init();
                JsonFormatter.GetContentsByIndate();
               
            }
            else
            {
                Debug.Log("로그인 실패: " + saveToken.ToString());
            }
            isSuccess = false;
            bro.Clear();
        }
    }
}
