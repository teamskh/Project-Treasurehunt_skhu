using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class QuizToServer : MonoBehaviour
{
    //for Test
    // 스키마가 있는 테이블 데이터 추가 테스트
    string id = "Admin";
    string pw = "toomuch";
    
    private void Start()
    {
        Backend.Initialize(() =>
        {
            if (Backend.IsInitialized)
            {
                BackendReturnObject bro = new BackendReturnObject();

                Debug.Log($"{id} : {pw}");

                bro = Backend.BMember.CustomLogin(id, pw);
                if (bro.IsSuccess())
                {
                    Debug.Log("Custom Login Success");
                }
                else
                    Debug.Log(bro.ToString());
            }
       
        });


    }   
}
