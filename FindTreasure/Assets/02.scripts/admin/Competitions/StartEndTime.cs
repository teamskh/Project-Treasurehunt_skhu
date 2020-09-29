using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;

public class StartEndTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Backend.Initialize(() =>
        {
            BackendReturnObject bro = Backend.BMember.CustomLogin("Admin", "toomuch");
            if (bro.IsSuccess())
            {
                BackendReturnObject data = Backend.GameSchemaInfo.Get("competitions", new Param(), 100);
                foreach (JsonData item in data.Rows())
                {
                    Debug.Log(item["starttime"]["S"].ToString());
                    Param where = new Param();
                    where.Add("code", int.Parse(item["code"]["N"].ToString()));
                    Param start = new Param();
                    start.Add("starttime", DateTime.Now.ToString());
                    Backend.GameSchemaInfo.Update("competitions", where, start);
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
